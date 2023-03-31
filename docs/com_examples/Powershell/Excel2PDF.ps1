### Options start

# Define Range
$rangeSheet = "A1"

# Define Printer
$printer = "clawPDF"

# Metadata
$printJobAuthor = ""
$subject = ""
$printJobName = ""

# Define OpenViewer
$openViewer = "false"

### Options end

<#

This script performs the task of printing Excel worksheets as PDF files using the clawPDF software. It first prompts the user to select an Excel file and an output folder using dialogs. It then opens the Excel file and loops through each worksheet, printing the worksheet to the clawPDF job queue and converting it to a PDF file. The PDF file is saved in the specified output folder with the name of the first cell of the printed range as the filename. The script also allows the user to set metadata such as the print job author, subject, and print job name. Finally, it closes the Excel file and exits the Excel application.

#>

# Create Shell and clawPDF objects
$shell = New-Object -ComObject Shell.Application
$clawPDFQueue = New-Object -ComObject clawPDF.JobQueue

# Create FileSystemObject
$objFSO = New-Object -ComObject Scripting.FileSystemObject

# Add System.Windows.Forms
Add-Type -AssemblyName System.Windows.Forms

# Define Excel file path using a file dialog
$excelFileDialog = New-Object System.Windows.Forms.OpenFileDialog
$excelFileDialog.Title = 'Select Excel Workbook'
$excelFileDialog.InitialDirectory = [Environment]::GetFolderPath('Desktop')
$excelFileDialog.Filter = 'Excel Workbook (*.xlsx; *.xls)|*.xlsx;*.xls|All Files (*.*)|*.*'
$excelFileDialog.ShowDialog() | Out-Null
$excelFilePath = $excelFileDialog.FileName

# Define output path using a folder dialog
$resultsFolderDialog = New-Object System.Windows.Forms.FolderBrowserDialog
$resultsFolderDialog.Description = 'Select Output Folder'
$resultsFolderDialog.SelectedPath = [Environment]::GetFolderPath('Desktop')
$resultsFolderDialog.ShowDialog() | Out-Null
$resultsFolderPath = $resultsFolderDialog.SelectedPath

# Create results folder if it does not exist
if (-not (Test-Path $resultsFolderPath -PathType Container)) {
    Write-Host "Creating Results folder..."
    New-Item -Path $resultsFolderPath -ItemType Directory | Out-Null
}

# Create Excel object
$excel = New-Object -ComObject Excel.Application

# Open Excel file
$workbook = $excel.Workbooks.Open($excelFilePath)

# Loop through all worksheets in the Excel file
foreach ($worksheet in $workbook.Worksheets) {

    # Read data from the first cell
    $range = $worksheet.Range($rangeSheet)

    # Print worksheet name and data
    Write-Host "Worksheet: $($worksheet.Name)"
    $range.Value2

    # Initialize clawPDF job queue
    $clawPDFQueue.Initialize()

    # Print worksheet to clawPDF job queue
    $worksheet.PrintOut(1, 1, 1, $false, $printer, $false, $true, "", $false)

    # Wait for job to arrive at the queue
    Write-Host "Waiting for the job to arrive at the queue..."
    if (-not $clawPDFQueue.WaitForJob(10)) {
        Write-Host "The print job did not reach the queue within 10 seconds"
    } else {
        Write-Host "Currently there are $($clawPDFQueue.Count) job(s) in the queue"
        Write-Host "Getting job instance"
        $printJob = $clawPDFQueue.NextJob

        # Set job profile
        $printJob.SetProfileByGuid("DefaultGuid")

        # Define full path for output PDF file
        $fullPath = [System.IO.Path]::Combine($resultsFolderPath, "$($range.Value2).pdf")

        # Set print job metadata and profile settings
        $printJob.PrintJobInfo.PrintJobAuthor = $printJobAuthor
        $printJob.PrintJobInfo.Subject = $subject
        $printJob.PrintJobInfo.PrintJobName = $printJobName
        $printJob.SetProfileSetting("OpenViewer", $openViewer)
	 
        # Convert job to PDF
        $printJob.ConvertTo($fullPath)

        # Check if conversion was successful
        if (-not $printJob.IsFinished -or -not $printJob.IsSuccessful) {
            Write-Host "Could not convert the file: $fullPath"
        } else {
            Write-Host "Job finished successfully"
        }
    }

    # Release clawPDF object
    Write-Host "Releasing the object"
    $clawPDFQueue.ReleaseCom()
}

# Close Excel file and exit Excel application
$workbook.Close()
$excel.Quit()
