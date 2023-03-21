Properties {
    ### Directories
    $base_dir = resolve-path .
    $build_dir = "$base_dir\Build"
    $publish_dir = "$base_dir\publish"
    $package_dir = "$base_dir\packages"

    ### Tools
    $nuget = "$base_dir\.nuget\nuget.exe"
    $nunit = "$package_dir\NUnit.Runners.*\tools\nunit-console.exe"

    ### AppVeyor-related
    $appVeyorConfig = "$base_dir\appveyor.yml"
    $appVeyor = $env:APPVEYOR

    ### Project information
    $solution_path = "$base_dir\$solution"
    $configuration = "Debug"
    $sharedAssemblyInfo = "$build_dir\AssemblySharedInfo.cs"
}

## Tasks

Task Restore -Description "Restore NuGet packages for solution." {
    "Restoring NuGet packages for '$solution'..."
    Exec { .$nuget restore $solution }
}

Task Clean -Description "Clean up publish folder and solution." {
    "Cleaning publish folder..."
    Clean-Directory $publish_dir

    "Cleaning '$solution'..."
    $extra = $null
    if ($appVeyor) {
        $extra = "/logger:C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll"
    }

    Exec { msbuild $solution_path /t:Clean /nologo /verbosity:minimal $extra }
}

Task Build -Depends Restore -Description "Rebuild all the projects in a solution." {
    "Building '$solution'..."

    $extra = $null
    if ($appVeyor) {
        $extra = "/logger:C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll"
    }

    Exec { msbuild $solution_path /t:Build /p:Configuration=$configuration /nologo /verbosity:minimal $extra }
}

Task RunTests -Depends Build -Description "Run unit tests for solution." {
    Create-Directory $publish_dir

    Run-Tests "SystemWrapper.Tests"
}

## Functions

### Test functions

function Run-Tests($project) {
    "Running unit tests for project '$project'..."
    $assembly = Get-Assembly $project

    if ($appVeyor) {
        Exec { nunit-console $assembly /framework:net-4.5 }
    } else {
        Exec { .$nunit $assembly /framework:net-4.5 /result:"$publish_dir\${project}_net40.xml" }
    }
}

### Pack functions

function Create-Package($project, $version) {
    Build-Project $project "Release"
    Build-Project $project "Release" "Package"
}

function Build-Project($project, $config, $target = "Build") {

    $extra = $null
    if ($appVeyor) {
        $extra = "/logger:C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll"
    }

    Exec { msbuild "$base_dir\$project\$project.csproj" /t:$target /p:Configuration=$config /p:Platform=AnyCPU /verbosity:minimal $extra }
}


### Version functions

function Get-BuildVersion {
    $version = Get-SharedVersion
    $buildNumber = $env:APPVEYOR_BUILD_NUMBER

    if ($env:APPVEYOR_REPO_TAG -ne "True" -And $buildNumber -ne $null) {
        $version += "-build-" + $buildNumber.ToString().PadLeft(5, '0')
    }

    return $version
}

function Get-SharedVersion {
    $line = Get-Content "$sharedAssemblyInfo" | where {$_.Contains("AssemblyVersion")}
    $line.Split('"')[1]
}

### Common functions

function Create-Directory($dir) {
    New-Item -Path $dir -Type Directory -Force > $null
}

function Clean-Directory($dir) {
    If (Test-Path $dir) {
        "Cleaning up '$dir'..."
        Remove-Item "$dir\*" -Recurse -Force
    }
}

function Remove-Directory($dir) {
    if (Test-Path $dir) {
        "Removing '$dir'..."
        Remove-Item $dir -Recurse -Force
    }
}

function Copy-Files($source, $destination) {
    Copy-Item "$source" $destination -Force > $null
}

function Move-Files($source, $destination) {
    Move-Item "$source" $destination -Force > $null
}

function Replace-Content($file, $pattern, $substring) {
    (gc $file) -Replace $pattern, $substring | sc $file
}

function Get-Assembly($project, $assembly) {
    if (!$assembly) {
        $assembly = $project
    }
    return (Get-OutputDir $base_dir $project) + "\$assembly.dll"
}

function Get-OutputDir($dir, $project, $config = $configuration) {
    return "$dir\$project\bin\$config"
}
