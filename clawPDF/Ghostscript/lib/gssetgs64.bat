@echo off

rem Set default values for GS (gs with graphics window) and GSC
rem (console mode gs) if the user hasn't set them.

if NOT %GS%/==/ goto :gsset
if EXIST %~dp0..\bin\gswin64.exe set GS=%~dp0..\bin\gswin64
if %GS/==/ set GS=gswin64
:gsset

if NOT %GSC%/==/ goto :gscset
if EXIST %~dp0..\bin\gswin64c.exe set GSC=%~dp0..\bin\gswin64c
if %GSC%/==/ set GSC=gswin64c
:gscset
