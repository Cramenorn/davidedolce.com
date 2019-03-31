using Microsoft.Extensions.Options;
using MyWebsite.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebsite.BusinessLogic.SendGrid
{
    public class SendGridEmail
    {
        public async Task SendContactFormData(ContactViewModel cvm, IOptions<SendGridModel> config)
        {
            var key = config.Value.Key;
            var client = new SendGridClient(key);
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress(cvm.Email, cvm.Name));

            var recipients = new List<EmailAddress>
            {
                new EmailAddress("davidedlc97@gmail.com")
            };
            msg.AddTos(recipients);

            msg.SetSubject(cvm.Subject);

            msg.AddContent(MimeType.Text, cvm.Message);
            msg.AddContent(MimeType.Html, "<p>" + cvm.Message + "</p>");
            var list = new List<Task>();

            await client.SendEmailAsync(msg).ContinueWith(task =>
            {
                list.Add(task);
            });

            Task.WaitAll(list.ToArray());
        }
    }
}
