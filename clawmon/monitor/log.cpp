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
#include "log.h"
#include "port.h"
#include <string.h>
#include <stdarg.h>

static const unsigned short int BOM = 0xFEFF;

CMfmLog* g_pLog = NULL;

CMfmLog::CMfmLog()
	: m_nLogLevel(LOGLEVEL_NONE)
{
	m_hLogFile = CreateFileW(L"clawmon.log", GENERIC_READ | GENERIC_WRITE,
		FILE_SHARE_READ, NULL, CREATE_ALWAYS, 0, NULL);

	if (m_hLogFile != INVALID_HANDLE_VALUE)
	{
		DWORD wri;
		WriteFile(m_hLogFile, &BOM, sizeof(BOM), &wri, NULL);
	}

	InitializeCriticalSection(&m_CSLog);
}

CMfmLog::~CMfmLog()
{
	CloseHandle(m_hLogFile);
	DeleteCriticalSection(&m_CSLog);
}

void CMfmLog::SetLogLevel(DWORD nLevel)
{
	if (nLevel < LOGLEVEL_MIN)
		nLevel = LOGLEVEL_MIN;
	else if (nLevel > LOGLEVEL_MAX)
		nLevel = LOGLEVEL_MAX;

	m_nLogLevel = nLevel;
}

void CMfmLog::Log(DWORD nLevel, CPort* pPort, LPCWSTR szFormat, ...)
{
	if (m_hLogFile != INVALID_HANDLE_VALUE &&
		m_nLogLevel >= nLevel)
	{
		WCHAR szBuf1[MAXLOGLINE];
		WCHAR szBuf2[MAXLOGLINE];
		va_list args;

		va_start(args, szFormat);

		vswprintf_s(szBuf1, LENGTHOF(szBuf1), szFormat, args);

		va_end(args);

		swprintf_s(szBuf2, LENGTHOF(szBuf2), L"%s: %s", pPort->PortName(), szBuf1);

		Log(nLevel, szBuf2);
	}
}

void CMfmLog::Log(DWORD nLevel, LPCWSTR szFormat, ...)
{
	if (m_hLogFile != INVALID_HANDLE_VALUE &&
		m_nLogLevel >= nLevel)
	{
		SYSTEMTIME st;
		WCHAR szBuf[MAXLOGLINE - 25];
		DWORD wri;
		va_list args;

		va_start(args, szFormat);

		vswprintf_s(szBuf, LENGTHOF(szBuf), szFormat, args);

		va_end(args);

		GetLocalTime(&st);

		EnterCriticalSection(&m_CSLog);

		int len = swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), L"%02i/%02i/%04i %02i:%02i:%02i.%03i %s\r\n",
			st.wMonth, st.wDay, st.wYear, st.wHour, st.wMinute, st.wSecond, st.wMilliseconds, szBuf);

		if (len > 0)
		{
			WriteFile(m_hLogFile, m_szBuffer, len * sizeof(WCHAR), &wri, NULL);

			FlushFileBuffers(m_hLogFile);
		}

		LeaveCriticalSection(&m_CSLog);
	}
}