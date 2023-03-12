@echo off

if %1/==/ goto usage
if %2/==/ goto usage

call "%~dp0gssetgs.bat"

set infile=%~1
set outfile=%~2

rem Now convert the input to EPSF and add the Preview to the EPSF file
%GSC% -q -dNOOUTERSAVE -dNODISPLAY -dLastPage=1 -sOutputFile=%outfile% --permit-file-read=%infile% -- %~dp0ps2epsi.ps %infile%

goto end

:usage
echo "Usage: ps2epsi <infile.ps> <outfile.epi>"

:end
