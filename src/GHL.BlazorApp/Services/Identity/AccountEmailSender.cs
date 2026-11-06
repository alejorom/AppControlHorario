using System.Net;
using System.Text;
using GHL.Application.Interfaces;
using GHL.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace GHL.BlazorApp.Services.Identity;

public class AccountEmailSender
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly string _baseUrl;

    public AccountEmailSender(UserManager<ApplicationUser> userManager, IEmailService emailService, IConfiguration configuration)
    {
        _userManager = userManager;
        _emailService = emailService;
        _baseUrl = (configuration["App:BaseUrl"] ?? throw new InvalidOperationException("Configuration 'App:BaseUrl' not found.")).TrimEnd('/');
    }

    public async Task SendConfirmationAsync(ApplicationUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var link = $"{_baseUrl}/Account/ConfirmEmail?userId={Uri.EscapeDataString(user.Id)}&code={Encode(token)}";

        await SendAsync(user.Email!, "Confirma tu correo - GHL",
            "Gracias por registrarte. Confirma tu correo electrónico", "Confirmar correo", link);
    }

    public async Task SendPasswordResetAsync(ApplicationUser user)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var link = $"{_baseUrl}/Account/ResetPassword?userId={Uri.EscapeDataString(user.Id)}&code={Encode(token)}";

        await SendAsync(user.Email!, "Restablece tu contraseña - GHL",
            "Recibimos una solicitud para restablecer tu contraseña. El enlace es válido por 24 horas", "Restablecer contraseña", link);
    }

    public async Task SendChangeEmailAsync(ApplicationUser user, string newEmail)
    {
        var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        var link = $"{_baseUrl}/Account/ConfirmEmailChange?userId={Uri.EscapeDataString(user.Id)}&email={Uri.EscapeDataString(newEmail)}&code={Encode(token)}";

        await SendAsync(newEmail, "Confirma tu nuevo correo - GHL",
            "Solicitaste cambiar el correo de tu cuenta. Confirma este nuevo correo", "Confirmar nuevo correo", link);
    }

    public static string Decode(string code) => Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

    private static string Encode(string token) => WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

    private Task SendAsync(string to, string subject, string intro, string action, string link)
    {
        var safeLink = WebUtility.HtmlEncode(link);
        var body = $"<p>{intro}.</p><p><a href=\"{safeLink}\">{action}</a></p><p>Si no solicitaste esto, ignora este mensaje.</p>";
        return _emailService.SendAsync(to, subject, body);
    }
}
