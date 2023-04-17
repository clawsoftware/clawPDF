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

#ifndef __LOG_H
#define __LOG_H

#include <windows.h>

#define MAXLOGLINE 2048

#define LOGLEVEL_NONE		0
#define LOGLEVEL_ERRORS		1
#define LOGLEVEL_WARNINGS	2
#define LOGLEVEL_ALL		3
#define LOGLEVEL_MIN		LOGLEVEL_NONE
#define LOGLEVEL_MAX		LOGLEVEL_ALL

class CPort;

class CMfmLog
{
public:
	CMfmLog();
	virtual ~CMfmLog();

public:
	void Log(DWORD nLevel, LPCWSTR szFormat, ...);
	void Log(DWORD nLevel, CPort* pPort, LPCWSTR szFormat, ...);
	void SetLogLevel(DWORD nLevel);
	DWORD GetLogLevel() const { return m_nLogLevel; }

private:
	DWORD m_nLogLevel;
	HANDLE m_hLogFile;
	WCHAR m_szBuffer[MAXLOGLINE];
	CRITICAL_SECTION m_CSLog;
};

extern CMfmLog* g_pLog;

#endif
