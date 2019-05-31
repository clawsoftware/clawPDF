/*
clawmon - print to file with automatic filename assignment
Copyright (C) 2019 // Andrew Hess // clawSoft

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

#include "stdafx.h"
#include "monitor.h"
#include "pattern.h"
#include "portlist.h"
#include "log.h"
#include "..\common\autoclean.h"
#include "..\common\monutils.h"
#include "..\common\config.h"
#include "..\common\defs.h"

//-------------------------------------------------------------------------------------
typedef struct tagXCVDATA
{
	tagXCVDATA()
	{
		pPort = NULL;
		bDeleting = FALSE;
		GrantedAccess = 0;
	}
	CPort* pPort;
	BOOL bDeleting;
	ACCESS_MASK GrantedAccess;
} XCVDATA, *LPXCVDATA;

//-------------------------------------------------------------------------------------
BOOL WINAPI MfmEnumPorts(HANDLE hMonitor, LPWSTR pName, DWORD Level, LPBYTE pPorts,
	DWORD cbBuf, LPDWORD pcbNeeded, LPDWORD pcReturned)
{
	return g_pPortList->EnumMultiFilePorts(hMonitor, pName, Level, pPorts,
		cbBuf, pcbNeeded, pcReturned);
}

//-------------------------------------------------------------------------------------
BOOL WINAPI MfmOpenPort(HANDLE hMonitor, LPWSTR pName, PHANDLE pHandle)
{
	UNREFERENCED_PARAMETER(hMonitor);

	g_pLog->Log(LOGLEVEL_ALL, L"MfmOpenPort called (%s)", pName);

	CPort* pPort = g_pPortList->FindPort(pName);
	*pHandle = (HANDLE)pPort;
	if (!pPort)
	{
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmOpenPort: can't find port %s", pName);
		SetLastError(ERROR_CAN_NOT_COMPLETE);
		return FALSE;
	}

	DWORD dwErr;
	if ((dwErr = pPort->Logon()) != ERROR_SUCCESS)
	{
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmOpenPort: can't logon user");
		SetLastError(dwErr);
		return FALSE;
	}

	//2009-05-14 we'd better create missing directories rather than giving up..
	//	else if (!DirectoryExists(pPort->szOutputPath))
	DWORD dwRet;
	if ((dwRet = pPort->CreateOutputPath()) != ERROR_SUCCESS)
	{
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmOpenPort: can't create output directory (%i)", dwRet);
		SetLastError(ERROR_DIRECTORY);
		return FALSE;
	}

	g_pLog->Log(LOGLEVEL_ALL, L"MfmOpenPort returning TRUE (%s)", pName);

	return TRUE;
}

//-------------------------------------------------------------------------------------
BOOL WINAPI MfmStartDocPort(HANDLE hPort, LPWSTR pPrinterName, DWORD JobId,
	DWORD Level, LPBYTE pDocInfo)
{
	UNREFERENCED_PARAMETER(Level);

	if (!hPort || !pDocInfo)
	{
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmStartDocPort: invalid parameter (hPort = %X pDocInfo = %X)", hPort, pDocInfo);
		SetLastError(ERROR_CAN_NOT_COMPLETE);
		return FALSE;
	}

	CPort* pPort = (CPort*)hPort;
	DOC_INFO_1W* pdi = (DOC_INFO_1W*)pDocInfo;

	g_pLog->Log(LOGLEVEL_ALL, L"MfmStartDocPort called (%s)", pPort->PortName());

	CAutoCriticalSection acs(g_pPortList->CriticalSection());

	/*set initial job data*/
	if (!pPort->StartJob(JobId, pdi->pDocName, pPrinterName))
	{
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmStartDocPort: can't start print job");
		SetLastError(ERROR_CAN_NOT_COMPLETE);
		return FALSE;
	}

	/*create output file or pipe*/
	DWORD res;
	if ((res = pPort->CreateOutputFile()) != 0)
	{
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmStartDocPort: can't create output file");
		SetLastError(res);
		return FALSE;
	}

	g_pLog->Log(LOGLEVEL_ALL, L"MfmStartDocPort returning TRUE (%s)", pPort->PortName());

	return TRUE;
}

