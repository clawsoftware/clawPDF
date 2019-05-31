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
#include "pattern.h"
#include "patsegment.h"
#include "port.h"

LPCWSTR CPattern::szDefaultFilePattern = L"file%i.prn";
LPCWSTR CPattern::szDefaultUserCommand = L"";

//-------------------------------------------------------------------------------------
CPattern::CPattern(LPCWSTR szPattern, CPort* pPort, BOOL bUserCommand)
{
	m_szBuffer = new WCHAR[MAX_COMMAND];
	m_szSearchBuffer = new WCHAR[MAX_COMMAND];

	//initialization
	m_pPort = pPort;
	m_pFirstSegment = m_pLastSegment = NULL;
	m_szBuffer[0] = L'\0';
	m_szSearchBuffer[0] = L'\0';
	wcscpy_s(m_szPattern, LENGTHOF(m_szPattern), szPattern);

	//buffers for search fields
	WCHAR szBuf[3][MAX_PATH + 1];
	WCHAR* pBuf[3] = {
		szBuf[0],
		szBuf[1],
		szBuf[2]
	};
	int nPipes = 0;

	//parse pattern string
	while (*szPattern)
	{
		switch (*szPattern)
		{
		case L'%':
			if (bUserCommand || nPipes == 0)
			{
				int nWidth = 0;
				UINT nStart = 1;

				//outside of a search field, all is treated as usually
				LPCWSTR pTemp = szPattern;
				szPattern++;
				if (*szPattern == L'%')
				{
					//check buffer overflow
					if ((pBuf[0] - szBuf[0]) < (LENGTHOF(szBuf[0]) - 1))
						*pBuf[0]++ = *szPattern++;
					else
						szPattern++;
				}
				else
				{
					//unary minus
					int nSign = 1;
					if (*szPattern == L'-')
					{
						nSign = -1;
						szPattern++;
					}

					//width max 2 digits
					int digits = 0;
					while (*szPattern >= L'0' && *szPattern <= L'9')
					{
						if (digits < 2)
							nWidth = nWidth * 10 + (*szPattern - L'0');
						szPattern++;
						digits++;
					}

					//apply sign
					nWidth *= nSign;

					//dot eventually followed by starting value
					if (*szPattern == L'.')
					{
						nStart = 0;
						szPattern++;
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
						BOOL bOverflow = FALSE;
						int index = (nWidth < -9 || nWidth > 9)
							? 8
							: (nWidth == 0)
							? 3
							: (nWidth < 0)
							? 1 - nWidth
							: nWidth - 1;
						while (*szPattern >= L'0' && *szPattern <= L'9')
						{
							WCHAR c = *szPattern++;
							if (bOverflow)
								continue;
							nStart = nStart * 10 + (c - L'0');
							if (nStart > max[index])
							{
								nStart = 1;
								bOverflow = TRUE;
							}
						}
					}

					//read segment type and create appropriate object
					CPatternSegment* pNewSeg = NULL;
					switch (*szPattern)
					{
					case L'i':
						if (!bUserCommand)
							pNewSeg = new CAutoIncrementSegment(nWidth, nStart);
						else
							while (pTemp <= szPattern)
							{
								//check buffer overflow
								if ((pBuf[0] - szBuf[0]) < (LENGTHOF(szBuf[0]) - 1))
									*pBuf[0]++ = *pTemp++;
								else
									pTemp++;
							}
						break;
					case L'f':
						if (bUserCommand)
							pNewSeg = new CFileNameSegment(nWidth, m_pPort);
						else
							while (pTemp <= szPattern)
							{
								//check buffer overflow
								if ((pBuf[0] - szBuf[0]) < (LENGTHOF(szBuf[0]) - 1))
									*pBuf[0]++ = *pTemp++;
								else
									pTemp++;
							}
						break;
					case L'p':
						if (bUserCommand)
							pNewSeg = new CPathSegment(nWidth, m_pPort);
						else
							while (pTemp <= szPattern)
							{
								//check buffer overflow
								if ((pBuf[0] - szBuf[0]) < (LENGTHOF(szBuf[0]) - 1))
									*pBuf[0]++ = *pTemp++;
								else
									pTemp++;
							}
						break;
					case L'y':
						pNewSeg = new CShortYearSegment(nWidth);
						break;
					case L'Y':
						pNewSeg = new CLongYearSegment(nWidth);
						break;
					case L'm':
						pNewSeg = new CMonthSegment(nWidth);
						break;
					case L'M':
						pNewSeg = new CMonthNameSegment(nWidth);
						break;
					case L'd':
						pNewSeg = new CDaySegment(nWidth);
						break;
					case L'D':
						pNewSeg = new CDayNameSegment(nWidth);
						break;
					case L'h':
						pNewSeg = new CHour12Segment(nWidth);
						break;
					case L'H':
						pNewSeg = new CHour24Segment(nWidth);
						break;
					case L'n':
						pNewSeg = new CMinuteSegment(nWidth);
						break;
					case L's':
						pNewSeg = new CSecondSegment(nWidth);
						break;
					case L't':
						pNewSeg = new CJobTitleSegment(nWidth, m_pPort);
						break;
					case L'T':
						pNewSeg = new CTempDirSegment(nWidth);
						break;
					case L'j':
						pNewSeg = new CJobIdSegment(nWidth, m_pPort);
						break;
					case L'u':
						pNewSeg = new CUserNameSegment(nWidth, m_pPort);
						break;
					case L'c':
						pNewSeg = new CComputerNameSegment(nWidth, m_pPort);
						break;
					case L'r':
						pNewSeg = new CPrinterNameSegment(nWidth, m_pPort);
						break;
					case L'b':
						pNewSeg = new CPrinterBinSegment(nWidth, m_pPort);
						break;
					default:
						//not a valid field, get here from where we started parsing
						//and put aside for a static field
						while (pTemp <= szPattern)
						{
							//check buffer overflow
							if ((pBuf[0] - szBuf[0]) < (LENGTHOF(szBuf[0]) - 1))
								*pBuf[0]++ = *pTemp++;
							else
								pTemp++;
						}
						break;
					}

					if (pNewSeg)
					{
						//add previously accumulated static segment
						if (pBuf[0] > szBuf[0])
						{
							*pBuf[0] = L'\0';
							AddSegment(new CStaticSegment(szBuf[0]));
							pBuf[0] = szBuf[0];
						}
						//then add the dynamic segment just created
						AddSegment(pNewSeg);
					}

					szPattern++;
				}
			}
			else
			{
				//we populate the nPipes-th buffer inside the search field
				//all characters are treated literally here
				if ((pBuf[nPipes] - szBuf[nPipes]) < (LENGTHOF(szBuf[nPipes]) - 1))
					*pBuf[nPipes]++ = *szPattern++;
				else
					szPattern++;
			}
			break;
		case L'|':
			if (bUserCommand)
			{
				if ((pBuf[nPipes] - szBuf[nPipes]) < (LENGTHOF(szBuf[nPipes]) - 1))
					*pBuf[nPipes]++ = *szPattern++;
				else
					szPattern++;
			}
			else
			{
				//we are entering or leaving a search field
				if (nPipes == 0 && pBuf[0] > szBuf[0])
				{
					*pBuf[0] = L'\0';
					AddSegment(new CStaticSegment(szBuf[0]));
					pBuf[0] = szBuf[0];
				}
				nPipes++;
				nPipes %= 3;
				if (nPipes == 0 && pBuf[1] > szBuf[1])
				{
					*pBuf[1] = L'\0';
					*pBuf[2] = L'\0';
					AddSegment(new CSearchSegment(szBuf[1], szBuf[2]));
					pBuf[1] = szBuf[1];
					pBuf[2] = szBuf[2];
				}
				szPattern++;
			}
			break;
		default:
			if ((pBuf[nPipes] - szBuf[nPipes]) < (LENGTHOF(szBuf[nPipes]) - 1))
				*pBuf[nPipes]++ = *szPattern++;
			else
				szPattern++;
			break;
		}
	}

	//we reached the end of the pattern - did we collect a last static field?
	if (nPipes == 0 && pBuf[0] > szBuf[0])
	{
		*pBuf[0] = L'\0';
		AddSegment(new CStaticSegment(szBuf[0]));
	}
}

