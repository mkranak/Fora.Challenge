namespace Fora.Challenge.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="BadRequestException"/> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public BadRequestException(string message) : base(message)
        {

        }
    }
}
