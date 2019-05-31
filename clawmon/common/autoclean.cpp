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
#include "autoclean.h"

//-------------------------------------------------------------------------------------
CAutoCriticalSection::CAutoCriticalSection(CRITICAL_SECTION* pCritSect)
{
	m_pCritSect = pCritSect;

	EnterCriticalSection(m_pCritSect);
}

//-------------------------------------------------------------------------------------
CAutoCriticalSection::~CAutoCriticalSection()
{
	//2009-06-09 we must preserve last error
	DWORD dwLastErr = GetLastError();
	LeaveCriticalSection(m_pCritSect);
	SetLastError(dwLastErr);
}

//-------------------------------------------------------------------------------------
CPrinterHandle::CPrinterHandle(LPWSTR szPrinterName, ACCESS_MASK DesiredAccess)
{
	PRINTER_DEFAULTSW pd;
	pd.pDatatype = NULL;
	pd.pDevMode = NULL;
	pd.DesiredAccess = DesiredAccess;
	if (!OpenPrinterW(szPrinterName, &m_hHandle, &pd))
		m_hHandle = NULL;
}

//-------------------------------------------------------------------------------------
CPrinterHandle::CPrinterHandle(LPWSTR szPrinterName, LPPRINTER_DEFAULTSW pDefaults)
{
	if (!OpenPrinterW(szPrinterName, &m_hHandle, pDefaults))
		m_hHandle = NULL;
}

//-------------------------------------------------------------------------------------
CPrinterHandle::~CPrinterHandle()
{
	DWORD dwLastErr = GetLastError();
	ClosePrinter(m_hHandle);
	SetLastError(dwLastErr);
}