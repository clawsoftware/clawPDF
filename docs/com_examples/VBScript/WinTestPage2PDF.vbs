Dim ShellObj
Set ShellObj = CreateObject("Shell.Application")

Dim clawPDFQueue
Set clawPDFQueue = CreateObject("clawPDF.JobQueue")

Dim objFSO
Set objFSO = CreateObject("Scripting.FileSystemObject")

Dim resultsFolderPath
resultsFolderPath = objFSO.BuildPath(WScript.ScriptFullName, "..\Results")

Dim fullPath
fullPath = objFSO.BuildPath(resultsFolderPath, "WinTestPage2Pdf.pdf")

If (Not objFSO.FolderExists(resultsFolderPath)) Then
WScript.Echo "Creating Results folder..."
objFSO.CreateFolder(resultsFolderPath)
End If

WScript.Echo "Initializing clawPDF queue..."
clawPDFQueue.Initialize()

WScript.Echo "Printing a windows testpage"
ShellObj.ShellExecute "RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n ""clawPDF""", "", "open", 1

WScript.Echo "Waiting for the job to arrive at the queue..."
If (Not clawPDFQueue.WaitForJob(10)) Then
WScript.Echo "The print job did not reach the queue within 10 seconds"
Else
WScript.Echo "Currently there are " & clawPDFQueue.Count & " job(s) in the queue"
WScript.Echo "Getting job instance"
Dim printJob
Set printJob = clawPDFQueue.NextJob

printJob.SetProfileByGuid("DefaultGuid")

WScript.Echo "Converting under ""DefaultGuid"" conversion profile"
printJob.ConvertTo(fullPath)

If (Not printJob.IsFinished Or Not printJob.IsSuccessful) Then
    WScript.Echo "Could not convert the file: " & fullPath
Else
    WScript.Echo "Job finished successfully"
End If
End If

WScript.Echo "Releasing the object"
clawPDFQueue.ReleaseCom()