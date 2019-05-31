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

#include "sec_api.h"

static int dummy = 0;

#ifdef __GNUC__
#ifndef MINGW_HAS_SECURE_API
int __cdecl swprintf_s(wchar_t *_Dst, size_t _SizeInWords, const wchar_t *_Format, ...)
{
	int ret;
	va_list args;
	va_start(args, _Format);
	ret = vswprintf(_Dst, _Format, args);
	va_end(args);
	return ret;
}
#endif
#endif