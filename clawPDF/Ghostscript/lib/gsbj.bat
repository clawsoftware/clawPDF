@echo off

call "%~dp0gssetgs.bat"
%GSC% -q -sDEVICE=bj10e -r180 -P- -dSAFER -dNOPAUSE -sPROGNAME=gsbj -- gslp.ps %1 %2 %3 %4 %5 %6 %7 %8 %9
