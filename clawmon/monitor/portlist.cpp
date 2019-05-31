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
#include "portlist.h"
#include "pattern.h"
#include "log.h"
#include "..\common\autoclean.h"
#include "..\common\monutils.h"
#include <openssl\evp.h>

CPortList* g_pPortList = NULL;
LPCWSTR CPortList::szOutputPathKey = L"OutputPath";
LPCWSTR CPortList::szFilePatternKey = L"FilePattern";
LPCWSTR CPortList::szOverwriteKey = L"Overwrite";
LPCWSTR CPortList::szUserCommandPatternKey = L"UserCommand";
LPCWSTR CPortList::szExecPathKey = L"ExecPath";
LPCWSTR CPortList::szWaitTerminationKey = L"WaitTermination";
LPCWSTR CPortList::szWaitTimeoutKey = L"WaitTimeout";
LPCWSTR CPortList::szPipeDataKey = L"PipeData";
LPCWSTR CPortList::szLogLevelKey = L"LogLevel";
LPCWSTR CPortList::szUserKey = L"User";
LPCWSTR CPortList::szDomainKey = L"Domain";
LPCWSTR CPortList::szPasswordKey = L"Password";
LPCWSTR CPortList::szHideProcessKey = L"HideProcess";
LPCWSTR CPortList::szRunAsPUserProcessKey = L"RunAsPUser";

static BYTE aeskey[] = {
	0x73, 0xb6, 0x45, 0x0c, 0x24, 0xc9, 0xfe, 0x6b, 0x74, 0xf8, 0xc2, 0xbe, 0x94, 0xd4, 0xdf, 0xd4,
	0x40, 0xd3, 0xdd, 0xdd, 0x8e, 0xa3, 0x2a, 0x56, 0x89, 0x3c, 0x75, 0xd8, 0x45, 0xb7, 0xb9, 0x34,
};

//-------------------------------------------------------------------------------------
CPortList::CPortList(LPCWSTR szMonitorName, LPCWSTR szPortDesc)
{
	InitializeCriticalSection(&m_CSPortList);
	wcscpy_s(m_szMonitorName, LENGTHOF(m_szMonitorName), szMonitorName);
	wcscpy_s(m_szPortDesc, LENGTHOF(m_szPortDesc), szPortDesc);
	m_pFirstPortRec = NULL;
	srand((unsigned)time(NULL));
}

//-------------------------------------------------------------------------------------
CPortList::~CPortList()
{
	LPPORTREC pNext = NULL;

	while (m_pFirstPortRec)
	{
		pNext = m_pFirstPortRec->m_pNext;
		delete m_pFirstPortRec;
		m_pFirstPortRec = pNext;
	}

	DeleteCriticalSection(&m_CSPortList);
}

//-------------------------------------------------------------------------------------
CPort* CPortList::FindPort(LPCWSTR szPortName)
{
	CAutoCriticalSection acs(CriticalSection());

	LPPORTREC pPortRec = m_pFirstPortRec;

	while (pPortRec)
	{
		if (_wcsicmp(pPortRec->m_pPort->PortName(), szPortName) == 0)
			break;
		pPortRec = pPortRec->m_pNext;
	}

	return pPortRec
		? pPortRec->m_pPort
		: NULL;
}

//-------------------------------------------------------------------------------------
DWORD CPortList::GetPortSize(LPCWSTR szPortName, DWORD dwLevel)
{
	DWORD cb = 0;

	switch (dwLevel)
	{
	case 1:
		cb = sizeof(PORT_INFO_1W) +
			sizeof(WCHAR) * ((DWORD)wcslen(szPortName) + 1);
		break;
	case 2:
		cb = sizeof(PORT_INFO_2W) +
			sizeof(WCHAR) * (
			(DWORD)wcslen(szPortName) + 1 +
				(DWORD)wcslen(m_szMonitorName) + 1 +
				(DWORD)wcslen(m_szPortDesc) + 1);
		break;
	default:
		break;
	}

	return cb;
}

