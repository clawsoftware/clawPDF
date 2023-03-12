@echo off 
@rem Convert PDF to PostScript.

if %1/==/ goto usage
if %2/==/ goto usage
call "%~dp0gssetgs.bat"
echo -dNOPAUSE -dBATCH -P- -dSAFER -sDEVICE#ps2write >"%TEMP%\_.at"
:cp
if %3/==/ goto doit
echo %1 >>"%TEMP%\_.at"
shift
goto cp

:doit
rem Watcom C deletes = signs, so use # instead.
%GSC% -q -sOutputFile#%2 @"%TEMP%\_.at" %1
if exist "%TEMP%\_.at" erase "%TEMP%\_.at"
goto end

:usage
echo "Usage: pdf2ps [-dASCII85DecodePages=false] [-dLanguageLevel=n] input.pdf output.ps"

:end
