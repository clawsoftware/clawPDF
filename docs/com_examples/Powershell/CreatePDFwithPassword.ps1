$ShellObj = New-Object -ComObject Shell.Application
$clawPDFQueue = New-Object -ComObject clawPDF.JobQueue
$objFSO = New-Object -ComObject Scripting.FileSystemObject
$tmp = ""

$resultsFolderPath = Join-Path (Split-Path -Path $MyInvocation.MyCommand.Path) "Results"

$fullPath = Join-Path (Split-Path -Path $MyInvocation.MyCommand.Path) "Results\TestPagePDFwithPassword.pdf"

if (-not (Test-Path $resultsFolderPath -PathType Container)) {
Write-Host "Creating Results folder..."
New-Item -Path $resultsFolderPath -ItemType Directory | Out-Null
}

Write-Host "Initializing clawPDF queue..."
$clawPDFQueue.Initialize()

Write-Host "Printing a windows testpage"
$ShellObj.ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""clawPDF""", "", "open", 1)

Write-Host "Waiting for the job to arrive at the queue..."
if (-not $clawPDFQueue.WaitForJob(10)) {
Write-Host "The print job did not reach the queue within 10 seconds"
} else {
Write-Host "Currently there are $($clawPDFQueue.Count) job(s) in the queue"
Write-Host "Getting job instance"
$printJob = $clawPDFQueue.NextJob

$printJob.SetProfileByGuid("DefaultGuid")

Write-Host "Enable Security Settings"

$printJob.SetProfileSetting("PdfSettings.Security.Enabled", "true")
$printJob.SetProfileSetting("PdfSettings.Security.RequireUserPassword", "true")

$printJob.SetProfileSetting("PdfSettings.Security.EncryptionLevel", "Aes128Bit")

Write-Host "Password: test"

$printJob.SetProfileSetting("PdfSettings.Security.OwnerPassword", "test")
$printJob.SetProfileSetting("PdfSettings.Security.UserPassword", "test")

$printJob.ConvertTo($fullPath)

if (-not $printJob.IsFinished -or -not $printJob.IsSuccessful) {
    Write-Host "Could not convert the file: $fullPath"
} else {
    Write-Host "Job finished successfully"
}
}

Write-Host "Releasing the object"
$clawPDFQueue.ReleaseCom()

Read-Host "Press Enter to exit"