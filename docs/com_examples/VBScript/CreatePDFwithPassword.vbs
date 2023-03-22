Dim ShellObj, clawPDFQueue, scriptName, fullPath, printJob, objFSO, tmp
 
Set objFSO = CreateObject("Scripting.FileSystemObject")
Set ShellObj = CreateObject("Shell.Application")
Set clawPDFQueue = CreateObject("clawPDF.JobQueue")

Dim resultsFolderPath
resultsFolderPath = objFSO.BuildPath(WScript.ScriptFullName, "..\Results")

fullPath = objFSO.GetParentFolderName(WScript.ScriptFullname ) & "\Results\TestPagePDFwithPassword.pdf" 

If (Not objFSO.FolderExists(resultsFolderPath)) Then
WScript.Echo "Creating Results folder..."
objFSO.CreateFolder(resultsFolderPath)
End If


MsgBox "Initializing clawPDF queue..."
clawPDFQueue.Initialize

MsgBox "Printing a windows testpage"
ShellObj.ShellExecute "RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""clawPDF""", "", "open", 1

MsgBox "Waiting for the job to arrive at the queue..."
if not clawPDFQueue.WaitForJob(10) then
    MsgBox "The print job did not reach the queue within " & " 10 seconds"
else 
    MsgBox "Currently there are " & clawPDFQueue.Count & " job(s) in the queue"
    MsgBox "Getting job instance"
    Set printJob = clawPDFQueue.NextJob
    
    printJob.SetProfileByGuid("DefaultGuid")

    MsgBox "Enable Security Settings"
    
    printJob.SetProfileSetting "PdfSettings.Security.Enabled", "true"
    printJob.SetProfileSetting "PdfSettings.Security.RequireUserPassword", "true"

    printJob.SetProfileSetting "PdfSettings.Security.EncryptionLevel", "Aes128Bit"

    MsgBox "Password: test"

    printJob.SetProfileSetting "PdfSettings.Security.OwnerPassword", "test"
    printJob.SetProfileSetting "PdfSettings.Security.UserPassword", "test"

    printJob.ConvertTo(fullPath)
    
    if (not printJob.IsFinished or not printJob.IsSuccessful) then
		MsgBox "Could not convert the file: " & fullPath
	else
		MsgBox "Job finished successfully"
    end if
end if

MsgBox "Releasing the object"
clawPDFQueue.ReleaseCom