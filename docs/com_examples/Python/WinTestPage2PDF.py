import os
import subprocess
import win32com.client
import contextlib

@contextlib.contextmanager
def dispatch_clawpdf_jobqueue():
    clawPDF_queue = win32com.client.Dispatch("clawPDF.JobQueue")
    clawPDF_queue.Initialize()
    try:
        yield clawPDF_queue
    finally:
        clawPDF_queue.ReleaseCom()

results_folder_path = os.path.abspath(os.path.join(os.path.dirname(__file__), 'Results'))
os.makedirs(results_folder_path, exist_ok=True)

subprocess.run(['rundll32', 'printui.dll,PrintUIEntry', '/k', '/n', 'clawPDF'])

with dispatch_clawpdf_jobqueue() as clawPDF_queue:
    if clawPDF_queue.WaitForJob(10):
        print_job = clawPDF_queue.NextJob
        print_job.SetProfileByGuid("DefaultGuid")
        full_path = os.path.join(results_folder_path, "WinTestPage2Pdf.pdf")
        print_job.ConvertTo(full_path)
        if not print_job.IsFinished or not print_job.IsSuccessful:
            print("Could not convert the file:", full_path)
        else:
            print("Job finished successfully")
    else:
        print("The print job did not reach the queue within 10 seconds")
