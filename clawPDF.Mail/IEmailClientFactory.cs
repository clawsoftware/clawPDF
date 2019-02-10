namespace clawSoft.clawPDF.Mail
{
    public interface IEmailClientFactory
    {
        IEmailClient CreateEmailClient();
    }
}