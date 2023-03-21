Framework "4.8"

Properties {
    $solution = "SystemWrapper.sln"
}

Include "Build\psake-common.ps1"

Task Default -Depends Clean, Build, RunTests

Task Pack -Depends Default -Description "Create NuGet packages." {
    $version = Get-BuildVersion

    $tag = $env:APPVEYOR_REPO_TAG_NAME
    if ($tag -And $tag.StartsWith("v$version-")) {
        "Using tag-based version for packages."
        $version = $tag.Substring(1)
    }

    Create-Package "SystemInterface" $version
    Create-Package "SystemWrapper" $version
}
