using System;
using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Confirms a payment that was created using the request-payment method
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/request-external-payment-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class ProcessExternalPaymentRequest<TResult> : ProcessPaymentRequest<TResult>
    {
        public string InstanceId { get; set; }

        public Boolean RequestToken { get; set; }

        public string MoneySourceToken { get; set; }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.ProcessExternalPaymentRequest class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer"></param>
        public ProcessExternalPaymentRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer)
            : base(client, jsonSerializer)
        {
        }

        /// <summary>
        /// IRequest interface implementation
        /// </summary>
        /// <param name="uiParams"></param>
        public override void AppendItemsTo(Dictionary<string, string> uiParams)
        {
            if (uiParams == null)
                return;

            base.AppendItemsTo(uiParams);

            if (!String.IsNullOrEmpty(InstanceId))
                uiParams.Add("instance_id", InstanceId);

            if (RequestToken)
                uiParams.Add("request_token", RequestToken.ToString());

            if (!String.IsNullOrEmpty(MoneySourceToken))
                uiParams.Add("money_source_token", MoneySourceToken);
        }

        /// <summary>
        /// IRequest interface implementation
        /// </summary>
        public override string RelativeUri
        {
            get { return @"/api/process-external-payment"; }
        }
    }
}