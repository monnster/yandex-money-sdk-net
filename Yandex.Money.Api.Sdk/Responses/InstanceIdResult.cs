using System.Runtime.Serialization;
using Yandex.Money.Api.Sdk.Responses.Base;

namespace Yandex.Money.Api.Sdk.Responses
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class InstanceIdResult : ApiResultBase
    {
        [DataMember(Name = "instance_id")]
        public string InstanceId { get; set; }
    }
}
