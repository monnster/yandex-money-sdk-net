using System;

namespace Yandex.Money.Api.Sdk.Exceptions
{
    /// <summary>
    /// Represents error when nonexistent, expired, or revoked token specified
    /// <see cref="http://api.yandex.com/money/doc/dg/concepts/protocol-response.xml"/>
    /// See codes for reasons for authorization refusal
    /// </summary>
    public class InvalidTokenException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Exceptions.InvalidTokenException class
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public InvalidTokenException(String message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Exceptions.InvalidTokenException class
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public InvalidTokenException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}