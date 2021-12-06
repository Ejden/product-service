using System.Text.Json;
using System.Text.Json.Serialization;

namespace product_service.Infrastructure.ExceptionHandlers
{
    public class ErrorDetails
    {
        [JsonPropertyName("message")]
        public string Message { get; }

        public ErrorDetails(string message)
        {
            Message = message;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
