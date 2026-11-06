using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GHL.Domain.Entities;

namespace GHL.BlazorApp.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpPost("PerformLogin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password, [FromForm] bool rememberMe, [FromForm] string returnUrl = "/")
    {
        var safeReturnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : "/";
        var encodedReturnUrl = Uri.EscapeDataString(safeReturnUrl);

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return LocalRedirect($"/Account/Login?error=MissingFields&returnUrl={encodedReturnUrl}");
        }

        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            return LocalRedirect(safeReturnUrl);
        }

        if (result.IsLockedOut)
        {
            return LocalRedirect($"/Account/Login?error=LockedOut&returnUrl={encodedReturnUrl}");
        }

        if (result.IsNotAllowed && await IsPasswordValidAsync(email, password))
        {
            // Only reveal the unconfirmed state after the password is proven correct
            return LocalRedirect($"/Account/Login?error=EmailNotConfirmed&email={Uri.EscapeDataString(email)}&returnUrl={encodedReturnUrl}");
        }

        return LocalRedirect($"/Account/Login?error=InvalidCredentials&returnUrl={encodedReturnUrl}");
    }

    [HttpPost("PerformLogout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return LocalRedirect("/Account/Login");
    }

    [HttpGet("RefreshProfile")]
    public async Task<IActionResult> RefreshProfile([FromServices] UserManager<ApplicationUser> userManager, [FromQuery] bool emailPending = false)
    {
        var user = await userManager.GetUserAsync(User);
        if (user != null)
        {
            await _signInManager.RefreshSignInAsync(user);
        }
        return LocalRedirect(emailPending ? "/settings?success=true&emailPending=true" : "/settings?success=true");
    }

    // PasswordSignInAsync returns NotAllowed before checking the password, so verify it separately
    private async Task<bool> IsPasswordValidAsync(string email, string password)
    {
        var user = await _signInManager.UserManager.FindByEmailAsync(email);
        if (user is null)
        {
            return false;
        }

        var check = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
        return check.Succeeded;
    }
}
