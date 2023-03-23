<#

This script utilizes the Windows application clawPDF to merge the print queue into a single PDF file. 
It prompts the user to press Enter to merge the queue and displays the number of jobs in the queue. 
Then, it initializes a SaveFileDialog to prompt the user for the file path of the merged PDF file. 
Once the user selects a file path, the script merges all jobs in the queue and converts them to a PDF file.

#>

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Speech

$clawPDFQueue = New-Object -ComObject "clawPDF.JobQueue"
$clawPDFQueue.Initialize()

while($true) {
    Write-Host ""
    Write-Host "  Press 'Enter' to merge the printer queue to one file"
    Write-Host "  $($clawPDFQueue.Count) jobs in the print queue"
    Start-Sleep -Seconds 2
    Clear-Host
    if([System.Console]::KeyAvailable) {
        $key = [System.Console]::ReadKey($true).Key
        if($key -eq "Enter") {
            break
        }
    }
}

if ($clawPDFQueue.Count -eq 0) { exit }

$clawPDFQueue.MergeAllJobs()
$printJob = $clawPDFQueue.NextJob
$printJob.SetProfileByGuid("DefaultGuid")

$file_dialog = New-Object System.Windows.Forms.SaveFileDialog
$file_dialog.Filter = "PDF files (*.pdf)|*.pdf"
$file_dialog.Title = "Select a destination for the merged PDF file"
$file_dialog.InitialDirectory = [Environment]::GetFolderPath('MyDocuments')

if ($file_dialog.ShowDialog() -eq 'OK') {
    $file_path = $file_dialog.FileName
}
else {
    exit
}

$printJob.ConvertTo($file_path)

Write-Host "Releasing the object"
$clawPDFQueue.ReleaseCom()
