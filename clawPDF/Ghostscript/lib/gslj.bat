@echo off

call "%~dp0gssetgs.bat"
%GSC% -q -sDEVICE=laserjet -r300 -P- -dSAFER -dNOPAUSE -sPROGNAME=gslj -- gslp.ps %1 %2 %3 %4 %5 %6 %7 %8 %9