//-------------------------------------------------------------------------------------
LPBYTE CPortList::CopyPortToBuffer(CPort* pPort, DWORD dwLevel, LPBYTE pStart, LPBYTE pEnd)
{
	size_t len = 0;

	switch (dwLevel)
	{
	case 1:
	{
		PORT_INFO_1W* pPortInfo = (PORT_INFO_1W*)pStart;
		len = wcslen(pPort->PortName()) + 1;
		pEnd -= len * sizeof(WCHAR);
		wcscpy_s((LPWSTR)pEnd, len, pPort->PortName());
		pPortInfo->pName = (LPWSTR)pEnd;
		break;
	}
	case 2:
	{
		PORT_INFO_2W* pPortInfo = (PORT_INFO_2W*)pStart;
		len = wcslen(m_szMonitorName) + 1;
		pEnd -= len * sizeof(WCHAR);
		wcscpy_s((LPWSTR)pEnd, len, m_szMonitorName);
		pPortInfo->pMonitorName = (LPWSTR)pEnd;

		len = wcslen(m_szPortDesc) + 1;
		pEnd -= len * sizeof(WCHAR);
		wcscpy_s((LPWSTR)pEnd, len, m_szPortDesc);
		pPortInfo->pDescription = (LPWSTR)pEnd;

		len = wcslen(pPort->PortName()) + 1;
		pEnd -= len * sizeof(WCHAR);
		wcscpy_s((LPWSTR)pEnd, len, pPort->PortName());
		pPortInfo->pPortName = (LPWSTR)pEnd;

		pPortInfo->fPortType = 0;
		pPortInfo->Reserved = 0;
		break;
	}
	default:
		break;
	}

	return pEnd;
}

//-------------------------------------------------------------------------------------
BOOL CPortList::EnumMultiFilePorts(HANDLE hMonitor, LPCWSTR pName, DWORD Level, LPBYTE pPorts,
	DWORD cbBuf, LPDWORD pcbNeeded, LPDWORD pcReturned)
{
	UNREFERENCED_PARAMETER(pName);
	UNREFERENCED_PARAMETER(hMonitor);

	CAutoCriticalSection acs(CriticalSection());

	LPPORTREC pPortRec = m_pFirstPortRec;

	DWORD cb = 0;
	while (pPortRec)
	{
		cb += GetPortSize(pPortRec->m_pPort->PortName(), Level);
		pPortRec = pPortRec->m_pNext;
	}

	*pcbNeeded = cb;

	if (cbBuf < *pcbNeeded)
	{
		SetLastError(ERROR_INSUFFICIENT_BUFFER);
		return FALSE;
	}

	LPBYTE pEnd = pPorts + cbBuf;
	*pcReturned = 0;
	pPortRec = m_pFirstPortRec;
	while (pPortRec)
	{
		pEnd = CopyPortToBuffer(pPortRec->m_pPort, Level, pPorts, pEnd);
		switch (Level)
		{
		case 1:
			pPorts += sizeof(PORT_INFO_1W);
			break;
		case 2:
			pPorts += sizeof(PORT_INFO_2W);
			break;
		default:
		{
			SetLastError(ERROR_INVALID_LEVEL);
			return FALSE;
		}
		}
		(*pcReturned)++;

		pPortRec = pPortRec->m_pNext;
	}

	return TRUE;
}

//-------------------------------------------------------------------------------------
void CPortList::AddMfmPort(LPPORTCONFIG pConfig)
{
	//alloc port on the heap
	CPort* pNewPort = new CPort(pConfig);

	//add it to the list
	AddMfmPort(pNewPort);
}

//-------------------------------------------------------------------------------------
void CPortList::AddMfmPort(CPort* pNewPort)
{
	CAutoCriticalSection acs(CriticalSection());

	LPPORTREC pPortRec = new PORTREC;

	pPortRec->m_pPort = pNewPort;
	pPortRec->m_pNext = m_pFirstPortRec;
	m_pFirstPortRec = pPortRec;

	g_pLog->Log(LOGLEVEL_ALL, L"port %s up and running", pNewPort->PortName());
}

