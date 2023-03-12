/*
 * This file is maintained by a user: if you have any questions about it,
 * please contact Mark Hale (mark.hale@physics.org).
 */

/* Convert PostScript to PDF 1.4 (Acrobat 5-and-later compatible). */
/* The PDF compatibility level may change in the future: */
/* use ps2pdf12 or ps2pdf13 if you want a specific level. */

parse arg params

gs='@gsos2'
inext='.ps'
outext='.pdf'

if params='' then call usage

options=''

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

gs options '-q -P- -dSAFER -dNOPAUSE -dBATCH -sDEVICE=pdfwrite -sOutputFile='outfile options '-c save pop -f' infile
exit

usage:
say 'Usage: ps2pdf [options...] input[.ps output.pdf]'
exit
