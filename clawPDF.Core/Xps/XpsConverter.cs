using System;
using clawSoft.clawPDF.Core.Jobs;

namespace clawSoft.clawPDF.Core.Xps
{
    public class XpsConverter
    {
        private readonly IJobInfo _job;

        public XpsConverter(IJobInfo job)
        {
            _job = job;
        }

        public bool Convert(string path)
        {
            //var pdfXpsDoc = LoadPdfSharpXpsFromSourceFiles();
            //PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, path, 0);
            //return true;
            throw new NotImplementedException();
        }

        /*private XpsDocument LoadPdfSharpXpsFromSourceFiles()
        {
            //Create new xps package
            var memoryStream = new MemoryStream();
            var combinedXPS = Package.Open(memoryStream, FileMode.Create);
            var xpsWriter = System.Windows.Xps.Packaging.XpsDocument.CreateXpsDocumentWriter(new System.Windows.Xps.Packaging.XpsDocument(combinedXPS));

            var combinedSequence = new FixedDocumentSequence();

            //Go through each file given
            var filenames = _job.SourceFiles;

            foreach (var sourceFileInfo in filenames)
            {
                var file = sourceFileInfo.Filename;

                //Load Xps Package
                var singleXPS = new System.Windows.Xps.Packaging.XpsDocument(file, FileAccess.Read);
                var singleSequence = singleXPS.GetFixedDocumentSequence();

                //Go through each document in the file
                foreach (var docRef in singleSequence.References)
                {
                    var oldDoc = docRef.GetDocument(false);
                    var newDoc = new FixedDocument();
                    var newDocReference = new DocumentReference();

                    newDocReference.SetDocument(newDoc);

                    //Go through each page
                    foreach (var page in oldDoc.Pages)
                    {
                        var newPage = new PageContent();
                        newPage.Source = page.Source;
                        (newPage as IUriContext).BaseUri = ((IUriContext) page).BaseUri;
                        newPage.GetPageRoot(true);
                        newDoc.Pages.Add(newPage);
                    }

                    //Add the document to package
                    combinedSequence.References.Add(newDocReference);
                }
                singleXPS.Close();
            }

            xpsWriter.Write(combinedSequence);
            combinedXPS.Close();

            return XpsDocument.Open(memoryStream);
        }*/
    }
}