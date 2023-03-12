@echo off

rem Set default values for GS (gs with graphics window) and GSC
rem (console mode gs) if the user hasn't set them.

if NOT %GS%/==/ goto :gsset
if EXIST %~dp0..\bin\gswin32.exe set GS=%~dp0..\bin\gswin32
if %GS/==/ set GS=gswin32
:gsset

if NOT %GSC%/==/ goto :gscset
if EXIST %~dp0..\bin\gswin32c.exe set GSC=%~dp0..\bin\gswin32c
if %GSC%/==/ set GSC=gswin32c
:gscset
