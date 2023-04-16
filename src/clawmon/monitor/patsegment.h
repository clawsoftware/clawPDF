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

#ifndef _PATSEGMENT_H
#define _PATSEGMENT_H

#define DECLARE_DATEPART_SEGMENT(classname) \
class classname : public CDatePartSegment \
{ \
public: \
	classname(int nWidth); \
 \
protected: \
	virtual LPCWSTR GetDatePart(); \
};

#define MAXBUF 2048

class CPort;

class CPatternSegment
{
public:
	CPatternSegment(int nWidth = 0, CPort* pPort = NULL)
	{
		m_pPort = pPort;
		m_pNext = m_pPrevious = NULL;
		m_szBuffer[0] = L'\0';
		m_nWidth = nWidth;
	}
	virtual ~CPatternSegment() { }

public:
	virtual BOOL NextValue() { return FALSE; }
	virtual LPCWSTR Value() { return m_szBuffer; }
	virtual LPCWSTR SearchValue() { return Value(); }
	virtual void Reset() { }
	void AddSegment(CPatternSegment* pNext);
	CPatternSegment* GetNext() const { return m_pNext; }
	CPatternSegment* GetPrevious() const { return m_pPrevious; }

private:
	CPatternSegment* m_pNext;
	CPatternSegment* m_pPrevious;

protected:
	CPort* m_pPort;
	WCHAR m_szBuffer[MAXBUF];
	int m_nWidth;
};

/* Base class for segments that contain date parts */
class CDatePartSegment : public CPatternSegment
{
protected:
	CDatePartSegment(int nWidth)
		: CPatternSegment(nWidth)
	{ }

public:
	virtual LPCWSTR Value();

protected:
	virtual LPCWSTR GetDatePart() = 0;

protected:
	SYSTEMTIME m_SystemTime;
	static LPCWSTR m_szDayNames[7];
	static LPCWSTR m_szMonthNames[12];
};

/* The static segment */
class CStaticSegment : public CPatternSegment
{
public:
	CStaticSegment(LPCWSTR szString);
};

/* The auto increment segment */
class CAutoIncrementSegment : public CPatternSegment
{
public:
	CAutoIncrementSegment(int nWidth, UINT nStart);

public:
	virtual BOOL NextValue();
	virtual LPCWSTR Value();
	virtual void Reset() { m_nNumber = m_nStart; }

protected:
	UINT m_nStart;
	UINT m_nNumber;
};

/* various date part segments */
DECLARE_DATEPART_SEGMENT(CLongYearSegment)
DECLARE_DATEPART_SEGMENT(CShortYearSegment)
DECLARE_DATEPART_SEGMENT(CMonthSegment)
DECLARE_DATEPART_SEGMENT(CDaySegment)
DECLARE_DATEPART_SEGMENT(CHour24Segment)
DECLARE_DATEPART_SEGMENT(CHour12Segment)
DECLARE_DATEPART_SEGMENT(CMinuteSegment)
DECLARE_DATEPART_SEGMENT(CSecondSegment)

/* day name segment */
class CDayNameSegment : public CDatePartSegment
{
public:
	CDayNameSegment(int nWidth)
		: CDatePartSegment(nWidth)
	{ }

protected:
	virtual LPCWSTR GetDatePart();
};

/* month name segment */
class CMonthNameSegment : public CDatePartSegment
{
public:
	CMonthNameSegment(int nWidth)
		: CDatePartSegment(nWidth)
	{ }

protected:
	virtual LPCWSTR GetDatePart();
};

/* job title segment */
class CJobTitleSegment : public CPatternSegment
{
public:
	CJobTitleSegment(int nWidth, CPort* pPort)
		: CPatternSegment(nWidth, pPort)
	{ }

public:
	virtual LPCWSTR Value();
};

/* job id segment */
class CJobIdSegment : public CPatternSegment
{
public:
	CJobIdSegment(int nWidth, CPort* pPort)
		: CPatternSegment(nWidth, pPort)
	{ }

public:
	virtual LPCWSTR Value();
};

/* user name segment */
class CUserNameSegment : public CPatternSegment
{
public:
	CUserNameSegment(int nWidth, CPort* pPort)
		: CPatternSegment(nWidth, pPort)
	{ }

public:
	virtual LPCWSTR Value();
};

/* computer name segment */
class CComputerNameSegment : public CPatternSegment
{
public:
	CComputerNameSegment(int nWidth, CPort* pPort)
		: CPatternSegment(nWidth, pPort)
	{ }

public:
	virtual LPCWSTR Value();
};

/* printer name segment */
class CPrinterNameSegment : public CPatternSegment
{
public:
	CPrinterNameSegment(int nWidth, CPort* pPort)
		: CPatternSegment(nWidth, pPort)
	{ }

public:
	virtual LPCWSTR Value();
};

/* file name segment */
class CFileNameSegment : public CPatternSegment
{
public:
	CFileNameSegment(int nWidth, CPort* pPort)
		: CPatternSegment(nWidth, pPort)
	{ }

public:
	virtual LPCWSTR Value();
};

/* path segment */
class CPathSegment : public CPatternSegment
{
public:
	CPathSegment(int nWidth, CPort* pPort)
		: CPatternSegment(nWidth, pPort)
	{ }

public:
	virtual LPCWSTR Value();
};

/* search segment */
class CSearchSegment : public CStaticSegment
{
public:
	CSearchSegment(LPCWSTR szString, LPCWSTR szSearch);

public:
	virtual LPCWSTR SearchValue() { return m_szSearchBuffer; }

protected:
	WCHAR m_szSearchBuffer[MAX_PATH + 1];
};

/* printer bin segment */
class CPrinterBinSegment : public CPatternSegment
{
public:
	CPrinterBinSegment(int nWidth, CPort* pPort)
		: CPatternSegment(nWidth, pPort)
	{ }

public:
	virtual LPCWSTR Value();
};

/* temp dir segment */
class CTempDirSegment : public CPatternSegment
{
public:
	CTempDirSegment(int nWidth)
		: CPatternSegment(nWidth)
	{ }

protected:
	virtual LPCWSTR Value();
};

#endif