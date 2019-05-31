using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using NLog;
using SystemInterface.Microsoft.Win32;

namespace clawSoft.clawPDF.Mail
{
    public class MapiClient : IEmailClient
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IRegistry _registryWrap;

        public MapiClient(IRegistry registryWrap)
        {
            _registryWrap = registryWrap;
            StartInOwnThread = true;
        }

        public bool StartInOwnThread { get; set; }

        public bool IsMapiClientInstalled
        {
            get
            {
                var mailClient = _registryWrap.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Clients\Mail", "", null);
                if (mailClient != null)
                    return true;

                mailClient = _registryWrap.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\Mail", "", null);
                return mailClient != null;
            }
        }

        public bool ShowEmailClient(Email email)
        {
            if (!IsClientInstalled)
                return false;

            try
            {
                _logger.Info("Launched client email action");

                var message = new MapiMailMessage();

                message.Subject = email.Subject;
                message.Body = email.Body;

                foreach (var recipient in email.To)
                    if (recipient.Trim() != "")
                        message.Recipients.Add(recipient);

                foreach (var attachment in email.Attachments) message.Files.Add(attachment.Filename);

                _logger.Info("Start MAPI processing");

                if (StartInOwnThread)
                    message.ShowDialogInOwnThread();
                else
                    message.ShowDialog();

                _logger.Info("Done with MAPI");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception in MAPI Email client \r\n" + ex.Message);
                return false;
            }
        }

