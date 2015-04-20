using System.Runtime.Serialization;
using Yandex.Money.Api.Sdk.Responses.Base;

namespace Yandex.Money.Api.Sdk.Responses
{
    /// <summary>
    /// Access token response
    /// </summary>
    [DataContract]
    public class TokenResult : ResultBase
    {
        /// <summary>
        /// Access token
        /// </summary>
        [DataMember(Name = "access_token")]
        public string Token { get; set; }
    }
}