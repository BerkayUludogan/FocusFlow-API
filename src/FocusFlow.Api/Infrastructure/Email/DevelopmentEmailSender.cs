using FocusFlow.Api.Shared.Abstractions.Email;

namespace FocusFlow.Api.Infrastructure.Email;

public sealed class DevelopmentEmailSender(
    ILogger<DevelopmentEmailSender> logger)
    : IEmailSender
{
    public Task SendAsync(
        string to,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Development email sent. To: {To}, Subject: {Subject}, Body: {Body}",
            to,
            subject,
            htmlBody);

        return Task.CompletedTask;
    }
}