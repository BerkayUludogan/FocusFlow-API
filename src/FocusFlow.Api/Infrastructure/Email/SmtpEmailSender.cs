using FocusFlow.Api.Shared.Abstractions.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FocusFlow.Api.Infrastructure.Email;

public sealed class SmtpEmailSender(IOptions<EmailSettings> options) : IEmailSender
{
    private readonly EmailSettings _settings = options.Value;

    public async Task SendAsync(
        string to,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(
            _settings.FromName,
            _settings.FromEmail));

        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        message.Body = new BodyBuilder
        {
            HtmlBody = htmlBody
        }.ToMessageBody();

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(
            _settings.Host,
            _settings.Port,
            _settings.UseSsl,
            cancellationToken);

        await smtpClient.AuthenticateAsync(
            _settings.UserName,
            _settings.Password,
            cancellationToken);

        await smtpClient.SendAsync(message, cancellationToken);

        await smtpClient.DisconnectAsync(true, cancellationToken);
    }
}