//-------------------------------------------------------------------------------------
BOOL WINAPI MfmWritePort(HANDLE hPort, LPBYTE pBuffer,
	DWORD cbBuf, LPDWORD pcbWritten)
{
	if (!hPort)
	{
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmWritePort: invalid parameter (hPort = %X)", hPort);
		SetLastError(ERROR_CAN_NOT_COMPLETE);
		return FALSE;
	}

	CPort* pPort = (CPort*)hPort;

	g_pLog->Log(LOGLEVEL_ALL, L"MfmWritePort called (%s)", pPort->PortName());

	CAutoCriticalSection acs(g_pPortList->CriticalSection());

	/*write was unsuccessful, tell the spooler to restart and pause job*/
	if (!pPort->WriteToFile(pBuffer, cbBuf, pcbWritten))
	{
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmWritePort: can't write to output file");

		HANDLE hPrinter;

		if (OpenPrinterW(pPort->PrinterName(), &hPrinter, NULL))
		{
			g_pLog->Log(LOGLEVEL_ERRORS, L"MfmWritePort: pausing job %u on %s",
				pPort->JobId(), pPort->PrinterName());
			SetJobW(hPrinter, pPort->JobId(), 0, NULL, JOB_CONTROL_RESTART);
			SetJobW(hPrinter, pPort->JobId(), 0, NULL, JOB_CONTROL_PAUSE);
			ClosePrinter(hPrinter);
		}
		else
		{
			g_pLog->Log(LOGLEVEL_ERRORS, L"MfmWritePort: can't pause job %u on %s",
				pPort->JobId(), pPort->PrinterName());
		}

		return FALSE;
	}

	g_pLog->Log(LOGLEVEL_ALL, L"MfmWritePort returning TRUE (%s)", pPort->PortName());

	return TRUE;
}

//-------------------------------------------------------------------------------------
BOOL WINAPI MfmReadPort(HANDLE hPort, LPBYTE pBuffer,
	DWORD cbBuffer, LPDWORD pcbRead)
{
	UNREFERENCED_PARAMETER(hPort);
	UNREFERENCED_PARAMETER(pBuffer);
	UNREFERENCED_PARAMETER(cbBuffer);
	UNREFERENCED_PARAMETER(pcbRead);

	/*no reading from this port*/
	SetLastError(ERROR_INVALID_HANDLE);
	return FALSE;
}

//-------------------------------------------------------------------------------------
BOOL WINAPI MfmEndDocPort(HANDLE hPort)
{
	if (!hPort)
	{
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmEndDocPort: invalid parameter (hPort = %X)", hPort);
		SetLastError(ERROR_CAN_NOT_COMPLETE);
		return FALSE;
	}

	CPort* pPort = (CPort*)hPort;

	g_pLog->Log(LOGLEVEL_ALL, L"MfmEndDocPort called (%s)", pPort->PortName());

	CAutoCriticalSection acs(g_pPortList->CriticalSection());

	BOOL bRet = pPort->EndJob();

	g_pLog->Log(LOGLEVEL_ALL, L"MfmEndDocPort returning %s (%s)", bRet ? L"TRUE" : L"FALSE", pPort->PortName());

	return bRet;
}

//-------------------------------------------------------------------------------------
BOOL WINAPI MfmClosePort(HANDLE hPort)
{
	UNREFERENCED_PARAMETER(hPort);

	return TRUE;
}

//-------------------------------------------------------------------------------------
BOOL WINAPI MfmXcvOpenPort(HANDLE hMonitor, LPCWSTR pszObject,
	ACCESS_MASK GrantedAccess, PHANDLE phXcv)
{
	UNREFERENCED_PARAMETER(hMonitor);

	g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvOpenPort called (%s), GrantedAccess = %u",
		(LPWSTR)pszObject, GrantedAccess);

	LPXCVDATA pXCVDATA = new XCVDATA;

	*phXcv = (HANDLE)pXCVDATA;

	if (pszObject)
		pXCVDATA->pPort = g_pPortList->FindPort((LPWSTR)pszObject);

	pXCVDATA->GrantedAccess = GrantedAccess;

	g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvOpenPort returning TRUE (%s)", (LPWSTR)pszObject);

	return TRUE;
}

