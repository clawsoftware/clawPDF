' This script uses the Windows application clawPDF and the Shell.Application COM object to print a Windows test page and convert it to a PDF file. 
' It creates a Results folder to store the converted PDF file and initializes the clawPDF queue. Once the Windows test page is printed and arrives at the queue,
' the script sets the profile to "DefaultGuid" and converts the file to a PDF file. If the conversion is successful, the script indicates that the job finished successfully.
' Otherwise, the script displays an error message.

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