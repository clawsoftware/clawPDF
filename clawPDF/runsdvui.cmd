cd /d "C:\Git\clawPDF\clawPDF" &msbuild "clawPDF.csproj" /t:sdvViewer /p:configuration="Debug" /p:platform=x86
exit %errorlevel% 