//-------------------------------------------------------------------------------------
void CPortList::DeletePort(CPort* pPortToDelete)
{
	CAutoCriticalSection acs(CriticalSection());

	LPPORTREC pPortRec = m_pFirstPortRec, pPrevious = NULL;

	while (pPortRec)
	{
		if (pPortRec->m_pPort == pPortToDelete)
		{
			g_pLog->Log(LOGLEVEL_ALL, pPortToDelete, L"removing port");

			if (pPrevious)
				pPrevious->m_pNext = pPortRec->m_pNext;
			else
				m_pFirstPortRec = pPortRec->m_pNext;

			RemoveFromRegistry(pPortToDelete);

			delete pPortRec;

			break;
		}

		pPrevious = pPortRec;
		pPortRec = pPortRec->m_pNext;
	}
}

//-------------------------------------------------------------------------------------
void CPortList::RemoveFromRegistry(CPort* pPort)
{
	PMONITORREG pReg = g_pMonitorInit->pMonitorReg;
	HKEY hRoot = (HKEY)g_pMonitorInit->hckRegistryRoot;

	//If we're on an UAC enabled system, we're running under unprivileged
	//user account. Let's revert to ourselves for a while...
	HANDLE hToken = NULL;
	if (IsUACEnabled())
	{
		g_pLog->Log(LOGLEVEL_ALL, L"running on UAC enabled OS, reverting to local system");
		OpenThreadToken(GetCurrentThread(), TOKEN_IMPERSONATE, TRUE, &hToken);
		RevertToSelf();
	}

	pReg->fpDeleteKey(hRoot, pPort->PortName(), g_pMonitorInit->hSpooler);

	//let's revert to unprivileged user
	if (hToken)
	{
		SetThreadToken(NULL, hToken);
		CloseHandle(hToken);
		g_pLog->Log(LOGLEVEL_ALL, L"back to unprivileged user");
	}
}

