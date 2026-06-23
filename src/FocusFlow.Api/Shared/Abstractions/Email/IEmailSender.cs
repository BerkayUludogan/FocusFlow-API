namespace FocusFlow.Api.Shared.Abstractions.Email;

public interface IEmailSender
{
    Task SendAsync(
        string to,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken);
}