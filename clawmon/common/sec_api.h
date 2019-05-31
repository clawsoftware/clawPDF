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

//fall back to standard routines for systems that lack secure CRT

#ifndef __SEC_API_H
#define __SEC_API_H

#ifdef __GNUC__
#ifndef MINGW_HAS_SECURE_API
#include <wchar.h>
#include <stdarg.h>

#ifdef __cplusplus
extern "C" {
#endif
	int __cdecl swprintf_s(wchar_t *_Dst, size_t _SizeInWords, const wchar_t *_Format, ...);
#ifdef __cplusplus
}
#endif

#define wcscpy_s(dst,dstlen,src) wcscpy(dst,src)

#define wcscat_s(dst,dstlen,src) wcscat(dst,src)

#define vswprintf_s(dst,dstlen,format,args) vswprintf(dst,format,args)
#endif
#endif

#endif
