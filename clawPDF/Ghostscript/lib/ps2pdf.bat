@echo off

rem Convert PostScript to PDF 1.4 (Acrobat 5-and-later compatible).
rem The default PDF compatibility level may change in the future:
rem use ps2pdf12 or ps2pdf13 if you want a specific level.

set LIBDIR=%~dp0

rem The current default compatibility level is PDF 1.4.
echo -dCompatibilityLevel#1.4 >"%TEMP%\_.at"
goto bot

rem Pass arguments through a file to avoid overflowing the command line.
:top
echo %1 >> "%TEMP%\_.at"
shift
:bot
rem Search for leading '-'
echo %1 | findstr /b /C:- >nul 2>&1
if ERRORLEVEL 1 goto proc
goto top
:proc
call "%LIBDIR%ps2pdfxx.bat" %1 %2
