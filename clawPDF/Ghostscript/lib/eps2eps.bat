@echo off 
@rem "Distill" Encapsulated PostScript.

if %1/==/ goto usage
if %2/==/ goto usage
call "%~dp0gssetgs.bat"
echo -dNOPAUSE -dBATCH -P- -dSAFER >"%TEMP%\_.at"
rem Watcom C deletes = signs, so use # instead.
echo -dDEVICEWIDTH#250000 -dDEVICEHEIGHT#250000 >>"%TEMP%\_.at"
:cp
if %3/==/ goto doit
echo %1 >>"%TEMP%\_.at"
shift
goto cp

:doit
rem Watcom C deletes = signs, so use # instead.
%GSC% -q -sDEVICE#eps2write -sOutputFile#%2 @"%TEMP%\_.at" %1
if exist "%TEMP%\_.at" erase "%TEMP%\_.at"
goto end

:usage
echo "Usage: eps2eps ...switches... input.eps output.eps"

:end
