using Fora.Challenge.Application.Exceptions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fora.Challenge.Application.Converters
{
    public class TrimLeadingZerosToIntConverter : JsonConverter<int>
    {
        /// <summary>Reads and converts the json to an int.</summary>
        /// <param name="reader">The reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        /// <returns>The converted value.</returns>
        /// <exception cref="ConversionException" />
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String) // Handle as string (e.g., "0001858912")
            {
                var stringValue = reader.GetString();
                if (string.IsNullOrEmpty(stringValue))
                    throw new ConversionException("Cik value is null or empty.");

                if (int.TryParse(stringValue.TrimStart('0'), out var intValue))
                    return intValue;

                throw new ConversionException($"Invalid Cik value: {stringValue}");
            }
            else if (reader.TokenType == JsonTokenType.Number) // Handle as number directly
            {
                if (reader.TryGetInt32(out var intValue))
                {
                    return intValue;
                }

                throw new ConversionException("Cik value is out of range.");
            }

            throw new ConversionException($"Unexpected token type: {reader.TokenType}");
        }

        /// <summary>Writes a specified value as JSON.</summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to convert to JSON.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
