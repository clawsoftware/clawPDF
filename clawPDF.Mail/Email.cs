using System.Collections.Generic;

namespace clawSoft.clawPDF.Mail
{
    public class Email
    {
        public Email()
        {
            To = new List<string>();
            Attachments = new List<Attachment>();
        }

        public ICollection<string> To { get; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public ICollection<Attachment> Attachments { get; }
    }
}