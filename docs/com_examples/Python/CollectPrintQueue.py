# This script utilizes the clawPDF Windows application to merge the print queue into a single PDF file.
# After prompting the user to input the file path, the script waits for the user to press the Enter key before processing the print queue.
# The script displays the number of jobs in the queue and the file path entered by the user. Once the Enter key is pressed,
# the script merges all jobs in the queue and converts them to a PDF file.

import time
import os
import msvcrt
import win32com.client as win32

# Initialize clawPDF job queue
clawPDFQueue = win32.Dispatch("clawPDF.JobQueue")
clawPDFQueue.Initialize()

# Prompt user to enter output file path
file_path = input("Please enter the output file path: ")

# Wait for user input before merging print queue to a single file
while True:
    print()
    print(f"  Location: {file_path}")
    print("  Press Enter to merge the printer queue to one file")
    print(f"  {clawPDFQueue.Count} jobs in the print queue")
    time.sleep(2)
    os.system('cls' if os.name == 'nt' else 'clear')
    if msvcrt.kbhit():  # Check if a key has been pressed
        key = msvcrt.getch()  # Get the key that was pressed
        if key == b'\r':  # Check if the Enter key was pressed
            break

if clawPDFQueue.Count == 0:
    exit()

# Merge all jobs in the queue and convert to a PDF file
clawPDFQueue.MergeAllJobs()
printJob = clawPDFQueue.NextJob
printJob.SetProfileByGuid("DefaultGuid")
printJob.ConvertTo(file_path)

# Release the COM object and print message to indicate completion
print("Releasing the object")
clawPDFQueue.ReleaseCom()