//-------------------------------------------------------------------------------------
CPattern::~CPattern()
{
	CPatternSegment* pSeg = NULL;

	while (m_pFirstSegment)
	{
		pSeg = m_pFirstSegment->GetNext();
		delete m_pFirstSegment;
		m_pFirstSegment = pSeg;
	}

	delete[] m_szBuffer;
	delete[] m_szSearchBuffer;
}

//-------------------------------------------------------------------------------------
void CPattern::AddSegment(CPatternSegment *pSegment)
{
	if (m_pLastSegment)
	{
		m_pLastSegment->AddSegment(pSegment);
		m_pLastSegment = pSegment;
	}
	else
		m_pFirstSegment = m_pLastSegment = pSegment;
}

//-------------------------------------------------------------------------------------
BOOL CPattern::NextValue()
{
	CPatternSegment* pSeg = m_pLastSegment;
	while (pSeg)
	{
		if (pSeg->NextValue())
			return TRUE;
		pSeg = pSeg->GetPrevious();
	}
	return FALSE;
}

//-------------------------------------------------------------------------------------
LPWSTR CPattern::Value()
{
	m_szBuffer[0] = L'\0';
	CPatternSegment* pSeg = m_pFirstSegment;
	while (pSeg)
	{
		LPCWSTR szVal = pSeg->Value();
		if (szVal)
			wcscat_s(m_szBuffer, MAX_COMMAND, szVal);
		pSeg = pSeg->GetNext();
	}
	return m_szBuffer;
}

//-------------------------------------------------------------------------------------
LPWSTR CPattern::SearchValue()
{
	m_szSearchBuffer[0] = L'\0';
	CPatternSegment* pSeg = m_pFirstSegment;
	while (pSeg)
	{
		LPCWSTR szVal = pSeg->SearchValue();
		if (szVal)
			wcscat_s(m_szSearchBuffer, MAX_COMMAND, szVal);
		pSeg = pSeg->GetNext();
	}
	return m_szSearchBuffer;
}

//-------------------------------------------------------------------------------------
void CPattern::Reset()
{
	CPatternSegment* pSeg = m_pFirstSegment;
	while (pSeg)
	{
		pSeg->Reset();
		pSeg = pSeg->GetNext();
	}
}