//-------------------------------------------------------------------------------------
DWORD WINAPI MfmXcvDataPort(HANDLE hXcv, LPCWSTR pszDataName, PBYTE pInputData,
	DWORD cbInputData, PBYTE pOutputData, DWORD cbOutputData,
	PDWORD pcbOutputNeeded)
{
	g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvDataPort called (%s)", pszDataName);

	LPXCVDATA pXCVDATA = (LPXCVDATA)hXcv;

	if (wcscmp(pszDataName, L"AddPort") == 0)
	{
		if (pXCVDATA != NULL && pInputData != NULL)
		{
			if (!(pXCVDATA->GrantedAccess & SERVER_ACCESS_ADMINISTER))
			{
				g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort returning ERROR_ACCESS_DENIED (pXCVDATA->GrantedAccess = %X)",
					pXCVDATA->GrantedAccess);
				return ERROR_ACCESS_DENIED;
			}
			pXCVDATA->pPort = new CPort((LPWSTR)pInputData);
			g_pPortList->AddMfmPort(pXCVDATA->pPort);
			g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvDataPort returning ERROR_SUCCESS");
			return ERROR_SUCCESS;
		}
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort: bad arguments (pXCVDATA = %X pInputData = %X)", pXCVDATA, pInputData);
		return ERROR_BAD_ARGUMENTS;
	}
	else if (wcscmp(pszDataName, L"DeletePort") == 0)
	{
		if (pXCVDATA != NULL && pXCVDATA->pPort != NULL)
		{
			if (!(pXCVDATA->GrantedAccess & SERVER_ACCESS_ADMINISTER))
			{
				g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort returning ERROR_ACCESS_DENIED (pXCVDATA->GrantedAccess = %X)",
					pXCVDATA->GrantedAccess);
				return ERROR_ACCESS_DENIED;
			}
			pXCVDATA->bDeleting = TRUE;
			g_pPortList->DeletePort(pXCVDATA->pPort);
			g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvDataPort returning ERROR_SUCCESS");
			return ERROR_SUCCESS;
		}
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort: bad arguments (pXCVDATA = %X pInputData = %X)", pXCVDATA, pInputData);
		return ERROR_BAD_ARGUMENTS;
	}
	else if (wcscmp(pszDataName, L"PortDeleted") == 0)
	{
		if (pXCVDATA != NULL)
		{
			if (!(pXCVDATA->GrantedAccess & SERVER_ACCESS_ADMINISTER))
			{
				g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort returning ERROR_ACCESS_DENIED (pXCVDATA->GrantedAccess = %X)",
					pXCVDATA->GrantedAccess);
				return ERROR_ACCESS_DENIED;
			}
			pXCVDATA->bDeleting = FALSE;
			g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvDataPort returning ERROR_SUCCESS");
			return ERROR_SUCCESS;
		}
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort: bad arguments (pXCVDATA = %X)", pXCVDATA);
		return ERROR_BAD_ARGUMENTS;
	}
	else if (wcscmp(pszDataName, L"PortExists") == 0)
	{
		LPWSTR szPortName = (LPWSTR)pInputData;
		DWORD needed, returned;
		if (EnumPorts(NULL, 1, NULL, 0, &needed, &returned) == 0 &&
			GetLastError() == ERROR_INSUFFICIENT_BUFFER)
		{
			HANDLE hHeap = GetProcessHeap();
			LPBYTE pBuf;
			if ((pBuf = (LPBYTE)HeapAlloc(hHeap, 0, needed)) == NULL)
			{
				g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort: out of memory");
				return ERROR_OUTOFMEMORY;
			}
			if (EnumPorts(NULL, 1, pBuf, needed, &needed, &returned))
			{
				PORT_INFO_1W* pPorts = (PORT_INFO_1W*)pBuf;
				while (returned--)
				{
					if (_wcsicmp(szPortName, pPorts->pName) == 0)
					{
						g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvDataPort: port already exists (%s)", szPortName);
						*((BOOL*)pOutputData) = TRUE;
						break;
					}
					pPorts++;
				}
			}
			HeapFree(hHeap, 0, pBuf);
			g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvDataPort returning ERROR_SUCCESS");
			return ERROR_SUCCESS;
		}
	}
	else if (wcscmp(pszDataName, L"SetConfig") == 0)
	{
		if (cbInputData < sizeof(PORTCONFIG))
		{
			g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort returning ERROR_INSUFFICIENT_BUFFER");
			return ERROR_INSUFFICIENT_BUFFER;
		}
		if (pXCVDATA != NULL && pXCVDATA->pPort != NULL && pInputData != NULL)
		{
			if (!(pXCVDATA->GrantedAccess & SERVER_ACCESS_ADMINISTER))
			{
				g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort returning ERROR_ACCESS_DENIED (pXCVDATA->GrantedAccess = %X)",
					pXCVDATA->GrantedAccess);
				return ERROR_ACCESS_DENIED;
			}
			LPPORTCONFIG ppc = (LPPORTCONFIG)pInputData;
			pXCVDATA->pPort->SetConfig(ppc);
			g_pPortList->SaveToRegistry();
			g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvDataPort returning ERROR_SUCCESS");
			return ERROR_SUCCESS;
		}
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort: bad arguments (pXCVDATA = %X pXCVDATA->pPort = %X pInputData = %X)",
			pXCVDATA, pXCVDATA->pPort, pInputData);
		return ERROR_BAD_ARGUMENTS;
	}
	else if (wcscmp(pszDataName, L"GetConfig") == 0)
	{
		*pcbOutputNeeded = sizeof(PORTCONFIG);
		if (*pcbOutputNeeded > cbOutputData)
		{
			g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort returning ERROR_INSUFFICIENT_BUFFER");
			return ERROR_INSUFFICIENT_BUFFER;
		}
		if (pXCVDATA != NULL && pXCVDATA->pPort != NULL && pOutputData != NULL)
		{
			LPPORTCONFIG ppc = (LPPORTCONFIG)pOutputData;
			wcscpy_s(ppc->szPortName, LENGTHOF(ppc->szPortName), pXCVDATA->pPort->PortName());
			wcscpy_s(ppc->szOutputPath, LENGTHOF(ppc->szOutputPath), pXCVDATA->pPort->OutputPath());
			wcscpy_s(ppc->szFilePattern, LENGTHOF(ppc->szFilePattern), pXCVDATA->pPort->FilePattern());
			ppc->bOverwrite = pXCVDATA->pPort->Overwrite();
			wcscpy_s(ppc->szUserCommandPattern, LENGTHOF(ppc->szUserCommandPattern), pXCVDATA->pPort->UserCommandPattern());
			wcscpy_s(ppc->szExecPath, LENGTHOF(ppc->szExecPath), pXCVDATA->pPort->ExecPath());
			ppc->bWaitTermination = pXCVDATA->pPort->WaitTermination();
			ppc->dwWaitTimeout = pXCVDATA->pPort->WaitTimeout();
			ppc->bPipeData = pXCVDATA->pPort->PipeData();
			ppc->bHideProcess = pXCVDATA->pPort->HideProcess();
			ppc->bRunAsPUser = pXCVDATA->pPort->RunAsPUser();
			ppc->nLogLevel = g_pLog->GetLogLevel();
			wcscpy_s(ppc->szUser, LENGTHOF(ppc->szUser), pXCVDATA->pPort->User());
			wcscpy_s(ppc->szDomain, LENGTHOF(ppc->szDomain), pXCVDATA->pPort->Domain());
			wcscpy_s(ppc->szPassword, LENGTHOF(ppc->szPassword), pXCVDATA->pPort->Password());
			g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvDataPort returning ERROR_SUCCESS");
			return ERROR_SUCCESS;
		}
		g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort: bad arguments (pXCVDATA = %X pXCVDATA->pPort = %X pOutputData = %X)",
			pXCVDATA, pXCVDATA->pPort, pOutputData);
		return ERROR_BAD_ARGUMENTS;
	}
	else if (wcscmp(pszDataName, L"MonitorUI") == 0)
	{
		static WCHAR szUIDLL[] = L"clawmonui.dll";
		*pcbOutputNeeded = sizeof(szUIDLL);
		if (cbOutputData < sizeof(szUIDLL))
		{
			g_pLog->Log(LOGLEVEL_ERRORS, L"MfmXcvDataPort returning ERROR_INSUFFICIENT_BUFFER");
			return ERROR_INSUFFICIENT_BUFFER;
		}
		CopyMemory(pOutputData, szUIDLL, sizeof(szUIDLL));
		g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvDataPort returning ERROR_SUCCESS");
		return ERROR_SUCCESS;
	}
	g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvDataPort returning ERROR_CAN_NOT_COMPLETE");
	return ERROR_CAN_NOT_COMPLETE;
}

