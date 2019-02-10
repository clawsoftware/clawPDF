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

#ifndef _CONFIG_H
#define _CONFIG_H

#include <LMCons.h>
#include "defs.h"

//structure to transfer data between monitor DLL
//and user interface DLL
typedef struct tagPORTCONFIG
{
	WCHAR szPortName[MAX_PATH + 1];
	WCHAR szOutputPath[MAX_PATH + 1];
	WCHAR szFilePattern[MAX_PATH + 1];
	BOOL bOverwrite;
	WCHAR szUserCommandPattern[MAX_USERCOMMMAND];
	WCHAR szExecPath[MAX_PATH + 1];
	BOOL bWaitTermination;
	DWORD dwWaitTimeout;
	BOOL bPipeData;
	BOOL bHideProcess;
	BOOL bRunAsPUser;
	int nLogLevel;
	WCHAR szUser[MAX_USER];
	WCHAR szDomain[MAX_DOMAIN];
	WCHAR szPassword[MAX_PASSWORD];
} PORTCONFIG, *LPPORTCONFIG;

#endif