        public bool IsClientInstalled => IsMapiClientInstalled;
    }

    #region Public MapiMailMessage Class

    /// <summary>
    ///     Represents an email message to be sent through MAPI.
    /// </summary>
    public class MapiMailMessage
    {
        #region Enums

        /// <summary>
        ///     Specifies the valid RecipientTypes for a Recipient.
        /// </summary>
        public enum RecipientType
        {
            /// <summary>
            ///     Recipient will be in the TO list.
            /// </summary>
            To = 1,

            /// <summary>
            ///     Recipient will be in the CC list.
            /// </summary>
            Cc = 2,

            /// <summary>
            ///     Recipient will be in the BCC list.
            /// </summary>
            Bcc = 3
        }

        #endregion Enums

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        #region Member Variables

        private readonly ManualResetEvent _manualResetEvent;

        #endregion Member Variables

        #region Private MapiFileDescriptor Class

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private class MapiFileDescriptor
        {
            // ReSharper disable UnusedField.Compiler
            // ReSharper disable NotAccessedField.Local
            public int reserved = 0;

            public int flags = 0;
            public int position;
            public string path;
            public string name;

            // ReSharper disable once UnusedMember.Local
            public IntPtr type = IntPtr.Zero;

            // ReSharper restore NotAccessedField.Local
            // ReSharper restore UnusedField.Compiler
        }

        #endregion Private MapiFileDescriptor Class

        #region Private MAPIHelperInterop Class

        /// <summary>
        ///     Internal class for calling MAPI APIs
        /// </summary>
        internal class MapiHelperInterop
        {
            #region Constants

            // ReSharper disable once InconsistentNaming
            public const int MAPI_LOGON_UI = 0x1;

            #endregion Constants

            #region Constructors

            /// <summary>
            ///     Private constructor.
            /// </summary>
            private MapiHelperInterop()
            {
                // Intenationally blank
            }

            #endregion Constructors

            #region APIs

            [DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
            public static extern int MAPILogon(IntPtr hwnd, string prf, string pw, int flg, int rsv, ref IntPtr sess);

            #endregion APIs

            #region Structs

            [DllImport("MAPI32.DLL")]
            public static extern int MAPISendMail(IntPtr session, IntPtr hwnd, MapiMessage message, int flg, int rsv);

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public class MapiMessage
            {
                public string ConversationID = null;
                public string DateReceived = null;
                public int FileCount;
                public IntPtr Files = IntPtr.Zero;
                public int Flags = 0;
                public string MessageType = null;
                public string NoteText;
                public IntPtr Originator = IntPtr.Zero;
                public int RecipientCount;
                public IntPtr Recipients = IntPtr.Zero;
                public int Reserved = 0;
                public string Subject;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public class MapiRecipDesc
            {
                public string Address;
                public int eIDSize = 0;
                public IntPtr EntryID = IntPtr.Zero;
                public string Name;
                public int RecipientClass;
                public int Reserved = 0;
            }

            #endregion Structs
        }

        #endregion Private MAPIHelperInterop Class

        #region Constructors

        /// <summary>
        ///     Creates a blank mail message.
        /// </summary>
        public MapiMailMessage()
        {
            Files = new ArrayList();
            Recipients = new RecipientCollection();
            _manualResetEvent = new ManualResetEvent(false);
        }

        /// <summary>
        ///     Creates a new mail message with the specified subject.
        /// </summary>
        public MapiMailMessage(string subject)
            : this()
        {
            Subject = subject;
        }

        /// <summary>
        ///     Creates a new mail message with the specified subject and body.
        /// </summary>
        public MapiMailMessage(string subject, string body)
            : this()
        {
            Subject = subject;
            Body = body;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///     Gets or sets the subject of this mail message.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///     Gets or sets the body of this mail message.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     Gets the recipient list for this mail message.
        /// </summary>
        public RecipientCollection Recipients { get; }

        /// <summary>
        ///     Gets the file list for this mail message.
        /// </summary>
        public ArrayList Files { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Displays the mail message dialog asynchronously.
        /// </summary>
        public void ShowDialogInOwnThread()
        {
            _logger.Info("Creating MAPI thread");
            // Create the mail message in an STA thread
            var t = new Thread(new ThreadStart(_ShowMail));
            t.IsBackground = false;
            //t.ApartmentState = ApartmentState.STA;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            // only return when the new thread has built it's interop representation
            _manualResetEvent.WaitOne();
            _manualResetEvent.Reset();
        }

        public void ShowDialog()
        {
            _ShowMail(null);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Sends the mail message.
        /// </summary>
        private void _ShowMail(object ignore)
        {
            _logger.Info("Showing mail client");
            var message = new MapiHelperInterop.MapiMessage();

            using (var interopRecipients
                = Recipients.GetInteropRepresentation())
            {
                _logger.Info("Subject: " + Subject);
                message.Subject = Subject;
                message.NoteText = Body;

                message.Recipients = interopRecipients.Handle;
                message.RecipientCount = Recipients.Count;

                _logger.Info("Adding {0} files ", Files.Count);
                // Check if we need to add attachments
                if (Files.Count > 0) message.Files = _AllocAttachments(out message.FileCount);

                // Signal the creating thread (make the remaining code async)
                _manualResetEvent.Set();

                const int MAPI_DIALOG = 0x8;
                //const int MAPI_LOGON_UI = 0x1;
                //const int SUCCESS_SUCCESS = 0;
                _logger.Info("Starting MAPI call");

                try
                {
                    var error = MapiHelperInterop.MAPISendMail(IntPtr.Zero, IntPtr.Zero, message, MAPI_DIALOG, 0);

                    _logger.Info("MAPI result was: " + error);

                    if (Files.Count > 0) _DeallocFiles(message);

                    _LogErrorMapi(error);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }

            _logger.Info("Done with MAPI");
        }

        /// <summary>
        ///     Deallocates the files in a message.
        /// </summary>
        /// <param name="message">The message to deallocate the files from.</param>
        private void _DeallocFiles(MapiHelperInterop.MapiMessage message)
        {
            if (message.Files != IntPtr.Zero)
            {
                var fileDescType = typeof(MapiFileDescriptor);
                var fsize = Marshal.SizeOf(fileDescType);

                // Get the ptr to the files
                var runptr = (int)message.Files;
                // Release each file
                for (var i = 0; i < message.FileCount; i++)
                {
                    Marshal.DestroyStructure((IntPtr)runptr, fileDescType);
                    runptr += fsize;
                }

                // Release the file
                Marshal.FreeHGlobal(message.Files);
            }
        }

        /// <summary>
        ///     Allocates the file attachments
        /// </summary>
        /// <param name="fileCount"></param>
        /// <returns></returns>
        private IntPtr _AllocAttachments(out int fileCount)
        {
            fileCount = 0;
            if (Files == null) return IntPtr.Zero;
            if (Files.Count <= 0 || Files.Count > 100) return IntPtr.Zero;

            var atype = typeof(MapiFileDescriptor);
            var asize = Marshal.SizeOf(atype);
            var ptra = Marshal.AllocHGlobal(Files.Count * asize);

            var mfd = new MapiFileDescriptor();
            mfd.position = -1;
            var runptr = (int)ptra;
            for (var i = 0; i < Files.Count; i++)
            {
                var path = Files[i] as string;
                mfd.name = Path.GetFileName(path);
                mfd.path = path;
                Marshal.StructureToPtr(mfd, (IntPtr)runptr, false);
                runptr += asize;
            }

            fileCount = Files.Count;
            return ptra;
        }

        /// <summary>
        ///     Sends the mail message.
        /// </summary>
        private void _ShowMail()
        {
            _ShowMail(null);
        }

        /// <summary>
        ///     Logs any Mapi errors.
        /// </summary>
        private string _LogErrorMapi(int errorCode)
        {
            // ReSharper disable InconsistentNaming
            const int MAPI_USER_ABORT = 1;
            const int MAPI_E_FAILURE = 2;
            const int MAPI_E_LOGIN_FAILURE = 3;
            const int MAPI_E_DISK_FULL = 4;
            const int MAPI_E_INSUFFICIENT_MEMORY = 5;
            const int MAPI_E_BLK_TOO_SMALL = 6;
            const int MAPI_E_TOO_MANY_SESSIONS = 8;
            const int MAPI_E_TOO_MANY_FILES = 9;
            const int MAPI_E_TOO_MANY_RECIPIENTS = 10;
            const int MAPI_E_ATTACHMENT_NOT_FOUND = 11;
            const int MAPI_E_ATTACHMENT_OPEN_FAILURE = 12;
            const int MAPI_E_ATTACHMENT_WRITE_FAILURE = 13;
            const int MAPI_E_UNKNOWN_RECIPIENT = 14;
            const int MAPI_E_BAD_RECIPTYPE = 15;
            const int MAPI_E_NO_MESSAGES = 16;
            const int MAPI_E_INVALID_MESSAGE = 17;
            const int MAPI_E_TEXT_TOO_LARGE = 18;
            const int MAPI_E_INVALID_SESSION = 19;
            const int MAPI_E_TYPE_NOT_SUPPORTED = 20;
            const int MAPI_E_AMBIGUOUS_RECIPIENT = 21;
            const int MAPI_E_MESSAGE_IN_USE = 22;
            const int MAPI_E_NETWORK_FAILURE = 23;
            const int MAPI_E_INVALID_EDITFIELDS = 24;
            const int MAPI_E_INVALID_RECIPS = 25;
            const int MAPI_E_NOT_SUPPORTED = 26;
            const int MAPI_E_NO_LIBRARY = 999;
            const int MAPI_E_INVALID_PARAMETER = 998;
            // ReSharper restore InconsistentNaming

            var error = string.Empty;
            switch (errorCode)
            {
                case MAPI_USER_ABORT:
                    error = "User Aborted.";
                    break;

                case MAPI_E_FAILURE:
                    error = "MAPI Failure.";
                    break;

                case MAPI_E_LOGIN_FAILURE:
                    error = "Login Failure.";
                    break;

                case MAPI_E_DISK_FULL:
                    error = "MAPI Disk full.";
                    break;

                case MAPI_E_INSUFFICIENT_MEMORY:
                    error = "MAPI Insufficient memory.";
                    break;

                case MAPI_E_BLK_TOO_SMALL:
                    error = "MAPI Block too small.";
                    break;

                case MAPI_E_TOO_MANY_SESSIONS:
                    error = "MAPI Too many sessions.";
                    break;

                case MAPI_E_TOO_MANY_FILES:
                    error = "MAPI too many files.";
                    break;

                case MAPI_E_TOO_MANY_RECIPIENTS:
                    error = "MAPI too many recipients.";
                    break;

                case MAPI_E_ATTACHMENT_NOT_FOUND:
                    error = "MAPI Attachment not found.";
                    break;

                case MAPI_E_ATTACHMENT_OPEN_FAILURE:
                    error = "MAPI Attachment open failure.";
                    break;

                case MAPI_E_ATTACHMENT_WRITE_FAILURE:
                    error = "MAPI Attachment Write Failure.";
                    break;

                case MAPI_E_UNKNOWN_RECIPIENT:
                    error = "MAPI Unknown recipient.";
                    break;

                case MAPI_E_BAD_RECIPTYPE:
                    error = "MAPI Bad recipient type.";
                    break;

                case MAPI_E_NO_MESSAGES:
                    error = "MAPI No messages.";
                    break;

                case MAPI_E_INVALID_MESSAGE:
                    error = "MAPI Invalid message.";
                    break;

                case MAPI_E_TEXT_TOO_LARGE:
                    error = "MAPI Text too large.";
                    break;

                case MAPI_E_INVALID_SESSION:
                    error = "MAPI Invalid session.";
                    break;

                case MAPI_E_TYPE_NOT_SUPPORTED:
                    error = "MAPI Type not supported.";
                    break;

                case MAPI_E_AMBIGUOUS_RECIPIENT:
                    error = "MAPI Ambiguous recipient.";
                    break;

                case MAPI_E_MESSAGE_IN_USE:
                    error = "MAPI Message in use.";
                    break;

                case MAPI_E_NETWORK_FAILURE:
                    error = "MAPI Network failure.";
                    break;

                case MAPI_E_INVALID_EDITFIELDS:
                    error = "MAPI Invalid edit fields.";
                    break;

                case MAPI_E_INVALID_RECIPS:
                    error = "MAPI Invalid Recipients.";
                    break;

                case MAPI_E_NOT_SUPPORTED:
                    error = "MAPI Not supported.";
                    break;

                case MAPI_E_NO_LIBRARY:
                    error = "MAPI No Library.";
                    break;

                case MAPI_E_INVALID_PARAMETER:
                    error = "MAPI Invalid parameter.";
                    break;
            }

            if (!string.IsNullOrEmpty(error))
                _logger.Warn("Error sending MAPI Email. Error: " + error + " (code = " + errorCode + ").");

            return error;
        }

        #endregion Private Methods
    }

    #endregion Public MapiMailMessage Class

    #region Public Recipient Class

    /// <summary>
    ///     Represents a Recipient for a MapiMailMessage.
    /// </summary>
    public class Recipient
    {
        #region Internal Methods

        /// <summary>
        ///     Returns an interop representation of a recepient.
        /// </summary>
        /// <returns></returns>
        internal MapiMailMessage.MapiHelperInterop.MapiRecipDesc GetInteropRepresentation()
        {
            var interop = new MapiMailMessage.MapiHelperInterop.MapiRecipDesc();

            if (DisplayName == null)
            {
                interop.Name = Address;
            }
            else
            {
                interop.Name = DisplayName;
                interop.Address = Address;
            }

            interop.RecipientClass = (int)RecipientType;

            return interop;
        }

        #endregion Internal Methods

        #region Public Properties

        /// <summary>
        ///     The email address of this recipient.
        /// </summary>
        public string Address;

        /// <summary>
        ///     The display name of this recipient.
        /// </summary>
        public string DisplayName;

        /// <summary>
        ///     How the recipient will receive this message (To, CC, BCC).
        /// </summary>
        public MapiMailMessage.RecipientType RecipientType = MapiMailMessage.RecipientType.To;

        #endregion Public Properties

        #region Constructors

        /// <summary>
        ///     Creates a new recipient with the specified address.
        /// </summary>
        public Recipient(string address)
        {
            Address = address;
        }

        /// <summary>
        ///     Creates a new recipient with the specified address and display name.
        /// </summary>
        public Recipient(string address, string displayName)
        {
            Address = address;
            DisplayName = displayName;
        }

        /// <summary>
        ///     Creates a new recipient with the specified address and recipient type.
        /// </summary>
        public Recipient(string address, MapiMailMessage.RecipientType recipientType)
        {
            Address = address;
            RecipientType = recipientType;
        }

        /// <summary>
        ///     Creates a new recipient with the specified address, display name and recipient type.
        /// </summary>
        public Recipient(string address, string displayName, MapiMailMessage.RecipientType recipientType)
        {
            Address = address;
            DisplayName = displayName;
            RecipientType = recipientType;
        }

        #endregion Constructors
    }

    #endregion Public Recipient Class

    #region Public RecipientCollection Class

    /// <summary>
    ///     Represents a colleciton of recipients for a mail message.
    /// </summary>
    public class RecipientCollection : CollectionBase
    {
        /// <summary>
        ///     Returns the recipient stored in this collection at the specified index.
        /// </summary>
        public Recipient this[int index] => (Recipient)List[index];

        /// <summary>
        ///     Adds the specified recipient to this collection.
        /// </summary>
        public void Add(Recipient value)
        {
            List.Add(value);
        }

        /// <summary>
        ///     Adds a new recipient with the specified address to this collection.
        /// </summary>
        public void Add(string address)
        {
            Add(new Recipient(address));
        }

        /// <summary>
        ///     Adds a new recipient with the specified address and display name to this collection.
        /// </summary>
        public void Add(string address, string displayName)
        {
            Add(new Recipient(address, displayName));
        }

        /// <summary>
        ///     Adds a new recipient with the specified address and recipient type to this collection.
        /// </summary>
        public void Add(string address, MapiMailMessage.RecipientType recipientType)
        {
            Add(new Recipient(address, recipientType));
        }

        /// <summary>
        ///     Adds a new recipient with the specified address, display name and recipient type to this collection.
        /// </summary>
        public void Add(string address, string displayName, MapiMailMessage.RecipientType recipientType)
        {
            Add(new Recipient(address, displayName, recipientType));
        }

        internal InteropRecipientCollection GetInteropRepresentation()
        {
            return new InteropRecipientCollection(this);
        }

        /// <summary>
        ///     Struct which contains an interop representation of a colleciton of recipients.
        /// </summary>
        internal struct InteropRecipientCollection : IDisposable
        {
            #region Member Variables

            private int _count;

            #endregion Member Variables

            #region Constructors

            /// <summary>
            ///     Default constructor for creating InteropRecipientCollection.
            /// </summary>
            /// <param name="outer"></param>
            public InteropRecipientCollection(RecipientCollection outer)
            {
                _count = outer.Count;

                if (_count == 0)
                {
                    Handle = IntPtr.Zero;
                    return;
                }

                // allocate enough memory to hold all recipients
                var size = Marshal.SizeOf(typeof(MapiMailMessage.MapiHelperInterop.MapiRecipDesc));
                Handle = Marshal.AllocHGlobal(_count * size);

                // place all interop recipients into the memory just allocated
                var ptr = (int)Handle;
                foreach (Recipient native in outer)
                {
                    var interop = native.GetInteropRepresentation();

                    // stick it in the memory block
                    Marshal.StructureToPtr(interop, (IntPtr)ptr, false);
                    ptr += size;
                }
            }

            #endregion Constructors

            #region Public Properties

            public IntPtr Handle { get; private set; }

            #endregion Public Properties

            #region Public Methods

            /// <summary>
            ///     Disposes of resources.
            /// </summary>
            public void Dispose()
            {
                if (Handle != IntPtr.Zero)
                {
                    var type = typeof(MapiMailMessage.MapiHelperInterop.MapiRecipDesc);
                    var size = Marshal.SizeOf(type);

                    // destroy all the structures in the memory area
                    var ptr = (int)Handle;
                    for (var i = 0; i < _count; i++)
                    {
                        Marshal.DestroyStructure((IntPtr)ptr, type);
                        ptr += size;
                    }

                    // free the memory
                    Marshal.FreeHGlobal(Handle);

                    Handle = IntPtr.Zero;
                    _count = 0;
                }
            }

            #endregion Public Methods
        }
    }

    #endregion Public RecipientCollection Class
}