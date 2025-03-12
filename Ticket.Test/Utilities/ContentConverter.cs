using System.Text;
using System.Text.Json;

namespace Ticket.Test.Utilities;

public class ContentConverter
{
    public static StringContent ConvertToStringContent(object content)
    {
        var stringContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        return stringContent;
    }
}
