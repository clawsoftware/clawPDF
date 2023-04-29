using System;
using System.IO;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using NLog;

namespace clawSoft.clawPDF.PDFProcessing
{
    internal class BackgroundAdder
    {
        //ActionId = 17;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PdfReader pdfReader { get; set; }

        /// <summary>
        ///     Add a backgroundfile (setted in profile) to a document, that is opend in the stamper.
        ///     It considers cover- and attachment pages (that can also request a background).
        ///     The function does nothing, if background settings are disabled.
        /// </summary>
        /// <param name="stamper">Stamper with document</param>
        /// <param name="profile">Profile with backgroundpage settings</param>
        /// <exception cref="ProcessingException">In case of any error.</exception>
        public static void AddBackground(PdfDocument pdfDocument, ConversionProfile profile)
        {
            try
            {
                DoAddBackground(pdfDocument, profile);
            }
            catch (ProcessingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProcessingException(
                    ex.GetType() + " while addding background:" + Environment.NewLine + ex.Message, 17999);
            }
        }

        private static void DoAddBackground(PdfDocument pdfDocument, ConversionProfile profile)
        {
            if (!profile.BackgroundPage.Enabled)
                return;

            Logger.Debug("Start adding background.");

            var nFile = pdfDocument.GetNumberOfPages();

            var backgroundPdfDoc = new PdfDocument(new PdfReader(profile.BackgroundPage.File));

            // Get the first page of the background PDF document as a template
            var backgroundPage = backgroundPdfDoc.GetFirstPage().CopyAsFormXObject(pdfDocument);

            Logger.Debug("BackgroundFile: " + System.IO.Path.GetFullPath(profile.BackgroundPage.File));
            var nBackground = backgroundPdfDoc.GetNumberOfPages();

            int numberOfFrontPagesWithoutBackground;
            try
            {
                numberOfFrontPagesWithoutBackground = StartOffsetAccordingToCover(profile);
            }
            catch (Exception ex)
            {
                throw new ProcessingException(
                    ex.GetType() + " while detecting number of pages of the cover file: " +
                    profile.AttachmentPage.File +
                    "\r\n" + ex, 17200);
            }

            int lastPageWithBackground;
            try
            {
                lastPageWithBackground = nFile - EndOffsetAccordingToAttachment(profile);
            }
            catch (Exception ex)
            {
                throw new ProcessingException(
                    "Exception while detecting number of pages of the attachment file: " + profile.AttachmentPage.File +
                    "\r\n" + ex, 17201);
            }

            // Iterate through all pages of the main PDF
            for (int i = 1; i <= nFile; i++)
            {
                var backgroundPageNumber = GetBackgroundPageNumber(i, nBackground, profile.BackgroundPage.Repetition,
                    numberOfFrontPagesWithoutBackground, lastPageWithBackground);
                if (backgroundPageNumber < 1)
                    continue;

                var documentPage = pdfDocument.GetPage(i);

                // Create a canvas for the current page
                var canvas = new PdfCanvas(documentPage.NewContentStreamBefore(), documentPage.GetResources(), pdfDocument);

                // Add the background to the canvas as a template
                canvas.AddXObjectAt(backgroundPage, 0, 0);

                // Add the content of the current page to the canvas
                canvas.AddXObjectAt(documentPage.CopyAsFormXObject(pdfDocument), 0, 0);

                // Release the canvas
                canvas.Release();
            }

            backgroundPdfDoc.Close();
        }

        /// <summary>
        ///     Determine the number of pages at the beginning of the document, that do not get a background,
        ///     considering the coverpage settings in profile.
        ///     Returns 0 if cover is disabled or if "add background to cover" is enabled.
        /// </summary>
        /// <param name="profile">Profile with coverpage settings</param>
        /// <returns>Startpage for Background</returns>
        private static int StartOffsetAccordingToCover(ConversionProfile profile)
        {
            var nCover = 0;
            if (profile.CoverPage.Enabled)
                if (!profile.BackgroundPage.OnCover)
                {
                    var coverPdfReader = new PdfReader(profile.CoverPage.File);
                    var coverPdf = new PdfDocument(coverPdfReader);
                    nCover = coverPdf.GetNumberOfPages();
                }

            return nCover;
        }

        /// <summary>
        ///     Determine the number of pages at the end of the document, that do not get a background,
        ///     considering the attachmentpage settings in profile.
        ///     Returns 0 if attachment is disabled or if "add background to attachment" is enabled.
        /// </summary>
        /// <param name="profile">Profile with coverpage settings</param>
        /// <returns>Startpage for Background</returns>
        private static int EndOffsetAccordingToAttachment(ConversionProfile profile)
        {
            var nAttachment = 0;
            if (profile.AttachmentPage.Enabled)
                if (!profile.BackgroundPage.OnAttachment)
                {
                    var attachmentPdfReader = new PdfReader(profile.AttachmentPage.File);
                    var attachmentPdf = new PdfDocument(attachmentPdfReader);
                    nAttachment = attachmentPdf.GetNumberOfPages();
                }

            return nAttachment;
        }

        /// <summary>
        ///     Determine the background page for the current document page.
        /// </summary>
        /// <param name="currentDocumentPage">Current page of the document</param>
        /// <param name="numberOfBackgroundpages">Number of backgroundpages</param>
        /// <param name="repeat">Repetition of the background</param>
        /// <param name="startOffset">Number of pages that do not get a background at the beginning of the document</param>
        /// <param name="endPage">Last page that gets a background</param>
        /// <returns>Number of the backgroundpage or -1 if no background must be set</returns>
        private static int GetBackgroundPageNumber(int currentDocumentPage, int numberOfBackgroundpages,
            BackgroundRepetition repeat, int startOffset, int endPage)
        {
            if (currentDocumentPage <= startOffset)
                return -1;
            if (currentDocumentPage > endPage)
                return -1;

            switch (repeat)
            {
                case BackgroundRepetition.RepeatAllPages:
                    return (currentDocumentPage - startOffset - 1) % numberOfBackgroundpages + 1;

                case BackgroundRepetition.RepeatLastPage:
                    if (currentDocumentPage - startOffset < numberOfBackgroundpages)
                        return currentDocumentPage - startOffset;
                    //else if (currentDocumentPage - startOffset >= numberOfBackgroundpages)
                    return numberOfBackgroundpages;

                case BackgroundRepetition.NoRepetition:
                    if (currentDocumentPage - startOffset <= numberOfBackgroundpages)
                        return currentDocumentPage - startOffset;
                    //else
                    return -1;
            }

            return -1; //default
        }
    }
}