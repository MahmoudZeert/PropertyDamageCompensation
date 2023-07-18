using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace PropertyDamageCompensation.Web.Email.EMailService
{
    public class EmailSenddinfService : IEmailSending
    {
        private readonly IConfiguration _configuration;

        public EmailSenddinfService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        async Task IEmailSending.SendEmailGmailAsync(EmailMessage emailMessage)
        {
            var emailGmailSettings = _configuration.GetSection("EmailGmailSettings");
            if (emailGmailSettings == null)
            {
                throw new Exception("Unable to fimd gmail configuration!!");
            }

            string smtpServer = emailGmailSettings["SmtpServer"];
            int smtpPort = int.Parse(emailGmailSettings["Port"]);
            bool useSsll = bool.Parse(emailGmailSettings["UseSsl"]);
            string smtpUsername = emailGmailSettings["UserName"];
            string smtpPassword = emailGmailSettings["Password"];
            // create  a MimeMeaasage
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(smtpUsername, smtpUsername));
            message.To.Add(new MailboxAddress(emailMessage.RecipientName, emailMessage.RecipientEmail));
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart("plain")
            {
                Text = emailMessage.Body
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtpUsername, smtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        async Task IEmailSending.SendEmailHotmailAsync(EmailMessage emailMessage)
        {
            var emailHotmailSettings = _configuration.GetSection("EmailHotmailSettings");
            if (emailHotmailSettings == null)
            {
                throw new Exception("Unable to fimd hotmail configuration!!");
            }

            string smtpServer = emailHotmailSettings["SmtpServer"];
            int smtpPort = int.Parse(emailHotmailSettings["port"]);
            bool useSsll = bool.Parse(emailHotmailSettings["UseSsl"]);
            string smtpUsername = emailHotmailSettings["UserName"];
            string smtpPassword = emailHotmailSettings["Password"];
            // crea a MimeMeaasage
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(smtpUsername, smtpUsername));
            message.To.Add(new MailboxAddress(emailMessage.RecipientName, emailMessage.RecipientEmail));
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart("plain")
            {
                Text = emailMessage.Body
            };
            using (var client=new SmtpClient())
            {
                await client.ConnectAsync(smtpServer, smtpPort,SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtpUsername, smtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
