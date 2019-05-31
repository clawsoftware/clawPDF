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
#include "patsegment.h"
#include "port.h"

#define IMPLEMENT_DATEPART_SEGMENT(classname, minwidth, datepart) \
classname::classname(int nWidth) \
: CDatePartSegment(nWidth) \
{ \
	if (m_nWidth >= 0 && m_nWidth < (minwidth)) \
		m_nWidth = (minwidth); \
	else if (m_nWidth < 0 && m_nWidth > -(minwidth)) \
		m_nWidth = -(minwidth); \
} \
 \
LPCWSTR classname::GetDatePart() \
{ \
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), (m_nWidth > 0) ? L"%0*i" : L"%*i", m_nWidth, (datepart)); \
	return m_szBuffer; \
}

#if (!defined(CLAWMONLANG) || CLAWMONLANG == 0x0409)
LPCWSTR CDatePartSegment::m_szDayNames[7] = {
	L"sunday",
	L"monday",
	L"tuesday",
	L"wednesday",
	L"thursday",
	L"friday",
	L"saturday"
};
LPCWSTR CDatePartSegment::m_szMonthNames[12] = {
	L"january",
	L"february",
	L"march",
	L"april",
	L"may",
	L"june",
	L"july",
	L"august",
	L"september",
	L"october",
	L"november",
	L"december"
};
#endif

//-------------------------------------------------------------------------------------
LPWSTR Sanitize(LPWSTR szString, WCHAR cReplace = L'-')
{
	//strip off invalid characters for a filename
	static LPCWSTR szInvalidCharacters = L"\\/:*?\"<>|";
	LPWSTR pPos;
	while ((pPos = wcspbrk(szString, szInvalidCharacters)) != NULL)
		*pPos = cReplace;
	//trim trailing spaces
	pPos = szString + wcslen(szString) - 1;
	while (pPos >= szString && (*pPos == L' ' || *pPos == L'\t'))
		*pPos-- = L'\0';
	//trim leading spaces
	pPos = szString;
	while (*pPos == L' ' || *pPos == L'\t')
		pPos++;
	return pPos;
}

//-------------------------------------------------------------------------------------

void CPatternSegment::AddSegment(CPatternSegment* pNext)
{
	m_pNext = pNext;
	pNext->m_pPrevious = this;
}

/* CDatePartSegment */
LPCWSTR CDatePartSegment::Value()
{
	GetLocalTime(&m_SystemTime);
	return GetDatePart();
}

/* CStaticSegment */
CStaticSegment::CStaticSegment(LPCWSTR szString)
	: CPatternSegment()
{
	wcscpy_s(m_szBuffer, LENGTHOF(m_szBuffer), szString);
}

/* CAutoIncrementSegment */
CAutoIncrementSegment::CAutoIncrementSegment(int nWidth, UINT nStart)
	: CPatternSegment(nWidth)
{
	if (m_nWidth == 0)
		m_nWidth = 4;
	else if (m_nWidth < -9)
		m_nWidth = -9;
	else if (m_nWidth > 9)
		m_nWidth = 9;
	m_nStart = m_nNumber = nStart;
}

BOOL CAutoIncrementSegment::NextValue()
{
	static UINT max[] = {
		9,
		99,
		999,
		9999,
		99999,
		999999,
		9999999,
		99999999,
		999999999
	};
	int index = (m_nWidth < -9 || m_nWidth > 9)
		? 8
		: (m_nWidth < 0)
		? 1 - m_nWidth
		: m_nWidth - 1;
	if (m_nNumber == max[index])
	{
		m_nNumber = m_nStart;
		return FALSE;
	}

	m_nNumber++;

	return TRUE;
}

LPCWSTR CAutoIncrementSegment::Value()
{
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), (m_nWidth > 0) ? L"%0*i" : L"%*i", m_nWidth, m_nNumber);
	return m_szBuffer;
}

