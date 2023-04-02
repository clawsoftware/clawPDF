# clawPDF - Virtual PDF/OCR/Image Printer

Yet another PDF/OCR/Image Printer? Yes! This PDF/OCR/Image Printer has the intention to be completely open source.<br><br>
Open Source virtual PDF printer for Windows 7 / 8 / 10 / 11 / Windows Server RDS.<br><br>
Print to PDF, PDF/A-1b, PDF/A-2b, PDF/A-3b, PDF/X, PDF/Image, OCR, SVG, PNG, JPEG, TIF and TXT.

# Download

https://github.com/clawsoftware/clawPDF/releases/download/0.9.0/clawPDF_0.9.0_setup.msi


# Features

- Print to PDF, PDF/A-1b, PDF/A-2b, PDF/A-3b, PDF/X, PDF/Image, OCR, SVG, PNG, JPEG, TIF and TXT
- Print 100% valid [PDF/A-1b](https://github.com/clawsoftware/clawPDF/raw/master/docs/pdfa_valid/PDFA-1b.pdf), [PDF/A-2b](https://github.com/clawsoftware/clawPDF/raw/master/docs/pdfa_valid/PDFA-2b.pdf) and [PDF/A-3b](https://github.com/clawsoftware/clawPDF/raw/master/docs/pdfa_valid/PDFA-3b.pdf)
- [Optical Character Recognition (OCR)](https://github.com/clawsoftware/clawPDF/wiki/Optical-Character-Recognition-(OCR))
- [Scripting Interface (Python, Powershell, VBScript...)](https://github.com/clawsoftware/clawPDF/wiki/Scripting-Interface)
- [Network Printing](https://github.com/clawsoftware/clawPDF/wiki/Install-as-Network-Printer)
- [SVG Export](https://github.com/clawsoftware/clawPDF/wiki/SVG-Export)
- [Drag and Drop Support](https://github.com/clawsoftware/clawPDF/wiki/Drag-and-Drop)
- [Merge Files](https://github.com/clawsoftware/clawPDF/wiki/Merge-Files)
- [Command Line Support](https://github.com/clawsoftware/clawPDF/wiki/Command-Line-Commands)
- [Silent Printing](https://github.com/clawsoftware/clawPDF/wiki/Silent-Printing)
- [Custom Paper Sizes / Standard Paper Sizes](https://github.com/clawsoftware/clawPDF/wiki/(Custom)-Paper-Sizes)
- Full Unicode Support
- Multiple Profiles
- [Post Actions](https://github.com/clawsoftware/clawPDF/wiki/Post-Actions)
- Create additional printers with assigned profile
- [24 translations. Add yours!](https://github.com/clawsoftware/clawPDF/wiki/Translations)
- Easy to deploy (MSI-Installer & Config)
- Many settings
- Easy to use
- No adware, spyware and nagware

# Demo

## Optical Character Recognition (OCR)

![OCR](docs/images/ImageOCR.gif?raw=true "OCR")

## [Scripting Interface](https://github.com/clawsoftware/clawPDF/blob/master/docs/com_examples/Powershell/CreatePDFwithPassword.ps1)

![ScriptingInterface](docs/images/ScriptingInterface.gif?raw=true "Scripting Interface")

## Print a PDF/A and protect it with a password

![PrintPDFAwithPassword](docs/images/PrintPDFAwithPassword.gif?raw=true "PrintPDFAwithPassword")

## Merge multiple documents

![Merge](docs/images/MergeFiles.gif?raw=true "Merge")

# Tested under

- Windows Server 2022 RDS
- Windows Server 2019 RDS
- Windows Server 2016 RDS
- Windows 11 x64
- Windows 10 x86/x64
- Windows 8 x86/x64
- Windows 7 x86/x64

# Command Line

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

## v0.9.1 (TBA)

- [feature] Windows ARM64 support (Surface, Windows on Apple M1/M2/M3, ...)

## v0.9.0 (28.03.2023)

- [feature] Network printing support
- [feature] SVG file support
- [update] Ghostscript 10.01.1
- [update] Changed default time server
- [bugfix] PDF/X code regression
- [setup] Visual C++ Redistributable 14 dependency check

[more](https://github.com/clawsoftware/clawPDF/wiki/Changelog)


# Requirements

- .Net Framework 4.6.2+
- [Visual C++ Redistributable 14](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist#visual-studio-2015-2017-2019-and-2022) (Download: [x86](https://aka.ms/vs/17/release/vc_redist.x86.exe)/[x64](https://aka.ms/vs/17/release/vc_redist.x64.exe))


# Build

- Visual Studio 2022

[more](https://github.com/clawsoftware/clawPDF/wiki/Build-it-yourself)

# Third-party

## clawPDF uses the following licensed software or parts of the source code:

- PDFCreator (https://github.com/pdfforge/PDFCreator), licensed under AGPL v3 license.
- Pdftosvg.net (https://github.com/dmester/pdftosvg.net), licensed under MIT license.
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
