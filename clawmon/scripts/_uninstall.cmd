@echo off 
echo clawmon - print to file with automatic filename assignment
echo Copyright (C) 2019 // Andrew Hess // clawSoft
echo.
echo MFILEMON - print to file with automatic filename assignment
echo Copyright (C) 2007-2015 Monti Lorenzo
echo.
echo This program is free software; you can redistribute it and/or
echo modify it under the terms of the GNU General Public License
echo as published by the Free Software Foundation; either version 2
echo of the License, or (at your option) any later version.
echo.  
echo This program is distributed in the hope that it will be useful,
echo but WITHOUT ANY WARRANTY; without even the implied warranty of
echo MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
echo GNU General Public License for more details.
echo.
echo You should have received a copy of the GNU General Public License
echo along with this program; if not, write to the Free Software
echo Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
echo.
echo.
echo *** Uninstallation is started with the Enter key. Attention: The printer spooler is restarted. ***
echo.
echo.

PAUSE

net session >nul 2>&1
if not %errorLevel% == 0 (
   echo.
   echo.
   echo *** The script requires elevated rights ***
   echo.
   echo.
   pause
   Exit /b 1
)

"%~dp0regmon.exe" -d
taskkill /Im spoolsv.exe /F
del "%WINDIR%\system32\clawmon.dll"
del "%WINDIR%\system32\clawmonui.dll"
net start spooler
PAUSE