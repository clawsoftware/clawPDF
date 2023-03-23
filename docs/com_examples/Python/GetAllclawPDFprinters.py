# This script uses the Windows application clawPDF to retrieve all installed clawPDF printers and display them.

import win32com.client as win32

clawPDFObj = win32.Dispatch("clawPDF.clawPdfObj")

i = 0
allPrinters = ""

# Check if instance is running
if clawPDFObj.IsInstanceRunning:
    print(clawPDFObj.IsInstanceRunning)

printers = clawPDFObj.GetclawPdfPrinters

while i < printers.Count:
    allPrinters += f"{printers.GetPrinterByIndex(i)}\n"
    i += 1

print(allPrinters)
