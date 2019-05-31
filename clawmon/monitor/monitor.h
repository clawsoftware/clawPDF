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

#ifndef _MONITOR_H
#define _MONITOR_H

//funzioni del port monitor. I membri di MONITOR2 punteranno a queste funzioni

BOOL WINAPI MfmEnumPorts(HANDLE hMonitor, LPWSTR pName, DWORD Level, LPBYTE pPorts,
	DWORD cbBuf, LPDWORD pcbNeeded, LPDWORD pcReturned);

BOOL WINAPI MfmOpenPort(HANDLE hMonitor, LPWSTR pName, PHANDLE pHandle);

BOOL WINAPI MfmStartDocPort(HANDLE hPort, LPWSTR pPrinterName, DWORD JobId,
	DWORD Level, LPBYTE pDocInfo);

BOOL WINAPI MfmWritePort(HANDLE hPort, LPBYTE pBuffer,
	DWORD cbBuf, LPDWORD pcbWritten);

BOOL WINAPI MfmReadPort(HANDLE hPort, LPBYTE pBuffer,
	DWORD cbBuffer, LPDWORD pcbRead);

BOOL WINAPI MfmEndDocPort(HANDLE hPort);

BOOL WINAPI MfmClosePort(HANDLE hPort);

BOOL WINAPI MfmXcvOpenPort(HANDLE hMonitor, LPCWSTR pszObject,
	ACCESS_MASK GrantedAccess, PHANDLE phXcv);

DWORD WINAPI MfmXcvDataPort(HANDLE hXcv, LPCWSTR pszDataName, PBYTE pInputData,
	DWORD cbInputData, PBYTE pOutputData, DWORD cbOutputData,
	PDWORD pcbOutputNeeded);

BOOL WINAPI MfmXcvClosePort(HANDLE hXcv);

VOID WINAPI MfmShutdown(HANDLE hMonitor);

LPMONITOR2 WINAPI InitializePrintMonitor2(PMONITORINIT pMonitorInit, PHANDLE phMonitor);

extern "C"
{
	BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD dwReason, LPVOID lpvReserved);
}

#endif