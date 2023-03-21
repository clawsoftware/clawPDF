# clawPDF - Virtual PDF/OCR/Image Printer

Yet another PDF/OCR/Image Printer? Yes! This PDF/OCR/Image Printer has the intention to be completely open source.<br><br>
Open Source virtual PDF printer for Windows 7 / 8 / 10 / 11 / RDS<br>
Print to PDF, PDF/A-1b, PDF/A-2b, PDF/A-3b, OCR, PDF/X, PDF/Image, PNG, JPEG, TIF and TXT.

# Download

https://github.com/clawsoftware/clawPDF/releases/download/0.8.7/clawPDF_0.8.7_setup.msi


# Features

- Print to PDF, PDF/A-1b, PDF/A-2b, PDF/A-3b, OCR, PDF/X, PDF/Image, PNG, JPEG, TIF and TXT
- Print 100% valid [PDF/A-1b](https://github.com/clawsoftware/clawPDF/raw/master/docs/pdfa_valid/PDFA-1b.pdf), [PDF/A-2b](https://github.com/clawsoftware/clawPDF/raw/master/docs/pdfa_valid/PDFA-2b.pdf) and [PDF/A-3b](https://github.com/clawsoftware/clawPDF/raw/master/docs/pdfa_valid/PDFA-3b.pdf)
- [Optical Character Recognition (OCR)](https://github.com/clawsoftware/clawPDF/wiki/Optical-Character-Recognition-(OCR))
- [Drag and Drop Support](https://github.com/clawsoftware/clawPDF/wiki/Drag-and-Drop)
- [Merge Files](https://github.com/clawsoftware/clawPDF/wiki/Merge-Files)
- [Command Line Support](https://github.com/clawsoftware/clawPDF/wiki/Command-Line-Commands)
- [Silent Printing](https://github.com/clawsoftware/clawPDF/wiki/Silent-Print)
- Full Unicode Support
- Multiple Profiles
- [Post Actions](https://github.com/clawsoftware/clawPDF/wiki/Post-Actions)
- Create additional printers with assigned profile
- [24 translations. Add yours!](https://github.com/clawsoftware/clawPDF/wiki/Translations)
- Many settings
- Easy to use
- Easy to deploy (MSI-Installer & Config)
- No adware, spyware, nagware

# Demo

## Print a PDF/A and protect it with a password

![PrintPDFAwithPassword](docs/images/PrintPDFAwithPassword.gif?raw=true "PrintPDFAwithPassword")

## Merge multiple documents

![Merge](docs/images/MergeFiles.gif?raw=true "Merge")

# Tested under

- Windows Server 2022 RDS
- Windows Server 2019 RDS
- Windows Server 2016 RDS
- Windows 11 x64
- Windows 10 x32/x64
- Windows 8 x32/x64
- Windows 7 x32/x64

# Commandline

## Batch Printing
```
The GUID for the Profile parameter is located under: HKEY_CURRENT_USER\Software\clawSoft\clawPDF\Settings\ConversionProfiles\[id]\Guid

clawPDF.exe /PrintFile=D:\example.docx /profile=f81ea998-3a76-4104-a574-9a66d6f3039b
clawPDF.exe /PrintFile=D:\example.pdf /profile=JpegGuid
clawPDF.exe /PrintFile=D:\example.pdf /profile=JpegGuid /OutputPath=D:\batchjob

clawPDF.exe /PrintFile=D:\example.txt /printerName=clawPDF2
clawPDF.exe /PrintFile=D:\example.docx /printerName=clawJPG
```

## Overwrite Config
```
- To deploy a default configuration in an enterprise environment.
- To export a configuration select "Application Settings -> Debug -> Save settings to file".

clawPDF.exe /Config=D:\clawPDF.ini
```

## Printer Managment
```
SetupHelper.exe /Printer=Add /Name=ExamplePrinter
SetupHelper.exe /Printer=Remove /Name=ExamplePrinter
```

## ManagePrintJobs
```
clawPDF.exe /ManagePrintJobs
```


# Changelog

## v0.8.7 (2023.03.21)

- [feature] Optical Character Recognition (OCR)
- [feature] Added PDF/A-3b
- [feature] Added PDF/Image
- [feature] Added more settings to the Print Job window
- [feature] Application Settings -> General -> Print Job Window -> Stay on top
- [feature] OutputPath Parameter
- [misc]    Optimizations

[more](https://github.com/clawsoftware/clawPDF/wiki/Changlog)


# Requirements

- .Net Framework 4.6.2+


# Screenshot

![clawpdf1](clawPDF/docs/images/clawpdf1.png?raw=true "clawpdf1")
![clawpdf2](clawPDF/docs/images/clawpdf2.png?raw=true "clawpdf2")
![clawpdf3](clawPDF/docs/images/clawpdf3.png?raw=true "clawpdf3")
![clawpdf4](clawPDF/docs/images/clawpdf4.png?raw=true "clawpdf4")
![clawpdf5](clawPDF/docs/images/clawpdf5.png?raw=true "clawpdf5")
![clawpdf6](clawPDF/docs/images/clawpdf6.png?raw=true "clawpdf6")


# Build

- Visual Studio 2022

[more](https://github.com/clawsoftware/clawPDF/wiki/Build-it-yourself)

# Third-party

## clawPDF uses the following licensed software or parts of the source code:

- PDFCreator (https://github.com/pdfforge/PDFCreator), licensed under AGPL v3 license.
- PDF library: iTextSharp (https://github.com/itext/itextsharp), licensed under AGPL v3 license.
- Logging: Nlog (https://github.com/NLog/NLog), licensed under BSD 3-Clause.
- Parts of the ghostscript control: PdfScribe (https://github.com/stchan/PdfScribe), licensed under AGPL v3 license.
- Redirection Port Monitor: clawmon (https://github.com/clawsoftware/clawmon), licensed under GPL v2 license.
- Postscript Printer Drivers: Microsoft Postscript Printer Driver V3 (https://docs.microsoft.com/en-us/windows-hardware/drivers/print/microsoft-postscript-printer-driver), copyright (c) Microsoft Corporation. All rights reserved.
- Postscript and PDF interpreter/renderer: Ghostscript 10 (https://www.ghostscript.com/download/gsdnld.html), licensed under AGPL v3 license.
- SystemWrapper (https://github.com/jozefizso/SystemWrapper), licensed under Microsoft Public license.
- Ftplib (https://archive.codeplex.com/?p=ftplib), licensed under MIT license.
- DataStorage.dll, licensed under pdfforge Freeware License.
- DynamicTranslator.dll, licensed under pdfforge Freeware License.
- TrueTypeFontInfo.dll, licensed under pdfforge Freeware License.
- Appbar_save (http://modernuiicons.com/), licensed under Attribution-NoDerivs 3.0 Unported.
- Appbar_cogs (http://modernuiicons.com/), licensed under Attribution-NoDerivs 3.0 Unported.
- Appbar_page_file_pdf (http://modernuiicons.com/), licensed under Attribution-NoDerivs 3.0 Unported.


# License

clawPDF is licensed under AGPL v3 license<br>
Copyright (C) 2023 // Andrew Hess // clawSoft
