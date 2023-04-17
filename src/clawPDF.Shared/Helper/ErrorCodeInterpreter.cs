using System.Globalization;
using System.Text;
using clawSoft.clawPDF.Core.Actions;

namespace clawSoft.clawPDF.Shared.Helper
{
    public static class ErrorCodeInterpreter
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;

        public static string GetErrorText(ActionResult actionResult, bool withNumber)
        {
            var errorText = new StringBuilder();

            foreach (var errorCode in actionResult) errorText.Append(GetErrorText(errorCode, withNumber));
            return errorText.ToString();
        }

        public static string GetFirstErrorText(ActionResult actionResult, bool withNumber)
        {
            if (actionResult.Count > 0)
                return GetErrorText(actionResult[0], withNumber);
            return "";
        }

        public static string GetErrorText(int errorCode, bool withNumber)
        {
            var errorCodeString = errorCode.ToString(CultureInfo.InvariantCulture);
            string errorMessage;
            switch (errorCode)
            {
                //Default Viewer Action
                case 10100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "PDF Architect could not open output file.");
                    break;

                case 10101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "System could not open output file.");
                    break;
                //Email Client Action
                case 11100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Could not launch e-mail client.");
                    break;

                case 11101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No compatible e-mail client installed.");
                    break;
                /*
                case 11001: //MAPI_USER_ABORT
                     return "User Aborted.");

                case 11002: //MAPI_E_FAILURE
                     return "MAPI Failure.");

                case 11003: //MAPI_E_LOGIN_FAILURE
                     return "Login Failure.");

                case 11004: //MAPI_E_DISK_FULL
                     return "MAPI Disk full.");

                case 11005: //MAPI_E_INSUFFICIENT_MEMORY
                     return "MAPI Insufficient memory.");

                case 11006: //MAPI_E_BLK_TOO_SMALL
                     return "MAPI Block too small.");

                case 11008: //MAPI_E_TOO_MANY_SESSIONS
                     return "MAPI Too many sessions.");

                case 11009: //MAPI_E_TOO_MANY_FILES
                     return "MAPI too many files.");

                case 11010: //MAPI_E_TOO_MANY_RECIPIENTS
                     return "MAPI too many recipients.");

                case 11011: //MAPI_E_ATTACHMENT_NOT_FOUND
                     return "MAPI Attachment not found.");

                case 11012: //MAPI_E_ATTACHMENT_OPEN_FAILURE
                     return "MAPI Attachment open failure.");

                case 11013: //MAPI_E_ATTACHMENT_WRITE_FAILURE
                     return "MAPI Attachment Write Failure.");

                case 11014: //MAPI_E_UNKNOWN_RECIPIENT
                     return "MAPI Unknown recipient.");

                case 11015: //MAPI_E_BAD_RECIPTYPE
                     return "MAPI Bad recipient type.");

                case 11016: //MAPI_E_NO_MESSAGES
                     return "MAPI No messages.");

                case 11017: //MAPI_E_INVALID_MESSAGE
                     return "MAPI Invalid message.");

                case 11018: //MAPI_E_TEXT_TOO_LARGE
                     return "MAPI Text too large.");

                case 11019: //MAPI_E_INVALID_SESSION
                     return "MAPI Invalid session.");

                case 11020: //MAPI_E_TYPE_NOT_SUPPORTED
                     return "MAPI Type not supported.");

                case 11021: //MAPI_E_AMBIGUOUS_RECIPIENT
                     return "MAPI Ambiguous recipient.");

                case 11022: //MAPI_E_MESSAGE_IN_USE
                     return "MAPI Message in use.");

                case 11023: //MAPI_E_NETWORK_FAILURE
                     return "MAPI Network failure.");

                case 11024: //MAPI_E_INVALID_EDITFIELDS
                     return "MAPI Invalid edit fields.");

                case 11025: //MAPI_E_INVALID_RECIPS
                     return "MAPI Invalid Recipients.");

                case 11026: //MAPI_E_NOT_SUPPORTED
                     return "MAPI Not supported.");

                case 11999: //MAPI_E_NO_LIBRARY
                     return "MAPI No Library.");

                case 11998: //MAPI_E_INVALID_PARAMETER
                     return "MAPI Invalid parameter.");
                */

                //Signing (Checks in ProfileChecker)
                case 12100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No certification file is specified.");
                    break;

                case 12101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Certification file does not exists.");
                    break;

                case 12102:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Automatic saving without certificate password.");
                    break;

                case 12103:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Secured Time Server without login name.");
                    break;