//-------------------------------------------------------------------------------------
void CPortList::LoadFromRegistry()
{
	LPPORTCONFIG pConfig = new PORTCONFIG;
	LPBYTE pwBlob = new BYTE[MAX_PWBLOB];

	/*
	LPWSTR szPortName = new WCHAR[MAX_PATH + 1];
	LPWSTR szOutputPath = new WCHAR[MAX_PATH + 1];
	LPWSTR szFilePattern = new WCHAR[MAX_PATH + 1];
	LPWSTR szUserCommandPattern = new WCHAR[MAX_USERCOMMMAND];
	LPWSTR szExecPath = new WCHAR[MAX_PATH + 1];
	LPWSTR szUser = new WCHAR[MAX_USER];
	LPWSTR szDomain = new WCHAR[MAX_DOMAIN];
	LPWSTR szPassword = new WCHAR[MAX_PASSWORD];
	BOOL bOverwrite;
	BOOL bWaitTermination;
	DWORD dwWaitTimeout;
	BOOL bPipeData;
	BOOL bHideProcess;
	*/
#ifdef __GNUC__
	HANDLE hKey;
#else
	HKEY hKey;
#endif
	PMONITORREG pReg = g_pMonitorInit->pMonitorReg;
	HKEY hRoot = (HKEY)g_pMonitorInit->hckRegistryRoot;
	DWORD index = 0;
	DWORD cchName;
	DWORD cbData;

#ifndef _DEBUG
	DWORD nLogLevel;

	cbData = sizeof(nLogLevel);
	if (pReg->fpQueryValue(hRoot, szLogLevelKey, NULL, (LPBYTE)&nLogLevel, &cbData,
		g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
		nLogLevel = LOGLEVEL_NONE;

	g_pLog->SetLogLevel(nLogLevel);
#endif

	for (;;)
	{
		//read port name
		cchName = LENGTHOF(pConfig->szPortName);
		LONG res = pReg->fpEnumKey(hRoot, index++, pConfig->szPortName, &cchName, NULL, g_pMonitorInit->hSpooler);
		if (res == ERROR_NO_MORE_ITEMS)
			break;
		else if (res == ERROR_MORE_DATA)
			continue;

		//open port registry key
		if (pReg->fpOpenKey(hRoot, pConfig->szPortName, KEY_QUERY_VALUE, &hKey, g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			continue;

		//read OutputPath
		cbData = sizeof(pConfig->szOutputPath);
		if (pReg->fpQueryValue(hKey, szOutputPathKey, NULL, (LPBYTE)pConfig->szOutputPath, &cbData,
			g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			continue;
		else
			pConfig->szOutputPath[cbData / sizeof(WCHAR)] = L'\0';

		//read FilePattern
		cbData = sizeof(pConfig->szFilePattern);
		if (pReg->fpQueryValue(hKey, szFilePatternKey, NULL, (LPBYTE)pConfig->szFilePattern, &cbData,
			g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			continue;
		else
			pConfig->szFilePattern[cbData / sizeof(WCHAR)] = L'\0';
		if (cbData == 0)
			wcscpy_s(pConfig->szFilePattern, MAX_PATH + 1, CPattern::szDefaultFilePattern);

		//read Overwrite
		cbData = sizeof(pConfig->bOverwrite);
		if (pReg->fpQueryValue(hKey, szOverwriteKey, NULL, (LPBYTE)&pConfig->bOverwrite, &cbData,
			g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			continue;

		//read UserCommand
		cbData = sizeof(pConfig->szUserCommandPattern);
		if (pReg->fpQueryValue(hKey, szUserCommandPatternKey, NULL, (LPBYTE)pConfig->szUserCommandPattern,
			&cbData, g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			*pConfig->szUserCommandPattern = L'\0';
		else
			pConfig->szUserCommandPattern[cbData / sizeof(WCHAR)] = L'\0';

		//read ExecPath
		cbData = sizeof(pConfig->szExecPath);
		if (pReg->fpQueryValue(hKey, szExecPathKey, NULL, (LPBYTE)pConfig->szExecPath, &cbData,
			g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			*pConfig->szExecPath = L'\0';
		else
			pConfig->szExecPath[cbData / sizeof(WCHAR)] = L'\0';

		//read Wait termination
		cbData = sizeof(pConfig->bWaitTermination);
		if (pReg->fpQueryValue(hKey, szWaitTerminationKey, NULL, (LPBYTE)&pConfig->bWaitTermination, &cbData,
			g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			pConfig->bWaitTermination = FALSE;

		//read Wait timeout
		cbData = sizeof(pConfig->dwWaitTimeout);
		if (pReg->fpQueryValue(hKey, szWaitTimeoutKey, NULL, (LPBYTE)&pConfig->dwWaitTimeout, &cbData,
			g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			pConfig->dwWaitTimeout = 10;

		//read Pipe data
		cbData = sizeof(pConfig->bPipeData);
		if (pReg->fpQueryValue(hKey, szPipeDataKey, NULL, (LPBYTE)&pConfig->bPipeData, &cbData,
			g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			pConfig->bPipeData = FALSE;

		//read Hide process
		cbData = sizeof(pConfig->bHideProcess);
		if (pReg->fpQueryValue(hKey, szHideProcessKey, NULL, (LPBYTE)&pConfig->bHideProcess, &cbData,
			g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			pConfig->bHideProcess = TRUE;

		//read Run as printing user
		cbData = sizeof(pConfig->bRunAsPUser);
		if (pReg->fpQueryValue(hKey, szRunAsPUserProcessKey, NULL, (LPBYTE)&pConfig->bRunAsPUser, &cbData,
			g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			pConfig->bRunAsPUser = TRUE;

		//read User
		cbData = sizeof(pConfig->szUser);
		if (pReg->fpQueryValue(hKey, szUserKey, NULL, (LPBYTE)pConfig->szUser, &cbData,
			g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			*pConfig->szUser = L'\0';
		else
			pConfig->szUser[cbData / sizeof(WCHAR)] = L'\0';

		Trim(pConfig->szUser);

		//read Domain
		cbData = sizeof(pConfig->szDomain);
		if (pReg->fpQueryValue(hKey, szDomainKey, NULL, (LPBYTE)pConfig->szDomain, &cbData,
			g_pMonitorInit->hSpooler) != ERROR_SUCCESS)
			*pConfig->szDomain = L'\0';
		else
			pConfig->szDomain[cbData / sizeof(WCHAR)] = L'\0';

		Trim(pConfig->szDomain);

		//read Password
		cbData = MAX_PWBLOB;
		if (pReg->fpQueryValue(hKey, szPasswordKey, NULL, (LPBYTE)pwBlob, &cbData,
			g_pMonitorInit->hSpooler) != ERROR_SUCCESS || cbData < 32)
			*pConfig->szPassword = L'\0';
		else
		{
			LPBYTE iv = pwBlob;
			LPBYTE pwData = pwBlob + 16;
			EVP_CIPHER_CTX ctx;
			int outlen1, outlen2;

			if (EVP_DecryptInit(&ctx, EVP_aes_256_cbc(), aeskey, iv) &&
				EVP_DecryptUpdate(&ctx, (LPBYTE)pConfig->szPassword, &outlen1, pwData, cbData - 16) &&
				EVP_DecryptFinal(&ctx, (LPBYTE)pConfig->szPassword + outlen1, &outlen2))
			{
				EVP_CIPHER_CTX_cleanup(&ctx);

				int len = (outlen1 + outlen2) / sizeof(WCHAR);

				if (len == 0)
					len = 1;

				pConfig->szPassword[len - 1] = L'\0';
			}
			else
			{
				*pConfig->szPassword = L'\0';
			}
		}

		//close registry
		pReg->fpCloseKey(hKey, g_pMonitorInit->hSpooler);

		//add the port
		AddMfmPort(pConfig);
	}

	delete[] pwBlob;
	delete pConfig;
}

//-------------------------------------------------------------------------------------
void CPortList::SaveToRegistry()
{
#ifdef __GNUC__
	HANDLE hKey;
#else
	HKEY hKey;
#endif
	PMONITORREG pReg = g_pMonitorInit->pMonitorReg;
	HKEY hRoot = (HKEY)g_pMonitorInit->hckRegistryRoot;
	LPBYTE pwBlob = new BYTE[MAX_PWBLOB];

	CAutoCriticalSection acs(CriticalSection());

	LPPORTREC pPortRec = m_pFirstPortRec;

	//If we're on an UAC enabled system, we're running under unprivileged
	//user account. Let's revert to ourselves for a while...
	HANDLE hToken = NULL;
	if (IsUACEnabled())
	{
		g_pLog->Log(LOGLEVEL_ALL, L"running on UAC enabled OS, reverting to local system");
		OpenThreadToken(GetCurrentThread(), TOKEN_IMPERSONATE, TRUE, &hToken);
		RevertToSelf();
	}

#ifndef _DEBUG
	DWORD nLogLevel = g_pLog->GetLogLevel();
	pReg->fpSetValue(hRoot, szLogLevelKey, REG_DWORD, (LPBYTE)&nLogLevel, sizeof(nLogLevel),
		g_pMonitorInit->hSpooler);
#endif

	while (pPortRec)
	{
		if (pReg->fpCreateKey(hRoot, pPortRec->m_pPort->PortName(), 0, KEY_WRITE,
			NULL, &hKey, NULL, g_pMonitorInit->hSpooler) == ERROR_SUCCESS)
		{
			LPCWSTR szBuf;

			//OutputPath
			szBuf = pPortRec->m_pPort->OutputPath();
			pReg->fpSetValue(hKey, szOutputPathKey, REG_SZ, (LPBYTE)szBuf,
				(DWORD)wcslen(szBuf) * sizeof(WCHAR), g_pMonitorInit->hSpooler);

			//FilePattern
			szBuf = pPortRec->m_pPort->FilePattern();
			pReg->fpSetValue(hKey, szFilePatternKey, REG_SZ, (LPBYTE)szBuf,
				(DWORD)wcslen(szBuf) * sizeof(WCHAR), g_pMonitorInit->hSpooler);

			//Overwrite
			BOOL bOverwrite = pPortRec->m_pPort->Overwrite();
			pReg->fpSetValue(hKey, szOverwriteKey, REG_DWORD, (LPBYTE)&bOverwrite,
				sizeof(bOverwrite), g_pMonitorInit->hSpooler);

			//UserCommand
			szBuf = pPortRec->m_pPort->UserCommandPattern();
			pReg->fpSetValue(hKey, szUserCommandPatternKey, REG_SZ, (LPBYTE)szBuf,
				(DWORD)wcslen(szBuf) * sizeof(WCHAR), g_pMonitorInit->hSpooler);

			//OutputPath
			szBuf = pPortRec->m_pPort->ExecPath();
			pReg->fpSetValue(hKey, szExecPathKey, REG_SZ, (LPBYTE)szBuf,
				(DWORD)wcslen(szBuf) * sizeof(WCHAR), g_pMonitorInit->hSpooler);

			//Wait termination
			BOOL bWaitTermination = pPortRec->m_pPort->WaitTermination();
			pReg->fpSetValue(hKey, szWaitTerminationKey, REG_DWORD, (LPBYTE)&bWaitTermination,
				sizeof(bWaitTermination), g_pMonitorInit->hSpooler);

			//Wait timeout
			DWORD dwWaitTimeout = pPortRec->m_pPort->WaitTimeout();
			pReg->fpSetValue(hKey, szWaitTimeoutKey, REG_DWORD, (LPBYTE)&dwWaitTimeout,
				sizeof(dwWaitTimeout), g_pMonitorInit->hSpooler);

			//Pipe data
			BOOL bPipeData = pPortRec->m_pPort->PipeData();
			pReg->fpSetValue(hKey, szPipeDataKey, REG_DWORD, (LPBYTE)&bPipeData,
				sizeof(bPipeData), g_pMonitorInit->hSpooler);

			//Hide process
			BOOL bHideProcess = pPortRec->m_pPort->HideProcess();
			pReg->fpSetValue(hKey, szHideProcessKey, REG_DWORD, (LPBYTE)&bHideProcess,
				sizeof(bHideProcess), g_pMonitorInit->hSpooler);

			//Run as printing user
			BOOL bRunAsPUser = pPortRec->m_pPort->RunAsPUser();
			pReg->fpSetValue(hKey, szRunAsPUserProcessKey, REG_DWORD, (LPBYTE)&bRunAsPUser,
				sizeof(bRunAsPUser), g_pMonitorInit->hSpooler);

			//User
			szBuf = pPortRec->m_pPort->User();
			pReg->fpSetValue(hKey, szUserKey, REG_SZ, (LPBYTE)szBuf,
				(DWORD)wcslen(szBuf) * sizeof(WCHAR), g_pMonitorInit->hSpooler);

			//Domain
			szBuf = pPortRec->m_pPort->Domain();
			pReg->fpSetValue(hKey, szDomainKey, REG_SZ, (LPBYTE)szBuf,
				(DWORD)wcslen(szBuf) * sizeof(WCHAR), g_pMonitorInit->hSpooler);

			//Password
			LPBYTE iv = pwBlob;
			LPBYTE pData = pwBlob + 16;
			EVP_CIPHER_CTX ctx;
			int outlen1 = 0, outlen2 = 0;
			int len = (int)(wcslen(pPortRec->m_pPort->Password()) + 1) * sizeof(WCHAR);

			for (int n = 0; n < 16; n++)
				iv[n] = rand() % 256;

			if (EVP_EncryptInit(&ctx, EVP_aes_256_cbc(), aeskey, iv) &&
				EVP_EncryptUpdate(&ctx, pData, &outlen1, (LPBYTE)pPortRec->m_pPort->Password(), len) &&
				EVP_EncryptFinal(&ctx, pData + outlen1, &outlen2))
			{
				EVP_CIPHER_CTX_cleanup(&ctx);

				int len = 16 + outlen1 + outlen2;

				pReg->fpSetValue(hKey, szPasswordKey, REG_BINARY, pwBlob, len, g_pMonitorInit->hSpooler);
			}
			else
				pReg->fpSetValue(hKey, szPasswordKey, REG_BINARY, pwBlob, 0, g_pMonitorInit->hSpooler);

			//close registry
			pReg->fpCloseKey(hKey, g_pMonitorInit->hSpooler);
		}

		pPortRec = pPortRec->m_pNext;
	}

	delete[] pwBlob;

	//let's revert to unprivileged user
	if (hToken)
	{
		SetThreadToken(NULL, hToken);
		CloseHandle(hToken);
		g_pLog->Log(LOGLEVEL_ALL, L"back to unprivileged user");
	}
}