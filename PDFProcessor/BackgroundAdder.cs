using System;
using System.IO;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
        public static void AddBackground(PdfStamper stamper, ConversionProfile profile)
        {
            try
            {
                DoAddBackground(stamper, profile);
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

        private static void DoAddBackground(PdfStamper stamper, ConversionProfile profile)
        {
            if (!profile.BackgroundPage.Enabled)
                return;

            Logger.Debug("Start adding background.");

            var nFile = stamper.Reader.NumberOfPages;

            var backgroundPdfReader =
                new PdfReader(profile.BackgroundPage
                    .File); //ExtractPages(profile.BackgroundPage.File); //new PdfReader(profile.BackgroundPage.File);

            Logger.Debug("BackgroundFile: " + Path.GetFullPath(profile.BackgroundPage.File));
            var nBackground = backgroundPdfReader.NumberOfPages;

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

                var backgroundPage = stamper.GetImportedPage(backgroundPdfReader, backgroundPageNumber);
                var backgroundPageSize = backgroundPdfReader.GetPageSize(backgroundPageNumber);

                var backgroundPageRotation = backgroundPdfReader.GetPageRotation(backgroundPageNumber);

                var documentPage = stamper.GetUnderContent(i);
                var documentPageSize = stamper.Reader.GetPageSize(i);

                if (stamper.Reader.GetPageRotation(i) == 90 || stamper.Reader.GetPageRotation(i) == 270)
                {
                    //Turn with document page...
                    //*
                    backgroundPageRotation += 90;
                    backgroundPageRotation = backgroundPageRotation % 360;
                    //*/
                    documentPageSize = new Rectangle(documentPageSize.Height, documentPageSize.Width);
                }

                AddPageWithRotationAndScaling(documentPage, documentPageSize, backgroundPage, backgroundPageSize,
                    backgroundPageRotation);
            }
        }

        private static void AddPageWithRotationAndScaling(PdfContentByte documentPage, Rectangle documentPageSize,
            PdfImportedPage backgroundPage, Rectangle backgroundPageSize, int rotation)
        {
            float scaleWidth;
            float scaleHeight;
            float scale;
            float backgroundHeight;
            float backgroundWidth;

            switch (rotation)
            {
                case 90:
                    scaleWidth = documentPageSize.Width / backgroundPageSize.Height;
                    scaleHeight = documentPageSize.Height / backgroundPageSize.Width;
                    scale = scaleWidth < scaleHeight ? scaleWidth : scaleHeight;

                    backgroundHeight = scale * backgroundPageSize.Height;
                    backgroundWidth = scale * backgroundPageSize.Width;

                    documentPage.AddTemplate(backgroundPage, 0, -scale, scale, 0,
                        (documentPageSize.Width - backgroundHeight) / 2,
                        backgroundWidth + (documentPageSize.Height - backgroundWidth) / 2);
                    break;

                case 180:
                    scaleWidth = documentPageSize.Width / backgroundPageSize.Width;
                    scaleHeight = documentPageSize.Height / backgroundPageSize.Height;
                    scale = scaleWidth < scaleHeight ? scaleWidth : scaleHeight;

                    backgroundHeight = scale * backgroundPageSize.Height;
                    backgroundWidth = scale * backgroundPageSize.Width;

                    documentPage.AddTemplate(backgroundPage, -scale, 0, 0, -scale,
                        backgroundWidth + (documentPageSize.Width - backgroundWidth) / 2,
                        backgroundHeight + (documentPageSize.Height - backgroundHeight) / 2);
                    break;

                case 270:
                    scaleWidth = documentPageSize.Width / backgroundPageSize.Height;
                    scaleHeight = documentPageSize.Height / backgroundPageSize.Width;
                    scale = scaleWidth < scaleHeight ? scaleWidth : scaleHeight;

                    backgroundHeight = scale * backgroundPageSize.Height;
                    backgroundWidth = scale * backgroundPageSize.Width;

                    documentPage.AddTemplate(backgroundPage, 0, scale, -scale, 0,
                        backgroundHeight + (documentPageSize.Width - backgroundHeight) / 2,
                        (documentPageSize.Height - backgroundWidth) / 2);
                    break;

                case 0:
                default:
                    scaleWidth = documentPageSize.Width / backgroundPageSize.Width;
                    scaleHeight = documentPageSize.Height / backgroundPageSize.Height;
                    scale = scaleWidth < scaleHeight ? scaleWidth : scaleHeight;

                    backgroundHeight = scale * backgroundPageSize.Height;
                    backgroundWidth = scale * backgroundPageSize.Width;

                    documentPage.AddTemplate(backgroundPage, scale, 0, 0, scale,
                        (documentPageSize.Width - backgroundWidth) / 2,
                        (documentPageSize.Height - backgroundHeight) / 2);
                    break;
            }
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
                    nCover = coverPdfReader.NumberOfPages;
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
                    nAttachment = attachmentPdfReader.NumberOfPages;
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