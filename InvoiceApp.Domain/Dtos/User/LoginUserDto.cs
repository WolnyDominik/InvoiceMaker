using System.ComponentModel.DataAnnotations;
namespace InvoiceApp.Domain.Dtos.User;

public class LoginUserDto
{
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; } = false;
}
