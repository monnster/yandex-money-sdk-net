using System.Collections.Generic;
using System.Runtime.Serialization;
using Yandex.Money.Api.Sdk.Responses.Base;

namespace Yandex.Money.Api.Sdk.Responses
{
    /// <summary>
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/operation-history-docpage/"/>
    /// </summary>
    [DataContract]
    public class OperationHistoryResult : ApiResultBase
    {
        /// <summary>
        /// List of operations
        /// </summary>
        [DataMember(Name = "operations")]
        public List<OperationDetailsResult> Operations { get; set; }

        /// <summary>
        /// The number of the first history record on the next page
        /// </summary>
        [DataMember(Name = "next_record")]
        public string NextRecord { get; set; }
    }
}