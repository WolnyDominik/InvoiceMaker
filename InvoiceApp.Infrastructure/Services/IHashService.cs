namespace InvoiceApp.Infrastructure.Services;

public interface IHashService
{
    string HashPassword(string password);
    (bool Verified, bool ShouldUpgrade) VerifyHashedPassword(string hashedPassword, string password);
}
