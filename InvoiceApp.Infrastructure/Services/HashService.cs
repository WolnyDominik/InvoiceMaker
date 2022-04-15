using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using InvoiceApp.Infrastructure.Extensions;
using InvoiceApp.Infrastructure.Options;

namespace InvoiceApp.Infrastructure.Services;

public class HashService : IHashService
{
    private const int _saltSize = 32;
    private const int _keySize = 64;
    private readonly AuthenticationConfiguration _authOptions;

    public HashService(IOptions<AuthenticationConfiguration> options)
    {
        _authOptions = options.Value;
    }

    public string HashPassword(string password)
    {
        using (var hasher = new Rfc2898DeriveBytes(
            password, _saltSize, _authOptions.Iterations, HashAlgorithmName.SHA512))
        {
            var key = Convert.ToBase64String(hasher.GetBytes(_keySize));
            var salt = Convert.ToBase64String(hasher.GetBytes(_saltSize));

            return $"{_authOptions.IterationsBase64}.{key}.{salt}";
        }
    }
    public (bool Verified, bool ShouldUpgrade) VerifyHashedPassword(string hashedPassword, string password)
    {
        var elements = hashedPassword.Split('.');
        if (elements.Length != 3)
            throw new FormatException("Expected format {iter}.{key}.{hash}");
        
        var iterations = BitConverter.ToInt32(Convert.FromBase64String(elements[0]));
        var key = Convert.FromBase64String(elements[1]);
        var salt = Convert.FromBase64String(elements[2]);

        using (var hasher = new Rfc2898DeriveBytes(
            password, salt, iterations, HashAlgorithmName.SHA512))
        {
            var newKey = hasher.GetBytes(_keySize);

            return (newKey.SequenceEqual(key), iterations != _authOptions.Iterations);
        }
    }
}
