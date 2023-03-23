<#

This script uses the Windows application clawPDF to retrieve all installed clawPDF printers and display them.

#>

$clawPDFObj = New-Object -ComObject clawPDF.clawPDFObj
$printers = $clawPDFObj.GetclawPDFPrinters
$i = 0
$allPrinters = ""

if($clawPDFObj.IsInstanceRunning)
{
    Write-Host $clawPDFObj.IsInstanceRunning
}

while($i -lt $printers.Count)
{
    $allPrinters += "`n" + $printers.GetPrinterByIndex($i)
    $i++
}

Write-Host $allPrinters

Read-Host "Press Enter to exit"