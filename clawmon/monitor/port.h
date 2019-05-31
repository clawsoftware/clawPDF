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

#ifndef _PORT_H
#define _PORT_H

#include <tchar.h>
#include <psapi.h>
#include <windows.h>
#include <userenv.h>
#include <LMCons.h>
#include <wchar.h>
#include "pattern.h"
#include "md5.h"
#include "..\common\config.h"
#include "..\common\defs.h"

class CPort
{
private:
	void Initialize();
	void Initialize(LPCWSTR szPortName);
	void Initialize(LPPORTCONFIG pConfig);

public:
	CPort();
	explicit CPort(LPCWSTR szPortName);
	explicit CPort(LPPORTCONFIG pPortConfig);
	virtual ~CPort();
	CPattern* GetPattern() const { return m_pPattern; }
	void SetFilePatternString(LPCWSTR szPattern);
	void SetUserCommandString(LPCWSTR szPattern);
	BOOL BuildCommandLine();
	BOOL StartJob(DWORD nJobId, LPWSTR szJobTitle, LPWSTR szPrinterName);
	DWORD CreateOutputFile();
	BOOL WriteToFile(LPCVOID lpBuffer, DWORD cbBuffer,
		LPDWORD pcbWritten);
	BOOL EndJob();
	void SetConfig(LPPORTCONFIG pConfig);
	DWORD Logon();
	DWORD CreateOutputPath();
	HANDLE get_token_for_user(LPCTSTR pUsername);
	void WriteToIniFile(LPCWSTR section, LPCWSTR key, LPCWSTR value, LPCTSTR path);
	wchar_t* convertCharArrayToLPCWSTR(const char* charArray);
	void WriteControlFile();
	char* CTime();
	void GenerateHash();
	void SetInfPath();
	void SetPsPath();
	void SetFileName();
	void SetHomeDirectory(HANDLE hToken);

public:
	LPCWSTR PortName() const { return m_szPortName; }
	LPCWSTR OutputPath() const { return m_szOutputPath; }
	LPCWSTR ExecPath() const { return m_szExecPath; }
	LPCWSTR FilePattern() const;
	LPCWSTR UserCommandPattern() const;
	BOOL Overwrite() const { return m_bOverwrite; }
	BOOL WaitTermination() const { return m_bWaitTermination; }
	DWORD WaitTimeout() const { return m_dwWaitTimeout; }
	BOOL PipeData() const { return m_bPipeData; }
	BOOL HideProcess() const { return m_bHideProcess; }
	BOOL RunAsPUser() const { return m_bRunAsPUser; }
	LPWSTR PrinterName() const { return m_szPrinterName; }
	DWORD JobId() const { return m_nJobId; }
	LPWSTR JobTitle() const;
	LPCWSTR UserName() const;
	DWORD TotalPages() const;
	DWORD TotalCopies() const;
	LPCWSTR ComputerName() const;
	LPWSTR FileName() const { return (LPWSTR)m_szFileName; }
	LPWSTR Path() const { return (LPWSTR)m_szParent; }
	LPWSTR Bin() const;
	LPCWSTR User() const { return m_szUser; }
	LPCWSTR Domain() const { return m_szDomain; }
	LPCWSTR Password() const { return m_szPassword; }

private:
	typedef struct tagTHREADDATA
	{
		CPort* pPort;
		LPCVOID lpBuffer;
		DWORD cbBuffer;
		LPDWORD pcbWritten;
		BOOL bStatus;
		CRITICAL_SECTION csBuffer;
	} THREADDATA, *LPTHREADDATA;
	static DWORD WINAPI WriteThreadProc(LPVOID lpParam);
	static DWORD WINAPI ReadThreadProc(LPVOID lpParam);
	DWORD RecursiveCreateFolder(LPCWSTR szPath);

private:
	THREADDATA m_threadData;
	HANDLE m_hWriteThread;
	HANDLE m_hWorkEvt;
	HANDLE m_hDoneEvt;
	WCHAR m_szPortName[MAX_PATH + 1];
	WCHAR m_szOutputPath[MAX_PATH + 1];
	WCHAR m_nszOutputPath[MAX_PATH + 1];
	WCHAR m_szExecPath[MAX_PATH + 1];
	LPWSTR m_szPrinterName;
	DWORD m_cchPrinterName;
	CPattern* m_pPattern;
	CPattern* m_pUserCommand;
	BOOL m_bOverwrite;
	//	WCHAR m_szUserCommand[MAXUSERCOMMMAND];
	BOOL m_bWaitTermination;
	DWORD m_dwWaitTimeout;
	BOOL m_bPipeData;
	BOOL m_bPipeActive;
	BOOL m_bHideProcess;
	BOOL m_bRunAsPUser;
	WCHAR m_szFileName[MAX_PATH + 1];
	WCHAR m_nszFileName[MAX_PATH + 1];
	WCHAR m_uniqFileName[MAX_PATH + 1];
	HANDLE m_hFile;
	PROCESS_INFORMATION m_procInfo;
	//	LPWSTR m_szCommandLine;
	DWORD m_nJobId;
	JOB_INFO_1W* m_pJobInfo1;
	JOB_INFO_2W* m_pJobInfo2;
	DWORD m_cbJobInfo1;
	DWORD m_cbJobInfo2;
	BOOL m_bJobIsLocal;
	WCHAR m_szParent[MAX_PATH + 1];
	WCHAR m_szUser[MAX_USER];
	WCHAR m_szDomain[MAX_DOMAIN];
	WCHAR m_szPassword[MAX_PASSWORD];
	HANDLE m_hToken;
	BOOL m_bRestrictedToken;
	BOOL m_bLogonInvalidated;
	WCHAR infpath[MAX_PATH + 1];
	WCHAR pspath[MAX_PATH + 1];
};

#endif
