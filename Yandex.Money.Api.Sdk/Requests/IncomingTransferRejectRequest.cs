using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Cancel incoming transfers, protected by code, and transfer demand
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/incoming-transfer-reject-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class IncomingTransferRejectRequest<TResult> : JsonRequest<TResult>
    {
        /// <summary>
        ///  Operation ID
        /// </summary>
        public string OperationId { get; set; }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.IncomingTransferRejectRequest class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer"></param>
        public IncomingTransferRejectRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer)
            : base(client, jsonSerializer)
        {
        }

        public override string RelativeUri
        {
            get { return @"api/incoming-transfer-reject"; }
        }

        public override void AppendItemsTo(Dictionary<string, string> uiParams)
        {
            if (uiParams == null)
                return;

            uiParams.Add("operation_id", OperationId);
        }
    }
}
