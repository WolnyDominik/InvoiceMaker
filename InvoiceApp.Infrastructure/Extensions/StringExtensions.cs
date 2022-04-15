using System.Text;

namespace InvoiceApp.Infrastructure.Extensions;

public static class StringExtensions
{
    public static byte[] ToByteArray(this string str)
        => ASCIIEncoding.ASCII.GetBytes(str);
}
