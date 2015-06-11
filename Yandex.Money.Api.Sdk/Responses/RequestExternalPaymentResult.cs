using System.Runtime.Serialization;

namespace Yandex.Money.Api.Sdk.Responses
{
    /// <summary>
    /// https://tech.yandex.com/money/doc/dg/reference/request-external-payment-docpage/
    /// </summary>
    [DataContract]
    public class RequestExternalPaymentResult : RequestPaymentResult
    {
        /// <summary>
        /// Payment title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}