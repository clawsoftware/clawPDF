namespace clawSoft.clawPDF.Mail
{
    public interface IEmailClient
    {
        bool IsClientInstalled { get; }

        bool ShowEmailClient(Email email);
    }
}