                case 12104:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Secured Time Server without password.");
                    break;
                //Signing (Errors in Signer)
                case 12200:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "The certificate password is wrong.");
                    break;

                case 12201:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "The certificate has no private key.");
                    break;

                case 12202:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Not enough space for signature.");
                    break;

                case 12203:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Not enough space for signature.");
                    break;

                case 12204:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Launched signing without certification password.");
                    break;

                case 12205:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Error while signing. Can not connect to time server.");
                    break;

                case 12999:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Error while signing the file.");
                    break;

                //Printing Action
                case 13100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "The default printer is invalid.");
                    break;

                case 13101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "The selected printer is invalid.");
                    break;

                case 13999:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Error while printing the file.");
                    break;

                //Script Action
                case 14100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No script file is specified.");
                    break;

                case 14101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Script file does not exist.");
                    break;

                case 14999:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Error while running the script action.");
                    break;

                //SMTP EMail Action
                case 15100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No SMTP e-mail address is specified.");
                    break;

                case 15101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No SMTP e-mail recipients are specified.");
                    break;

                case 15102:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No SMTP e-mail host is specified.");
                    break;

                case 15103:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Invalid SMTP e-mail port.");
                    break;

                case 15104:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No SMTP e-mail username is specified.");
                    break;

                case 15105:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "E-mail over SMTP launched without password.");
                    break;

                case 15106:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "E-mail over SMTP could not be delivered to one or more recipients.");
                    break;

                case 15107:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Could not authorize to SMTP host.");
                    break;

                case 15108: //Info
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "User canceled retyping SMTP e-mail Password.");
                    break;

                case 15109: //Info
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Could not send e-mail over SMTP.");
                    break;

                case 15110:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Automatic saving without SMTP password.");
                    break;

                case 15999:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Error while sending e-mail over SMTP.");
                    break;

                //Stamp Action
                case 16100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "File to be stamped is no PDF file.");
                    break;

                case 16101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Could not copy from temp to output file after stamping.");
                    break;

                case 16999:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Could not stamp the file.");
                    break;

                //BackgroundPage (Checks in ProfileChecker)
                case 17100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No background file is specified.");
                    break;

                case 17101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Background file does not exist.");
                    break;

                case 17102:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Background file is no PDF file.");
                    break;

                //BackgroundPage (Errors in Signer)
                case 17200:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Could not open cover file.");
                    break;

                case 17201:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Could not open attachment file.");
                    break;

                case 17999:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Error while adding background to the document.");
                    break;

                //FTP Action
                case 18100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "FTP server is not specified.");
                    break;

                case 18101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "FTP server username is not specified.");
                    break;

                case 18102:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Invalid charackter in FTP directory.");
                    break;

                case 18104:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Could not login to FTP server.");
                    break;

                case 18105:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Failure in directory on FTP server.");
                    break;

                case 18106:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Could not read from FTP directory to ensure unique filenames.");
                    break;

                case 18107:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Could not upload file to FTP.");
                    break;

                case 18108:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Could not login to FTP server. Please check your Internet connection.");
                    break;

                case 18109:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Automatic saving without FTP server password.");
                    break;

                case 18999:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Error while uploading file to FTP server.");
                    break;

                // Attach.ME action
                case 20001:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No AttachMe credentials provided.");
                    break;

                case 20002:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Verifying user credentials failed.");
                    break;

                case 20999:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Could not share files with Attach.Me.");
                    break;

                //Autosave (Checks in ProfileChecker)
                case 21100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Automatic saving without target folder.");
                    break;

                case 21101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Automatic saving without filename template.");
                    break;

                //CoverPage (Checks in ProfileChecker)
                case 22100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No cover file is specified.");
                    break;

                case 22101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Cover file does not exist.");
                    break;

                case 22102:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "The cover file is no pdf file.");
                    break;

                //AttachmentPage (Checks in ProfileChecker)
                case 23100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No attachment file is specified.");
                    break;

                case 23101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Attachment file does not exist.");
                    break;

                case 23102:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Attachment file is no pdf file.");
                    break;

                //Stamping (Checks in ProfileChecker)
                case 24100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No stamp text is specified.");
                    break;

                case 24101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "No stamp font is specified.");
                    break;

                //Encryption (Checks in ProfileChecker)
                case 25100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Automatic saving without owner password.");
                    break;

                case 25101:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Automatic saving without user password.");
                    break;

                //Encryption (Errors in StamperCreator)
                case 25200: //PDFStamper could not be created
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Error while encrypting the file.");
                    break;

                case 25201:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Launched encryption without owner password.");
                    break;

                case 25202:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Launched encryption without user password.");
                    break;

                case 25999:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Error while encrypting the file.");
                    break;

                //PDFProcessor (general)
                case 26100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Missing output file for PDF processing.");
                    break;

                case 26101: //Exception while creating pdf preprocess file.
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Error while processing the document.");
                    break;

                case 26999:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Error while processing the document.");
                    break;

                //Folder in SaveDialog
                case 28100:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Preselected folder for save dialog is empty.");
                    break;

                //Update xmp metadata
                case 27999:
                    errorMessage = TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", errorCodeString,
                        "Error while processing the document.");
                    break;

                ///////////////////////////////////////////////////////////////////////////////////////////////
                default:
                    return TranslationHelper.TranslatorInstance.GetTranslation("ErrorCodes", "Default",
                        "Unexpected error.");
            }

            if (withNumber)
                return errorCodeString + " - " + errorMessage;
            return errorMessage;
        }
    }
}