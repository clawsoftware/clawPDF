@echo off
rem Internal batch file for calling pdfwrite driver.

rem The files that call this one (ps2pdf*.bat) write the command-line
rem options into _.at, and then pass the last 2 (or fewer) arguments
rem to this file.

call "%~dp0gssetgs.bat"
echo -q -P- -dSAFER -dNOPAUSE -dBATCH -sDEVICE#pdfwrite >"%TEMP%\_.at"2

if "%OS%"=="Windows_NT" goto nt

rem	Run ps2pdf on any Microsoft OS.

if %1/==/ goto usage
if %2/==/ goto usage

rem Watcom C deletes = signs, so use # instead.
rem We have to include the options twice because -I only takes effect if it
rem appears before other options.

:run
echo -sOutputFile#%2 >>"%TEMP%\_.at"2
copy /b /y "%TEMP%\_.at"2+"%TEMP%\_.at" "%TEMP%\_.at"2 >NUL
echo %1 >>"%TEMP%\_.at"2
%GSC% @"%TEMP%\_.at" @"%TEMP%\_.at"2
goto end

:usage
echo Usage: ps2pdf [options...] input.[e]ps output.pdf
goto end

rem	Run ps2pdf on Windows NT.

:nt
if not CMDEXTVERSION 1 goto run
if %1/==/ goto ntusage
if %2/==/ goto nooutfile
goto run

:ntusage
echo Usage: ps2pdf input.ps [output.pdf]
echo    or: ps2pdf [options...] input.[e]ps output.pdf
goto end

:nooutfile
rem We don't know why the circumlocution with _1 is needed....
set _1=%1
set _outf=%_1:.PS=.pdf%
if %_1%==%_outf% goto addsuff
call "%~dp0ps2pdfxx" %1 %_outf%
goto postsuff

:addsuff
call "%~dp0ps2pdfxx" %1 %1%.pdf

:postsuff
set _1=
set _outf=

:end
rem	Clean up.
if exist "%TEMP%\_.at" erase "%TEMP%\_.at"
if exist "%TEMP%\_.at"2 erase "%TEMP%\_.at"2
