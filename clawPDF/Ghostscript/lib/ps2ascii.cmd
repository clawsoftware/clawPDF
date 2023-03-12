/*
 * This file is maintained by a user: if you have any questions about it,
 * please contact Mark Hale (mark.hale@physics.org).
 */

@echo off
if '%1'=='' goto a0
if '%2'=='' goto a1
gsos2 -q -dSAFER -sDEVICE=txtwrite -o %2 %1
goto x
:a0
gsos2 -q -dSAFER -sDEVICE=txtwrite -o - -
goto x
:a1
gsos2 -q -dSAFER -sDEVICE=txtwrite -o - %1
goto x
:x
