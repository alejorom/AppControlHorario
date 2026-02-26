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
    public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password, [FromForm] bool rememberMe, [FromForm] string returnUrl = "/")
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return Redirect($"/Account/Login?error=MissingFields&returnUrl={Uri.EscapeDataString(returnUrl)}");
        }

        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return Redirect(returnUrl);
        }
        
        // If failed, redirect back to login page with error query param
        return Redirect($"/Account/Login?error=InvalidCredentials&returnUrl={Uri.EscapeDataString(returnUrl)}");
    }

    [HttpGet("PerformLogout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }
}
