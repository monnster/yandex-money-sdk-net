using System.Runtime.Serialization;
using Yandex.Money.Api.Sdk.Responses.Base;

namespace Yandex.Money.Api.Sdk.Responses
{
    [DataContract]
    public class TokenAuxResult : ResultBase
    {
        [DataMember(Name = "aux_token")]
        public string Token { get; set; }
    }
}