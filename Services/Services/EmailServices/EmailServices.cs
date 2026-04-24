using Data.Helper;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.EmailServices
{
    public class EmailServices : IEmailServices
    {
        private readonly MailSetting _mail;

        public EmailServices(MailSetting mail)
        {
            _mail = mail;
        }

        public async Task SendEmail(EmailDto dto)
        {
            var email = new MimeMessage();
            email.Sender=MailboxAddress.Parse(_mail.Email);
            email.To.Add(MailboxAddress.Parse(dto.To));
            email.Subject=dto.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = dto.Body;
            email.Body=builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mail.Host, _mail.Port,SecureSocketOptions.StartTls);
            smtp.Authenticate(_mail.Email, _mail.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

        }
    }
}
