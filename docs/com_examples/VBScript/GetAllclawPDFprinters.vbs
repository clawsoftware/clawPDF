' This script uses the Windows application clawPDF to retrieve all installed clawPDF printers and display them.

Dim clawPDFObj
Set clawPDFObj = CreateObject("clawPDF.clawPdfObj")
Dim printers
Set printers = clawPDFObj.GetclawPDFPrinters()
Dim i
i = 0
Dim allPrinters
allPrinters = ""

If clawPDFObj.IsInstanceRunning Then
WScript.Echo clawPDFObj.IsInstanceRunning
End If

While i < printers.Count
allPrinters = allPrinters + vbCrLf + printers.GetPrinterByIndex(i)
i = i + 1
Wend

WScript.Echo allPrinters