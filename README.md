# clawPDF // PDFCreator 2.3 fork

Yet another PDF Printer? Yes! This PDF Printer has the intention to be completely open source.<br><br>
Open Source virtual PDF printer for Windows 7 / 8 / 10 / 11 / RDS / Terminalserver<br>
Print to PDF, PDF/A, PDF/X, PNG, JPEG, TIF and TXT

# Download

https://github.com/clawsoftware/clawPDF/releases/download/0.8.6/clawPDF_0.8.6_setup.msi


# Features

- Print to PDF, PDF/A, PDF/X, PNG, JPEG, TIF and TXT
- Full unicode support
- Multiple profiles
- Post Actions
- Create additional printers with assigned profile
- 24 languages
- Many settings
- Easy to use
- Easy to deploy (MSI-Installer & Config)
- No adware, spyware, nagware
- ...

# Tested under

- Windows Server 2022 Terminalserver/RDS
- Windows Server 2019 Terminalserver/RDS
- Windows Server 2016 Terminalserver/RDS
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

## v0.8.6 (2023.03.16)

- [feature] Profile Settings -> Actions -> Run Script -> Hide the process execution
- [feature] Config parameter e.g. to deploy settings in enterprise environments
- [bugfix]  Unicode support in usernames
- [bugfix]  Printing is now working for ghostscript parameters with east-asian characters
- [bugfix]  Profiles for additional printers work now (Application Settings -> Printers -> Profile)
- [misc]    Optimizations for Windows Remote Desktop

## v0.8.5 (2023.03.11)

- [update]	Update to Ghostscript 10
- [bugfix]  MapiClient (thx to christian1980nrw)
- [bugfix]  FtpConnection (thx to droshcom)
- [bugfix]  Typo Czech.ini (thx to PetrTodorov)
- [bugfix]  Fixed printing via UWP print dialog
- [feature] OpenViewer setting (thx to victorromeo)
- [feature] Batch Printing
- [misc]	Optimizations

## v0.8.4 (2019.06.11)

- [bugfix]  Unicode filename support (thx to daooze for bugreport)

## v0.8.3 (2019.05.31)

- [bugfix]  Starts under System-Account
- [cleanup] Migrated code from c++ to c#
- [update]  Ghostscript 9.27
- [bugfix]  Author metadata

## v0.8.1 (2019.02.10)

- [bugfix] Performance boost for RDS environments

## v0.8.0 (2019.02.10)

- Initial version


# Requirements

- .Net Framework 4.5.2+


# Screenshot

![clawpdf1](clawPDF/docs/images/clawpdf1.png?raw=true "clawpdf1")
![clawpdf2](clawPDF/docs/images/clawpdf2.png?raw=true "clawpdf2")
![clawpdf3](clawPDF/docs/images/clawpdf3.png?raw=true "clawpdf3")
![clawpdf4](clawPDF/docs/images/clawpdf4.png?raw=true "clawpdf4")
![clawpdf5](clawPDF/docs/images/clawpdf5.png?raw=true "clawpdf5")
![clawpdf6](clawPDF/docs/images/clawpdf6.png?raw=true "clawpdf6")


# Build

- Visual Studio 2019


# Third-party

## clawPDF uses the following licensed software or parts of the source code:

- main code: PDFCreator 2.3 (https://github.com/pdfforge/PDFCreator), licensed under AGPL v3 license.
- PDF library: iTextSharp 5.5.13 (https://github.com/itext/itextsharp), licensed under AGPL v3 license.
- logging: Nlog 4.5.11 (https://github.com/NLog/NLog), licensed under BSD 3-Clause.
- parts of the ghostscript control: PdfScribe 1.0.6 (https://github.com/stchan/PdfScribe), licensed under AGPL v3 license.
- redirection Port Monitor: clawmon (https://github.com/clawsoftware/clawmon), licensed under GPL v2 license.
- Postscript Printer Drivers: Microsoft Postscript Printer Driver V3 (https://docs.microsoft.com/en-us/windows-hardware/drivers/print/microsoft-postscript-printer-driver), copyright (c) Microsoft Corporation. All rights reserved.
- Postscript and PDF interpreter/renderer: Ghostscript 10 (https://www.ghostscript.com/download/gsdnld.html), licensed under AGPL v3 license.
- SystemWrapper 0.25.0.186 (https://github.com/jozefizso/SystemWrapper), licensed under Microsoft Public license.
- ftplib 1.0.1.1 (https://archive.codeplex.com/?p=ftplib), licensed under MIT license.
- DataStorage.dll, licensed under pdfforge Freeware License.
- DynamicTranslator.dll, licensed under pdfforge Freeware License.
- TrueTypeFontInfo.dll, licensed under pdfforge Freeware License.
- appbar_save (http://modernuiicons.com/), licensed under Attribution-NoDerivs 3.0 Unported.
- appbar_cogs (http://modernuiicons.com/), licensed under Attribution-NoDerivs 3.0 Unported.
- appbar_page_file_pdf (http://modernuiicons.com/), licensed under Attribution-NoDerivs 3.0 Unported.


# License

clawPDF is licensed under AGPL v3 license<br>
Copyright (C) 2023 // Andrew Hess // clawSoft
