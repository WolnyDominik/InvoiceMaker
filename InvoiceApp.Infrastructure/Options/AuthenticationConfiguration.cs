using InvoiceApp.Infrastructure.Extensions;

namespace InvoiceApp.Infrastructure.Options;

public class AuthenticationConfiguration
{
    public int Iterations { get; set; } = 10000;
    public string IterationsBase64 { get => Convert.ToBase64String(Iterations.ToString().ToByteArray()); }
}