using System;
using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Confirms a payment that was created using the request-payment method
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/process-payment-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class ProcessPaymentRequest<TResult> : JsonRequest<TResult>
    {
        /// <summary>
        /// Payment request ID assigned by Yandex.Money
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// The requested method for making a payment: wallet - using funds on the user's account ID linked to the card's account (value of the id field in the bank card description)
        /// Default: wallet
        /// </summary>
        public string MoneySource { get; set; }
        /// <summary>
        /// Card Security Code
        /// </summary>
        public string Csc { get; set; }
        /// <summary>
        /// Address of the page to return to when card payment has been successfully authorized using 3-D Secure technology
        /// </summary>
        public string ExtAuthSuccessUri { get; set; }
        /// <summary>
        /// Address of the page to return to when authorization has been denied for card payment using 3-D Secure technology
        /// </summary>
        public string ExtAuthFailUri { get; set; }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.ProcessPaymentRequest class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer"></param>
        public ProcessPaymentRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer)
            : base(client, jsonSerializer)
        {
        }

        /// <summary>
        /// IRequest interface implementation
        /// </summary>
        public override string RelativeUri
        {
            get { return @"api/process-payment"; }
        }

        /// <summary>
        /// IRequest interface implementation
        /// </summary>
        /// <param name="uiParams"></param>
        public override void AppendItemsTo(Dictionary<string, string> uiParams)
        {
            if (uiParams == null)
                return;

            uiParams.Add("request_id", RequestId);

            if (!String.IsNullOrEmpty(MoneySource))
                uiParams.Add("money_source", MoneySource);

            if (!String.IsNullOrEmpty(Csc))
                uiParams.Add("csc", Csc);

            if (!String.IsNullOrEmpty(ExtAuthSuccessUri))
                uiParams.Add("ext_auth_success_uri", ExtAuthSuccessUri);

            if (!String.IsNullOrEmpty(ExtAuthFailUri))
                uiParams.Add("ext_auth_fail_uri", ExtAuthFailUri);
        }
    }
}