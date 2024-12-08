namespace Fora.Challenge.Application.Exceptions
{
    public class ConversionException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="ConversionException"/> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public ConversionException(string message) : base(message)
        { }
    }
}