/* various date part segments */
IMPLEMENT_DATEPART_SEGMENT(CLongYearSegment, 4, m_SystemTime.wYear)
IMPLEMENT_DATEPART_SEGMENT(CShortYearSegment, 2, m_SystemTime.wYear % 100)
IMPLEMENT_DATEPART_SEGMENT(CMonthSegment, 2, m_SystemTime.wMonth)
IMPLEMENT_DATEPART_SEGMENT(CDaySegment, 2, m_SystemTime.wDay)
IMPLEMENT_DATEPART_SEGMENT(CHour24Segment, 2, m_SystemTime.wHour)
IMPLEMENT_DATEPART_SEGMENT(CHour12Segment, 2, (m_SystemTime.wHour <= 12) ? m_SystemTime.wHour : m_SystemTime.wHour - 12)
IMPLEMENT_DATEPART_SEGMENT(CMinuteSegment, 2, m_SystemTime.wMinute)
IMPLEMENT_DATEPART_SEGMENT(CSecondSegment, 2, m_SystemTime.wSecond)

/* CDayNameSegment */
LPCWSTR CDayNameSegment::GetDatePart()
{
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), L"%*s", m_nWidth, m_szDayNames[m_SystemTime.wDayOfWeek]);
	if (m_nWidth > 0 && m_nWidth < (int)wcslen(m_szBuffer))
		m_szBuffer[m_nWidth] = L'\0';
	return m_szBuffer;
}

/* CMonthNameSegment */
LPCWSTR CMonthNameSegment::GetDatePart()
{
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), L"%*s", m_nWidth, m_szMonthNames[m_SystemTime.wMonth - 1]);
	if (m_nWidth > 0 && m_nWidth < (int)wcslen(m_szBuffer))
		m_szBuffer[m_nWidth] = L'\0';
	return m_szBuffer;
}

/* CMonthNameSegment */
LPCWSTR CJobTitleSegment::Value()
{
	WCHAR szTemp[MAXBUF];
	_ASSERTE(m_pPort != NULL);
	//copy title because we must sanitize string
	wcscpy_s(szTemp, LENGTHOF(szTemp), m_pPort->JobTitle());
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), L"%*s", m_nWidth, Sanitize(szTemp));
	return m_szBuffer;
}

/* CJobIdSegment */
LPCWSTR CJobIdSegment::Value()
{
	_ASSERTE(m_pPort != NULL);
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), (m_nWidth > 0) ? L"%0*i" : L"%*i", m_nWidth, m_pPort->JobId());
	return m_szBuffer;
}

/* CUserNameSegment */
LPCWSTR CUserNameSegment::Value()
{
	_ASSERTE(m_pPort != NULL);
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), L"%*s", m_nWidth, m_pPort->UserName());
	return m_szBuffer;
}

/* CComputerNameSegment */
LPCWSTR CComputerNameSegment::Value()
{
	_ASSERTE(m_pPort != NULL);
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), L"%*s", m_nWidth, m_pPort->ComputerName());
	return m_szBuffer;
}

/* CPrinterNameSegment */
LPCWSTR CPrinterNameSegment::Value()
{
	WCHAR szTemp[MAXBUF];
	_ASSERTE(m_pPort != NULL);
	//copy printer name because we must sanitize string
	wcscpy_s(szTemp, LENGTHOF(szTemp), m_pPort->PrinterName());
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), L"%*s", m_nWidth, Sanitize(szTemp));
	return m_szBuffer;
}

/* CFileNameSegment */
LPCWSTR CFileNameSegment::Value()
{
	_ASSERTE(m_pPort != NULL);
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), L"%*s", m_nWidth, m_pPort->FileName());
	return m_szBuffer;
}

/* CPathSegment */
LPCWSTR CPathSegment::Value()
{
	_ASSERTE(m_pPort != NULL);
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), L"%*s", m_nWidth, m_pPort->Path());
	return m_szBuffer;
}

/* CSearchSegment */
CSearchSegment::CSearchSegment(LPCWSTR szString, LPCWSTR szSearch)
	: CStaticSegment(szString)
{
	wcscpy_s(m_szSearchBuffer, LENGTHOF(m_szSearchBuffer), szSearch);
}

/* CPrinterBinSegment */
LPCWSTR CPrinterBinSegment::Value()
{
	_ASSERTE(m_pPort != NULL);
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), L"%*s", m_nWidth, m_pPort->Bin());
	return m_szBuffer;
}

/* CTempDirSegment */
LPCWSTR CTempDirSegment::Value()
{
	WCHAR szTemp[MAX_PATH + 1];
	GetTempPathW(LENGTHOF(szTemp), szTemp);
	swprintf_s(m_szBuffer, LENGTHOF(m_szBuffer), L"%*s", m_nWidth, szTemp);
	return m_szBuffer;
}