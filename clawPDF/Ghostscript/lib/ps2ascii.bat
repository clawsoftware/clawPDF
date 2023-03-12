@echo off

call "%~dp0gssetgs.bat"
if '%1'=='' goto a0
if '%2'=='' goto a1
%GSC% -dSAFER -sDEVICE=txtwrite -o %2 %1
goto x
:a0
%GSC% -dSAFER -sDEVICE=txtwrite -o - -
goto x
:a1
%GSC% -dSAFER -sDEVICE=txtwrite -o - %1
goto x
:x
