using Yandex.Money.Api.Sdk.Utils;

namespace Yandex.Money.Api.Sdk.Authorization
{
    /// <summary>
    /// Represents an authorization request parameters
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/request-access-token-docpage/"/>
    /// </summary>
    public class AuthorizationRequestParams : RequestParams
    {
        /// <summary>
        /// The client_id that was assigned to the application during registration  
        /// <see cref="http://tech.yandex.ru/money/doc/dg/tasks/register-client-docpage/"/>
        /// </summary>
        [ParamName("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Constant value: code
        /// </summary>
        [ParamName("response_type")]
        public string ResponseType { get { return "code"; } }

        /// <summary>
        /// URI that the OAuth server sends the authorization result to
        /// </summary>
        [ParamName("redirect_uri")]
        public string RedirectUri { get; set; }

        /// <summary>
        /// Instance ID authorization in the application. An optional parameter. Allows you to obtain multiple authorizations for a single application.
        /// </summary>
        [ParamName("instance_name")]
        public string InstanceName { get; set; }

        /// <summary>
        /// A list of requested permissions. Items in the list are separated by a space. List items are case-sensitive
        /// </summary>
        [ParamName("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// converts the properties referred above to send them as the post request parameters
        /// </summary>
        /// <returns>byte array</returns>
        public byte[] PostBytes()
        {
            return Misc.PostBytes(GetParams());
        }
    }
}