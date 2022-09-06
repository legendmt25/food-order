using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Service;

public class MailService
{
    private readonly string FROM;
    private readonly string HOST;
    private readonly int PORT;
    private readonly string USERNAME;
    private readonly string PASSWORD;
    public MailService(IConfiguration config)
    {
        IConfigurationSection mailConfig = config.GetSection("mailConfig");
        FROM = mailConfig.GetSection("from").Value;
        HOST = mailConfig.GetSection("host").Value;
        PORT = Convert.ToInt32(mailConfig.GetSection("port").Value);
        USERNAME = mailConfig.GetSection("username").Value;
        PASSWORD = mailConfig.GetSection("password").Value;
    }

    public async Task send(string to, string subject, string html)
    {
        MimeMessage email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(FROM));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = html };

        SmtpClient client = new SmtpClient();
        await client.ConnectAsync(HOST, PORT, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(USERNAME, PASSWORD);
        await client.SendAsync(email);
        await client.DisconnectAsync(true);
    }
}