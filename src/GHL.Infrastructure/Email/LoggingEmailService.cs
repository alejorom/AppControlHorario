using GHL.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace GHL.Infrastructure.Email;

// Development fallback used when SMTP is not configured: the message and its links go to the log
public class LoggingEmailService : IEmailService
{
    private readonly ILogger<LoggingEmailService> _logger;

    public LoggingEmailService(ILogger<LoggingEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(string to, string subject, string htmlBody, CancellationToken cancellationToken = default)
    {
        _logger.LogWarning("SMTP not configured. Email to {To} | Subject: {Subject} | Body: {Body}", to, subject, htmlBody);
        return Task.CompletedTask;
    }
}
