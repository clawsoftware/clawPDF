#include <File.au3>

if $cmdline[0] = 0 Then Exit(1)

if FileExists($cmdline[1]) Then

	Local $sDrive = "", $sDir = "", $sFileName = "", $sExtension = ""
	Local $aPathSplit = _PathSplit($cmdline[1], $sDrive, $sDir, $sFileName, $sExtension)

	$file = FileRead($cmdline[1])
	FileWrite(@TempDir & "\" & $sFileName & $sExtension, $file)

	if $cmdline[0] = 2 and $cmdline[2] = "delete" Then FileDelete($cmdline[1])

EndIf

Exit(0)