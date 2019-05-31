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
#include "monutils.h"

/*
//-------------------------------------------------------------------------------------
BOOL Is_CorrectProcessorArchitecture()
{
	SYSTEM_INFO si = {0};

	GetSystemInfo(&si);

#if defined(_X86_)
	return si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_INTEL;
#elif defined(_AMD64_)
	return si.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_AMD64;
#else
	return FALSE;
#endif
}
*/

//-------------------------------------------------------------------------------------
BOOL Is_Win2000()
{
	OSVERSIONINFOEXW osvi = { 0 };

	osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEXW);

	GetVersionExW((LPOSVERSIONINFOW)&osvi);

	return (
		osvi.dwMajorVersion == 5 &&
		osvi.dwMinorVersion == 0
		);
}

//-------------------------------------------------------------------------------------
BOOL Is_WinXP()
{
	OSVERSIONINFOEXW osvi = { 0 };

	osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEXW);

	GetVersionExW((LPOSVERSIONINFOW)&osvi);

	return (
		osvi.dwMajorVersion == 5 && (
			osvi.dwMinorVersion == 1 ||
			(osvi.dwMinorVersion == 2 && osvi.wProductType == VER_NT_WORKSTATION))
		);
}

//-------------------------------------------------------------------------------------
BOOL Is_WinXPOrAbove()
{
	OSVERSIONINFOEXW osvi = { 0 };

	osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEXW);

	GetVersionExW((LPOSVERSIONINFOW)&osvi);

	return (
		osvi.dwMajorVersion > 5 || (
			osvi.dwMajorVersion == 5 &&
			osvi.dwMinorVersion >= 1)
		);
}

//-------------------------------------------------------------------------------------
BOOL Is_Win2003()
{
	OSVERSIONINFOEXW osvi = { 0 };

	osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEXW);

	GetVersionExW((LPOSVERSIONINFOW)&osvi);

	return (
		osvi.dwMajorVersion == 5 &&
		osvi.dwMinorVersion == 2 &&
		osvi.wProductType != VER_NT_WORKSTATION
		);
}

//-------------------------------------------------------------------------------------
BOOL Is_WinVista()
{
	OSVERSIONINFOEXW osvi = { 0 };

	osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEXW);

	GetVersionExW((LPOSVERSIONINFOW)&osvi);

	return (
		osvi.dwMajorVersion == 6 &&
		osvi.dwMinorVersion == 0 &&
		osvi.wProductType == VER_NT_WORKSTATION
		);
}

//-------------------------------------------------------------------------------------
BOOL Is_WinVistaOrAbove()
{
	OSVERSIONINFOEXW osvi = { 0 };

	osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEXW);

	GetVersionExW((LPOSVERSIONINFOW)&osvi);

	return (osvi.dwMajorVersion >= 6);
}

//-------------------------------------------------------------------------------------
BOOL Is_Win2008()
{
	OSVERSIONINFOEXW osvi = { 0 };

	osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEXW);

	GetVersionExW((LPOSVERSIONINFOW)&osvi);

	return (
		osvi.dwMajorVersion == 6 &&
		(osvi.dwMinorVersion == 0 || osvi.dwMinorVersion == 1) &&
		osvi.wProductType != VER_NT_WORKSTATION
		);
}

//-------------------------------------------------------------------------------------
BOOL Is_Win7()
{
	OSVERSIONINFOEXW osvi = { 0 };

	osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEXW);

	GetVersionExW((LPOSVERSIONINFOW)&osvi);

	return (
		osvi.dwMajorVersion == 6 &&
		osvi.dwMinorVersion == 1 &&
		osvi.wProductType == VER_NT_WORKSTATION
		);
}

//-------------------------------------------------------------------------------------
BOOL FileExists(LPCWSTR szFileName)
{
	if (wcspbrk(szFileName, L"?*") != NULL)
		return FALSE;
	WIN32_FIND_DATAW wfd;
	HANDLE hFind = FindFirstFileW(szFileName, &wfd);
	if (hFind != INVALID_HANDLE_VALUE)
	{
		FindClose(hFind);
		return (wfd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) == 0;
	}
	return FALSE;
}

//-------------------------------------------------------------------------------------
BOOL FilePatternExists(LPCWSTR szFileName)
{
	BOOL bRet = FALSE;
	WIN32_FIND_DATAW wfd;
	HANDLE hFind = FindFirstFileW(szFileName, &wfd);
	if (hFind != INVALID_HANDLE_VALUE)
	{
		do
		{
			if ((wfd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) == 0)
			{
				bRet = TRUE;
				break;
			}
		} while (FindNextFileW(hFind, &wfd));
		FindClose(hFind);
	}
	return bRet;
}

//-------------------------------------------------------------------------------------
BOOL DirectoryExists(LPCWSTR szDirName)
{
	DWORD dwAttr = GetFileAttributesW(szDirName);
	return (dwAttr != INVALID_FILE_ATTRIBUTES) &&
		((dwAttr & FILE_ATTRIBUTE_DIRECTORY) != 0);
}

//-------------------------------------------------------------------------------------
void Trim(LPWSTR szString)
{
	LPWSTR pStart = szString;
	LPWSTR pDest = szString;

	while (*pStart == L'\r' ||
		*pStart == L'\n' ||
		*pStart == L' ' ||
		*pStart == L'\t')
	{
		pStart++;
	}

	if (pStart > szString)
	{
		while (*pStart)
			*pDest++ = *pStart++;
		*pDest = L'\0';
	}

	pStart = szString + wcslen(szString) - 1;
	while (pStart >= szString && (
		*pStart == L'\r' ||
		*pStart == L'\n' ||
		*pStart == L' ' ||
		*pStart == L'\t'))
	{
		*pStart-- = L'\0';
	}
}

//-------------------------------------------------------------------------------------
void GetFileParent(LPCWSTR szFile, LPWSTR szParent, size_t count)
{
	size_t i, len;
	i = len = wcslen(szFile) - 1;
	/*go back until we encounter a colon or backslash(es)*/
	BOOL bSlashSaw = FALSE;
	while (i != 0)
	{
		if (szFile[i] == L':')
			break;
		else if (ISSLASH(szFile[i]))
		{
			if (i == 1 && ISSLASH(szFile[0]))
			{
				i = len;
				break;
			}
			else
				bSlashSaw = TRUE; //begin to eat backslashes
		}
		else if (bSlashSaw)
			break; //last backslash eaten
		i--;
	}
	if (i < count - 1)
	{
		szParent[i + 1] = L'\0';
#ifdef __GNUC__
		wmemcpy(szParent, szFile, i + 1);
#else
		wmemcpy_s(szParent, count, szFile, i + 1);
#endif
	}
	else
		szParent[0] = L'\0'; //should never occur...
}

//-------------------------------------------------------------------------------------
BOOL IsUACEnabled()
{
	BOOL bRet = FALSE;

	if (Is_WinVistaOrAbove() /*Is_WinVista() || Is_Win7() || Is_Win2008()*/)
	{
		HKEY hKey;
		LONG rc;
		rc = RegOpenKeyExW(HKEY_LOCAL_MACHINE,
			L"Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System",
			0, KEY_QUERY_VALUE, &hKey);
		if (rc == ERROR_SUCCESS)
		{
			DWORD dwType;
			DWORD data;
			DWORD cbData = sizeof(data);
			rc = RegQueryValueExW(hKey, L"EnableLUA", NULL, &dwType, (LPBYTE)&data, &cbData);
			if (rc == ERROR_SUCCESS)
				bRet = (data == 0x00000001);
			RegCloseKey(hKey);
		}
	}

	return bRet;
}