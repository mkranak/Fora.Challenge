namespace Fora.Challenge.Application.Features.Responses
{
    public class ErrorResponse
    {
        /// <summary>Initializes a new instance of the <see cref="ErrorResponse"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        public ErrorResponse(string message, int code)
        {
            Code = code;
            Message = message;
        }

        /// <summary>Gets or sets the code.</summary>
        public int Code { get; set; }

        /// <summary>Gets or sets the message.</summary>
        public string Message { get; set; }
    }
}
