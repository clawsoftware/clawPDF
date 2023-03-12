@echo off

call "%~dp0gssetgs.bat"
%GSC% -q -sDEVICE=epson -r180 -P- -dSAFER -dNOPAUSE -sPROGNAME=gslp -- gslp.ps %1 %2 %3 %4 %5 %6 %7 %8 %9
