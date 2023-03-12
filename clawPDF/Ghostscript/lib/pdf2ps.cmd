/*
 * This file is maintained by a user: if you have any questions about it,
 * please contact Mark Hale (mark.hale@physics.org).
 */

/* Convert PDF to PostScript. */

parse arg params

gs='@gsos2'
inext='.pdf'
outext='.ps'

if params='' then call usage

options='-dNOPAUSE -dBATCH -P- -dSAFER -sDEVICE=ps2write'

/* extract options from command line */
i=1
param=word(params,i)
do while substr(param,1,1)='-'
	options=options param
	i=i+1
	param=word(params,i)
end

infile=param
if infile='' then call usage
outfile=word(params,i+1)
if outfile='' then do
	outfile=infile''outext
	infile=infile''inext
end

gs '-q -sOutputFile='outfile options infile
exit

usage:
say 'Usage: pdf2ps [-dASCII85DecodePages=false] [-dLanguageLevel=n] input[.pdf output.ps]'
exit
