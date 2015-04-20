using System;
using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Incoming transfers, protected by code, and transfer demand
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/incoming-transfer-accept-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class IncomingTransferAcceptRequest<TResult> : JsonRequest<TResult>
    {
        /// <summary>
        /// Operation ID
        /// </summary>
        public string OperationId { get; set; }

        /// <summary>
        /// Secret code
        /// </summary>
        public string ProtectionCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.IncomingTransferAcceptRequest class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer"></param>
        public IncomingTransferAcceptRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer)
            : base(client, jsonSerializer)
        {
        }

        /// <summary>
        /// Represents an interface implementation
        /// </summary>
        public override string RelativeUri
        {
            get { return @"api/incoming-transfer-accept"; }
        }

        /// <summary>
        /// Represents an interface implementation
        /// </summary>
        /// <param name="uiParams"></param>
        public override void AppendItemsTo(Dictionary<string, string> uiParams)
        {
            if (uiParams == null)
                return;

            uiParams.Add("operation_id", OperationId);

            if (!String.IsNullOrEmpty(ProtectionCode))
                uiParams.Add("protection_code", ProtectionCode);
        }
    }
}