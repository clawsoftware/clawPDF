/*
MFILEMON - print to file with automatic filename assignment
Copyright (C) 2007-2015 Monti Lorenzo

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

#ifndef _MONITORUI_H
#define _MONITORUI_H

BOOL WINAPI MfmAddPortUI(PCWSTR pszServer, HWND hWnd, PCWSTR pszMonitorNameIn,
	PWSTR* ppszPortNameOut);

BOOL WINAPI MfmConfigurePortUI(PCWSTR pszServer, HWND hWnd, PCWSTR pszPortName);

BOOL WINAPI MfmDeletePortUI(PCWSTR pszServer, HWND hWnd, PCWSTR pszPortName);

extern HINSTANCE g_hInstance;

#endif
