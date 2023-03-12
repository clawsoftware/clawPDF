#!/usr/bin/awk -f
###=====================================================================
### Read two Adobe Font Metric files, and compute tables of the
### differences in character repertoire, declared widths (WX), and
### bounding boxes.
###
### Usage:
###	awk -f afmdiff.awk file1.afm file2.afm
###
### Author:
### 	Nelson H. F. Beebe
### 	Center for Scientific Computing
### 	University of Utah
### 	Department of Mathematics, 322 INSCC
### 	155 S 1400 E RM 233
### 	Salt Lake City, UT 84112-0090
### 	USA
### 	Email: beebe@math.utah.edu, beebe@acm.org, beebe@computer.org,
###	       beebe@ieee.org (Internet)
### 	WWW URL: http://www.math.utah.edu/~beebe
### 	Telephone: +1 801 581 5254
### 	FAX: +1 801 585 1640, +1 801 581 4148
###
########################################################################
########################################################################
########################################################################
###                                                                  ###
###        awkdiff.awk: compare two Adobe Font Metric files          ###
###                                                                  ###
###              Copyright (C) 2000 Nelson H. F. Beebe               ###
###                                                                  ###
### This program is covered by the GNU General Public License (GPL), ###
### version 2 or later, available as the file COPYING in the program ###
### source distribution, and on the Internet at                      ###
###                                                                  ###
###               ftp://ftp.gnu.org/gnu/GPL                          ###
###                                                                  ###
###               http://www.gnu.org/copyleft/gpl.html               ###
###                                                                  ###
### This program is free software; you can redistribute it and/or    ###
### modify it under the terms of the GNU General Public License as   ###
### published by the Free Software Foundation; either version 2 of   ###
### the License, or (at your option) any later version.              ###
###                                                                  ###
### This program is distributed in the hope that it will be useful,  ###
### but WITHOUT ANY WARRANTY; without even the implied warranty of   ###
### MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the    ###
### GNU General Public License for more details.                     ###
###                                                                  ###
### You should have received a copy of the GNU General Public        ###
### License along with this program; if not, write to the Free       ###
### Software Foundation, Inc., 59 Temple Place, Suite 330, Boston,   ###
### MA 02111-1307 USA                                                ###
###                                                                  ###
### This program may also be distributed as part of AFPL             ###
### Ghostscript, under the terms of the Aladdin Free Public License  ###
### (the "License").                                                 ###
###                                                                  ###
### Every copy of AFPL Ghostscript must include a copy of the        ###
### License, normally in a plain ASCII text file named PUBLIC.  The  ###
### License grants you the right to copy, modify and redistribute    ###
### AFPL Ghostscript, but only under certain conditions              ###
### described in the License.  Among other things, the License       ###
### requires that the copyright notice and this notice be preserved  ###
### on all copies.                                                   ###
###                                                                  ###
########################################################################
########################################################################
########################################################################
#
# [29-Apr-2000]
#=======================================================================

/^FontName/	{ FontName[++NFontName] = $2 }


/^C /		{
		    if (NFontName == 1)
			CharName1[$8]++
		    if (NFontName == 2)
			CharName2[$8]++
		}


/^C /		{
		    name = $8
		    if (name in WX)
		    {
			if (WX[name] != $5)
			    WXDIFF[name] = WX[name] - $5
		    }
		    else
			WX[name] = $5
		}


/^C /		{
		    name = $8
		    bx = $13 - $11
		    if (name in BX)
		    {
			if (BX[name] != bx)
			    BXDIFF[name] = BX[name] - bx
		    }
		    else
			BX[name] = bx
		}


/^C /		{
		    name = $8
		    by = $14 - $12
		    if (name in BY)
		    {
			if (BY[name] != by)
			    BYDIFF[name] = BY[name] - by
		    }
		    else
			BY[name] = by
		}


END		{
		    Sortpipe = "sort -f | pr -3 -w80 -l1 -t"
		    print "Comparison of AFM metrics in files:", ARGV[1], ARGV[2]
		    print "Font names:", FontName[1], FontName[2]
		    show_name_diffs(FontName[2],CharName2, FontName[1],CharName1)
		    show_name_diffs(FontName[1],CharName1, FontName[2],CharName2)
		    show_num_diffs("WX width differences", WXDIFF)
		    show_num_diffs("Bounding box width differences", BXDIFF)
		    show_num_diffs("Bounding box height differences",BYDIFF)
		}

function show_name_diffs(font1,array1,font2,array2, name)
{
    print "\nChars from", font2, "missing from", font1 ":"
    for (name in array2)
    {
	if (!(name in array1))
	    printf("%s\n", name) | Sortpipe
    }
    close(Sortpipe)
}

function show_num_diffs(title,array, name)
{
    printf("\n%s:\n", title)
    for (name in array)
	printf("%-15s\t%4d\n", name, array[name]) | Sortpipe
    close(Sortpipe)
}
