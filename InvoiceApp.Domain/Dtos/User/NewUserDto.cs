using System.ComponentModel.DataAnnotations;
namespace InvoiceApp.Domain.Dtos.User;

public class NewUserDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