//-------------------------------------------------------------------------------------
BOOL WINAPI MfmXcvClosePort(HANDLE hXcv)
{
	LPXCVDATA pXCVDATA = (LPXCVDATA)hXcv;

	g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvClosePort called");

	//in caso di chiamata a XcvDataPort con metodo "DeletePort", si passa di qui 2 volte!
	//la prima volta, imposto, bDeleting = TRUE, così la memoria non viene liberata
	//poi, chiamo di nuovo XcvDataPort con metodo "PortDeleted", che imposta bDeleting = FALSE
	if (pXCVDATA && !pXCVDATA->bDeleting)
		delete pXCVDATA;

	g_pLog->Log(LOGLEVEL_ALL, L"MfmXcvClosePort returning TRUE");

	return TRUE;
}

//-------------------------------------------------------------------------------------
VOID WINAPI MfmShutdown(HANDLE hMonitor)
{
	UNREFERENCED_PARAMETER(hMonitor);
}

//-------------------------------------------------------------------------------------
LPMONITOR2 WINAPI InitializePrintMonitor2(PMONITORINIT pMonitorInit, PHANDLE phMonitor)
{
	UNREFERENCED_PARAMETER(phMonitor);

	static MONITOR2 themon;

	if (!pMonitorInit->bLocal)
	{
		g_pLog->Log(LOGLEVEL_ERRORS, L"InitializePrintMonitor2: can't work on clusters");
		return NULL;
	}

	ZeroMemory(&themon, sizeof(MONITOR2));

	//this is completely useless.
	//if we were on the wrong processor, spooler wouldn't have loaded us
	/*
	if (!Is_CorrectProcessorArchitecture())
	{
		g_pLog->Log(LOGLEVEL_ERRORS, L"InitializePrintMonitor2: running on wrong processor");
		return NULL;
	}
	*/

	if (Is_Win2000())
	{
		g_pLog->Log(LOGLEVEL_ALL, L"clawmon is running on Windows 2000");
		themon.cbSize = MONITOR2_SIZE_WIN2K;
	}
	else if (Is_WinXPOrAbove() /*Is_WinXP() || Is_Win2003() || Is_WinVista() || Is_Win2008() || Is_Win7()*/)
	{
		g_pLog->Log(LOGLEVEL_ALL, L"clawmon is running on Windows XP or above");
		themon.cbSize = sizeof(MONITOR2);
	}
	else
	{
		g_pLog->Log(LOGLEVEL_ERRORS, L"InitializePrintMonitor2: can't determine OS version");
		return NULL;
	}

	themon.pfnEnumPorts = MfmEnumPorts;
	themon.pfnOpenPort = MfmOpenPort;
	themon.pfnStartDocPort = MfmStartDocPort;
	themon.pfnWritePort = MfmWritePort;
	themon.pfnReadPort = MfmReadPort;
	themon.pfnEndDocPort = MfmEndDocPort;
	themon.pfnClosePort = MfmClosePort;
	themon.pfnXcvOpenPort = MfmXcvOpenPort;
	themon.pfnXcvDataPort = MfmXcvDataPort;
	themon.pfnXcvClosePort = MfmXcvClosePort;
	themon.pfnShutdown = MfmShutdown;

	g_pMonitorInit = pMonitorInit;

	_ASSERTE(g_pPortList != NULL);

	g_pPortList->LoadFromRegistry();

	g_pLog->Log(LOGLEVEL_ALL, L"InitializePrintMonitor2 successfully initialized clawmon");

	return &themon;
}

