﻿namespace Fora.Challenge.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="NotFoundException"/> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
