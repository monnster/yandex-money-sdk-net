using System;
using System.Collections.Generic;
using System.Linq;
using Yandex.Money.Api.Sdk.Interfaces;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Creates a payment
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class RequestExternalPaymentRequest<TResult> : RequestPaymentRequest<TResult>
    {
        public string InstanceId { get; set; }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.RequestExternalPaymentRequest class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer"></param>
        public RequestExternalPaymentRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer)
            : base(client, jsonSerializer)
        {
        }

        /// <summary>
        /// IRequest interface implementation
        /// </summary>
        public override string RelativeUri
        {
            get { return @"api/request-external-payment"; }
        }

        public override void AppendItemsTo(Dictionary<string, string> uiParams)
        {
            if (uiParams == null)
                return;

            foreach (var param in PaymentParams.Where(param => !uiParams.ContainsKey(param.Key)))
                uiParams.Add(param.Key, param.Value);

            if (String.IsNullOrEmpty(InstanceId) || PaymentParams.ContainsKey("instance_id"))
                return;

            uiParams.Add("instance_id", InstanceId);
        }
    }
}