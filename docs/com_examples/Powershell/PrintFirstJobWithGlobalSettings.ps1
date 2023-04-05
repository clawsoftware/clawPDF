<#

The code creates a PDF conversion job using a software called clawPDF, which converts a Windows test page to a PDF document with password protection. The code initializes the clawPDF job queue, sets global settings and simulates user input for 10 seconds. It then execute a print command for the Windows test page. After the print job is completed, the code checks if the conversion job was successful.

#>

$ShellObj = New-Object -ComObject Shell.Application
$clawPDFQueue = New-Object -ComObject clawPDF.JobQueue
$objFSO = New-Object -ComObject Scripting.FileSystemObject
$resultsFolderPath = $PSScriptRoot
$fullPath = Join-Path $resultsFolderPath "WinTestPage2Pdf.pdf"

Write-Host "Initializing clawPDF queue..."
$clawPDFQueue.Initialize()

Write-Host "Set global settings..."
$clawPDFQueue.SetProfileSetting("PdfSettings.Security.Enabled", "true")
$clawPDFQueue.SetProfileSetting("PdfSettings.Security.RequireUserPassword", "true")
$clawPDFQueue.SetProfileSetting("PdfSettings.Security.EncryptionLevel", "Aes128Bit")

Write-Host "Password: test"
$clawPDFQueue.SetProfileSetting("PdfSettings.Security.OwnerPassword", "test")
$clawPDFQueue.SetProfileSetting("PdfSettings.Security.UserPassword", "test")

Write-Host "Simulate user input for 10 seconds..."
Start-Sleep -Seconds 10

Write-Host "Printing a windows testpage"
$ShellObj.ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""clawPDF""", "", "open", 1)

$printJob = $clawPDFQueue.WaitForFirstJob()
$printJob.ConvertTo($fullPath)

if (-not $printJob.IsFinished -or -not $printJob.IsSuccessful) {
    Write-Host "Could not convert the file: $fullPath"
}
else {
    Write-Host "Job finished successfully"
}


Write-Host "Releasing the object"
$clawPDFQueue.ReleaseCom()

Read-Host "Press Enter to exit"