# Using the Windows application clawPDF, this script prints a Windows test page and converts it into a password-protected PDF file.
# It initializes the clawPDF queue and creates a "Results" folder to store the converted PDF. After the Windows test page is printed and added to the queue,
# the script sets the profile to "DefaultGuid" and enables security settings with 128-bit AES encryption.
# It also specifies the owner and user passwords as "test" and converts the file to PDF.
# Upon successful conversion, the script confirms the completion of the job. In case of an error, an error message is displayed.

import os
import win32com.client as win32

objFSO = win32.Dispatch("Scripting.FileSystemObject")
ShellObj = win32.Dispatch("Shell.Application")
clawPDFQueue = win32.Dispatch("clawPDF.JobQueue")

fullPath = os.path.join(os.path.dirname(os.path.abspath(__file__)), "Results\TestPagePDFwithPassword.pdf")

print("Initializing clawPDF queue...")
clawPDFQueue.Initialize()

print("Printing a windows testpage")
ShellObj.ShellExecute("RUNDLL32.exe", "PRINTUI.DLL,PrintUIEntry /k /n \"clawPDF\"", "", "open", 1)

print("Waiting for the job to arrive at the queue...")
if not clawPDFQueue.WaitForJob(10):
    print("The print job did not reach the queue within 10 seconds")
else:
    print("Currently there are", clawPDFQueue.Count, "job(s) in the queue")
    print("Getting job instance")
    printJob = clawPDFQueue.NextJob
    
    printJob.SetProfileByGuid("DefaultGuid")
    
    print("Enable Security Settings")
    
    printJob.SetProfileSetting("PdfSettings.Security.Enabled", "true")
    printJob.SetProfileSetting("PdfSettings.Security.RequireUserPassword", "true")
    
    printJob.SetProfileSetting("PdfSettings.Security.EncryptionLevel", "Aes128Bit")
    
    print("Password: test")
    
    printJob.SetProfileSetting("PdfSettings.Security.OwnerPassword", "test")
    printJob.SetProfileSetting("PdfSettings.Security.UserPassword", "test")

    out_dir = os.path.dirname(fullPath)
    if not os.path.exists(out_dir):
        print("Creating output directory:", out_dir)
        os.makedirs(out_dir)
    
    printJob.ConvertTo(fullPath)
    
    if (not printJob.IsFinished or not printJob.IsSuccessful):
        print("Could not convert the file:", fullPath)
    else:
        print("Job finished successfully")
    
print("Releasing the object")
clawPDFQueue.ReleaseCom()
