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

#ifndef _PATTERN_H
#define _PATTERN_H

/*
*  CPattern
*  una classe per gestire un "pattern". Ogni pattern è composto di più segmenti, memorizzati come lista concatenata.
*  Ogni segmento rappresenta un frammento di pattern (un campo o un frammento statico).
*  Ad esempio il seguente pattern:
*    FILE%i.pdf
*  viene suddiviso nei seguenti segmenti:
*    FILE		-> statico
*    %i			-> campo numerico con autoincremento
*    .pdf		-> statico
*/

class CPatternSegment;
class CPort;

class CPattern
{
public:
	CPattern(LPCWSTR szPattern, CPort* pPort, BOOL bUserCommand);
	virtual ~CPattern();

public:
	BOOL NextValue();
	LPWSTR Value();
	LPWSTR SearchValue();
	//2009-05-24 all of job's properties are stored in the CPort's m_pJobInfo structure
	//	void SetJobId(DWORD nJobId) { m_nJobId = nJobId; }
	//	void SetJobTitle(LPCWSTR szJobTitle);
	//	DWORD JobId() const { return m_nJobId; }
	/*
		LPWSTR JobTitle() const
		{
			return m_szJobTitle
				? m_szJobTitle
				: L"";
		}
	*/
	LPWSTR PatternString() { return m_szPattern; }
	void Reset();
	static LPCWSTR szDefaultFilePattern;
	static LPCWSTR szDefaultUserCommand;

private:
	CPatternSegment* m_pFirstSegment;
	CPatternSegment* m_pLastSegment;
	LPWSTR m_szBuffer;
	LPWSTR m_szSearchBuffer;
	WCHAR m_szPattern[MAX_PATH + 1];
	CPort* m_pPort;
	void AddSegment(CPatternSegment* pSegment);
};

#endif
