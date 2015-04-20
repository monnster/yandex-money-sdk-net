using System;

namespace Yandex.Money.Api.Sdk.Exceptions
{
    /// <summary> 
    /// Represents error when the requested operation is that the token has no rights.
    /// <see cref="http://api.yandex.com/money/doc/dg/concepts/protocol-response.xml"/>
    /// See codes for reasons for authorization refusal
    /// </summary>
    public class InsufficientScopeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Exceptions.InsufficientScopeException class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public InsufficientScopeException(String message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Exceptions.InsufficientScopeException class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public InsufficientScopeException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}