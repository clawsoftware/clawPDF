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

#ifndef _DEFS_H
#define _DEFS_H

#include <LMCons.h>

//maximum command line for CreateProcessW
#define MAX_COMMAND 32768

//maximum user defined command
#define MAX_USERCOMMMAND 1024
#define MAX_USER (UNLEN + 1)
#define MAX_DOMAIN (DNLEN + 1)
#define MAX_PWBLOB ((PWLEN + 1 * sizeof(WCHAR)) + 32)
#define MAX_PASSWORD (PWLEN + 1)

extern LPCWSTR szMonitorName;
extern LPCWSTR szDescription;
extern LPCWSTR szAppTitle;
extern LPCWSTR szMsgUserCommandLocksSpooler;
extern LPCWSTR szTrue;
extern LPCWSTR szFalse;

#ifdef CLAWMONUI
extern LPCWSTR szMsgInvalidPortName;
extern LPCWSTR szMsgBrowseFolderTitle;
extern LPCWSTR szMsgProvideFileName;
extern LPCWSTR szMsgInvalidFileName;
extern LPCWSTR szMsgNoAddOnRemoteSvr;
extern LPCWSTR szMsgPortExists;
extern LPCWSTR szMsgNoConfigOnRemoteSvr;
extern LPCWSTR szMsgNoDropOnRemoteSvr;
extern LPCWSTR szMsgBadInteger;
#endif

#endif