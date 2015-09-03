using Yandex.Money.Api.Sdk.Interfaces;

namespace Yandex.Money.Api.Sdk.Net
{
    /// <summary>
    /// Represents an interface implementation by default
    /// </summary>
    public class  DefaultAuthenticator : IAuthenticator
    {
        /// <summary>
        /// HTTP requests must have this header:  "Authorization: Bearer Token"
        /// </summary>
        public string AuthenticationScheme
        {
            get { return "Bearer"; }
        }

        /// <summary>
        /// Access token
        /// </summary>
        public string Token { get; private set; }

		/// <summary>
		/// Initializes new instance of <see cref="DefaultAuthenticator"/> class with auth token.
		/// </summary>
		/// <param name="token">Authentication token.</param>
	    public DefaultAuthenticator([NotNull] string token)
	    {
			Argument.NotNullOrEmpty(token, "Authentication token is required.");

		    Token = token;
	    }
    }
}
