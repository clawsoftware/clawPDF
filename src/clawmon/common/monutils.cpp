/*
clawmon - print to file with automatic filename assignment
Copyright (C) 2023 // Andrew Hess // clawSoft

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