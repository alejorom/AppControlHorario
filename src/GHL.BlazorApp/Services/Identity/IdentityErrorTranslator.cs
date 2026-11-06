using Microsoft.AspNetCore.Identity;

namespace GHL.BlazorApp.Services.Identity;

public static class IdentityErrorTranslator
{
    public static string Translate(IdentityError error) => error.Code switch
    {
        "DuplicateEmail" or "DuplicateUserName" => "Este correo ya está registrado.",
        "PasswordTooShort" => "La contraseña debe tener al menos 8 caracteres.",
        "PasswordRequiresDigit" => "La contraseña debe incluir al menos un número.",
        "PasswordRequiresLower" => "La contraseña debe incluir al menos una minúscula.",
        "PasswordRequiresUpper" => "La contraseña debe incluir al menos una mayúscula.",
        "PasswordRequiresNonAlphanumeric" => "La contraseña debe incluir al menos un caracter especial (#,$,%,etc).",
        "InvalidToken" => "El enlace no es válido o ha expirado.",
        _ => error.Description
    };
}
