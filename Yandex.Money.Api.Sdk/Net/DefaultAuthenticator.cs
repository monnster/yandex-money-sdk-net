using Yandex.Money.Api.Sdk.Interfaces;

namespace Yandex.Money.Api.Sdk.Net
{
    /// <summary>
    /// Represents an interface implementation by default
    /// </summary>
    public abstract class  DefaultAuthenticator : IAuthenticator
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
        public abstract string Token { get; set; }
    }
}
