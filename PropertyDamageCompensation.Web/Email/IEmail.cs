namespace PropertyDamageCompensation.Web.Email
{
    public interface IEmailSending
    {
        Task SendEmailHotmailAsync(EmailMessage emailMessage);
        Task SendEmailGmailAsync(EmailMessage emailMessage);
    }
}
