using GHL.Application;
using GHL.BlazorApp.Components;
using GHL.Domain.Entities;
using GHL.Infrastructure;
using GHL.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// View features register the antiforgery filter used by [ValidateAntiForgeryToken]
builder.Services.AddControllersWithViews();

// Add MudBlazor
builder.Services.AddMudServices();

// Add Clean Architecture layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Add HttpContextAccessor for Identity
builder.Services.AddHttpContextAccessor();

// Add Authentication services
builder.Services.AddCascadingAuthenticationState();
// Add Full Identity (Cookies + SignInManager + Validators)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<GHLDbContext>()
    .AddDefaultTokenProviders()
    .AddClaimsPrincipalFactory<GHL.BlazorApp.Services.Identity.CustomUserClaimsPrincipalFactory>();

// Confirmation and password reset links stay valid for 24 hours
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromHours(24));

builder.Services.AddAuthorization();

builder.Services.AddScoped<GHL.BlazorApp.Services.Identity.AccountEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.Run();
