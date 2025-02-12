using System.Security.Cryptography;
using System.Text;

namespace Ticket.Application.Extentions;

public static class StringExtentions
{
    public static string ToSha256(this string input)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
