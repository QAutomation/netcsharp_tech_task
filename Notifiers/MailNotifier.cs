using System;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PBITracker.Clients
{
    public class MailNotifier : INotifier
    {
        private readonly SmtpClient client;
        private readonly MailAddress fromAddress;
        private readonly MailAddress toAddress;

        public MailNotifier(Hashtable config)
        {
            client = new SmtpClient()
            {
                Port = Convert.ToInt32(config["port"]),
                Host = $"{config["smtpHost"]}",
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential($"{config["login"]}", $"{config["appToken"]}"),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            fromAddress = new MailAddress($"{config["from"]}");
            toAddress = new MailAddress($"{config["to"]}");
        }

        public async Task Notify(string data)
        {
            MailMessage message = new MailMessage {
                From = fromAddress,
                Subject = data,
                IsBodyHtml = false, Body = data,
            };
            message.To.Add(toAddress);
            await client.SendMailAsync(message);
        }
    }
}