//-------------------------------------------------------------------------------------
BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD dwReason, LPVOID lpvReserved)
{
	UNREFERENCED_PARAMETER(lpvReserved);
	UNREFERENCED_PARAMETER(hinstDLL);

	switch (dwReason)
	{
	case DLL_PROCESS_ATTACH:
#ifdef _DEBUG
		//Steady, ready, go. You have 20 seconds to attach your debugger to spoolsv
		Sleep(20000);
#endif
		// see here http://msdn.microsoft.com/en-us/library/ms682659%28v=vs.85%29.aspx
		// why the following call should not be done
		//		DisableThreadLibraryCalls(hinstDLL);
		g_pLog = new CMfmLog();
		g_pLog->Log(LOGLEVEL_NONE, L"*** clawmon log start ***");
		g_pPortList = new CPortList(szMonitorName, szDescription);
#ifdef _DEBUG
		//Force max log level in debug mode
		g_pLog->SetLogLevel(LOGLEVEL_ALL);
#else
		//Show only errors by default. We'll load the wanted log level from the registry
		g_pLog->SetLogLevel(LOGLEVEL_ERRORS);
#endif
		break;
	case DLL_PROCESS_DETACH:
		delete g_pPortList;
		g_pLog->Log(LOGLEVEL_NONE, L"*** clawmon log end ***");
		delete g_pLog;
		break;
	}

	return TRUE;
}