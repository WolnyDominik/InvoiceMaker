namespace InvoiceApp.Common.Options;

public class PostgresConfiguration
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 5432;
    public string Database { get; set; } = "InvoiceMaker";
    public string Username { get; set; } = "postgres";
    public string Password { get; set; } = "postgres";
    public bool Pooling { get; set; } = true;

    public string ToConnectionString()
        => $"Host={Host}; Port={Port}; Database={Database}; Username={Username}; Password={Password}; Pooling={Pooling}";
}
