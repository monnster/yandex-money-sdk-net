using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// obtaining detailed information about the operation from the history
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/operation-details-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class OperationDetailsRequest<TResult> : JsonRequest<TResult>
    {
        /// <summary>
        /// Operation ID
        /// </summary>
        public string OperationId { get; set; }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.OperationDetailsRequest class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer"></param>
        public OperationDetailsRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer) : base(client, jsonSerializer)
        {
        }

        public override string RelativeUri
        {
            get { return @"api/operation-details"; }
        }

        public override void AppendItemsTo(Dictionary<string, string> uiParams)
        {
            if (uiParams == null)
                return;

            uiParams.Add("operation_id", OperationId);
        }
    }
}
