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
#include "defs.h"

LPCWSTR szMonitorName = L"clawmon";
LPCWSTR szDescription = L"clawmon printer port";
LPCWSTR szAppTitle = L"printer port monitor";
LPCWSTR szTrue = L"true";
LPCWSTR szFalse = L"false";
LPCWSTR szMsgUserCommandLocksSpooler = L"User command is locking the spooler. Wait anyway?";
LPCWSTR szMsgInvalidPortName = L"Insert a valid port name (no backslashes allowed).";
LPCWSTR szMsgBrowseFolderTitle = L"Output folder:";
LPCWSTR szMsgProvideFileName = L"Insert a valid pattern.";
LPCWSTR szMsgInvalidFileName = L"A pattern cannot contain the following characters: /:*?\"<>\r\n"
L"except * and ? can be present in a \"search field\".";
LPCWSTR szMsgNoAddOnRemoteSvr = L"Unable to add a port on a remote server.";
LPCWSTR szMsgPortExists = L"A port with this name already exists.";
LPCWSTR szMsgNoConfigOnRemoteSvr = L"Unable to configure a port on a remote server.";
LPCWSTR szMsgNoDropOnRemoteSvr = L"Unable to drop a port on a remote server.";
LPCWSTR szMsgBadInteger = L"Insert a valid number.";