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

            for (var i = 1; i <= nFile; i++)
            {
                var backgroundPageNumber = GetBackgroundPageNumber(i, nBackground, profile.BackgroundPage.Repetition,
                    numberOfFrontPagesWithoutBackground, lastPageWithBackground);
                if (backgroundPageNumber < 1)
                    continue;

                var documentPage = pdfDocument.GetPage(i);
                var documentPageSize = documentPage.GetPageSize();

                if (documentPage.GetRotation() == 90 || documentPage.GetRotation() == 270)
                {
                    //Swap width and height for landscape pages
                    documentPageSize = new Rectangle(documentPageSize.GetHeight(), documentPageSize.GetWidth());
                }

                AddPageWithRotationAndScaling(new PdfCanvas(documentPage), documentPageSize, backgroundPage,
                    new Rectangle(0, 0, backgroundPage.GetWidth(), backgroundPage.GetHeight()), documentPage.GetRotation());
            }

            backgroundPdfDoc.Close();
        }

        private static void AddPageWithRotationAndScaling(PdfCanvas canvas, Rectangle canvasSize, PdfFormXObject sourcePage,
           Rectangle sourcePageSize, int sourceRotation)
        {
            float sourceWidth = sourcePageSize.GetWidth();
            float sourceHeight = sourcePageSize.GetHeight();
            float scaleX = canvasSize.GetWidth() / sourceWidth;
            float scaleY = canvasSize.GetHeight() / sourceHeight;
            float scale = Math.Max(scaleX, scaleY);

            float translationX = (canvasSize.GetWidth() - sourceWidth * scale) / 2;
            float translationY = (canvasSize.GetHeight() - sourceHeight * scale) / 2;

            canvas.SaveState();
            canvas.ConcatMatrix(scale, 0, 0, scale, translationX, translationY);

            switch (sourceRotation)
            {
                case 90:
                    canvas.ConcatMatrix(0, 1, -1, 0, sourceHeight, 0);
                    break;

                case 180:
                    canvas.ConcatMatrix(-1, 0, 0, -1, sourceWidth, sourceHeight);
                    break;

                case 270:
                    canvas.ConcatMatrix(0, -1, 1, 0, 0, sourceWidth);
                    break;

                case 0:
                default:
                    break;
            }

            canvas.AddXObjectAt(sourcePage, 0, 0);
            canvas.RestoreState();
        }

        // -- clockwise --              cos  sin  -sin  cos  dx  dy
        //background.AddTemplate(page,  1f,   0,    0,  1f,  0,  0 ); //0°
        //background.AddTemplate(page,  0,  -1f,   1f,   0,  0,  0 ); //90°
        //background.AddTemplate(page, -1f,   0,    0, -1f,  0,  0 ); //180°
        //background.AddTemplate(page,  0,   1f,  -1f,   0,  0,  0 ); //270°

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