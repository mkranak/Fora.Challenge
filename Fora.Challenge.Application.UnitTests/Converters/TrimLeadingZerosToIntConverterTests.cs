using Fora.Challenge.Application.Converters;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace Fora.Challenge.Application.UnitTests.Converters
{
    public class TrimLeadingZerosToIntConverterTests
    {
        private readonly TrimLeadingZerosToIntConverter _converter;

        public TrimLeadingZerosToIntConverterTests()
        {
            _converter = new TrimLeadingZerosToIntConverter();
        }

        [Fact]
        public void Read_WhenInputIsEmpty_ExpectException()
        {
            // Arrange
            var input = "\"\""; // json of an empty string

            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(input));
            reader.Read(); // move to the input

            // Act & Assert
            try
            {
                _converter.Read(ref reader, typeof(int), null);
                Assert.Fail("Exception was not thrown.");
            }
            catch (JsonException ex) 
            {
                Assert.Equal("Cik value is null or empty.", ex.Message);
            }
        }

        [Fact]
        public void Read_WhenInputIsStringWithLeadingZeros_ExpectValue()
        {
            // Arrange
            var input = "\"0001234\""; // json of a string with leading zeros
            const int expected = 1234;

            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(input));
            reader.Read(); // move to the input

            // Act
            var actual = _converter.Read(ref reader, typeof(int), null);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Read_WhenInputIsStringAndNotANumber_ExpectException()
        {
            // Arrange
            var rawInput = "NaN";
            var input = $"\"{rawInput}\""; // json of a string and not a number

            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(input));
            reader.Read(); // move to the input

            // Act & Assert
            try
            {
                _converter.Read(ref reader, typeof(int), null);
                Assert.Fail("Exception was not thrown.");
            }
            catch (JsonException ex)
            {
                Assert.Equal($"Invalid Cik value: {rawInput}", ex.Message);
            }
        }

        [Fact]
        public void Read_WhenInputIsNumber_ExpectValue()
        {
            // Arrange
            var input = "1234"; // json of a number
            const int expected = 1234;

            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(input));
            reader.Read(); // move to the input

            // Act
            var actual = _converter.Read(ref reader, typeof(int), null);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Read_WhenInputIsNumberButTooLarge_ExpectException()
        {
            // Arrange
            var input = "4000000000";

            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(input));
            reader.Read(); // move to the input

            // Act & Assert
            try
            {
                _converter.Read(ref reader, typeof(int), null);
                Assert.Fail("Exception was not thrown.");
            }
            catch (JsonException ex)
            {
                Assert.Equal("Cik value is out of range for an integer.", ex.Message);
            }
        }

        [Fact]
        public void Read_WhenInputIsInvalidTokenType_ExpectException()
        {
            // Arrange
            var input = "true";

            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(input));
            reader.Read(); // move to the input

            // Act & Assert
            try
            {
                _converter.Read(ref reader, typeof(int), null);
                Assert.Fail("Exception was not thrown.");
            }
            catch (JsonException ex)
            {
                Assert.Equal($"Unexpected token type: {reader.TokenType}", ex.Message);
            }
        }
    }
}
