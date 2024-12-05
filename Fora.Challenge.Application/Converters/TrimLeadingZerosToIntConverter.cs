using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fora.Challenge.Application.Converters
{
    public class TrimLeadingZerosToIntConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                // Handle as string (e.g., "0001858912")
                var stringValue = reader.GetString();
                if (string.IsNullOrEmpty(stringValue))
                {
                    throw new JsonException("Cik value is null or empty.");
                }

                if (int.TryParse(stringValue.TrimStart('0'), out var intValue))
                {
                    return intValue;
                }

                throw new JsonException($"Invalid Cik value: {stringValue}");
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                // Handle as number directly
                if (reader.TryGetInt32(out var intValue))
                {
                    return intValue;
                }

                throw new JsonException("Cik value is out of range for an integer.");
            }

            throw new JsonException($"Unexpected token type: {reader.TokenType}");
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
