using System.Collections.Generic;
using System.Linq;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;
using Yandex.Money.Api.Sdk.Utils;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Creates a payment
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/request-payment-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class RequestPaymentRequest<TResult> : JsonRequest<TResult>
    {
        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.RequestPaymentRequest class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer"></param>
        public RequestPaymentRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer)
            : base(client, jsonSerializer)
        {
        }

        /// <summary>
        /// payment parameters
        /// </summary>
        public Dictionary<string, string> PaymentParams { get; set; }

        /// <summary>
        /// relative url https request
        /// </summary>
        public override string RelativeUri
        {
            get { return @"api/request-payment"; }
        }

        /// <summary>
        /// adds a new parameter to the parameter list
        /// </summary>
        /// <param name="uiParams"></param>
        public override void AppendItemsTo(Dictionary<string, string> uiParams)
        {
            if (uiParams == null)
                return;

            if (PaymentParams == null)
                return;

            foreach (var param in PaymentParams.Where(param => !uiParams.ContainsKey(param.Key)))
                uiParams.Add(param.Key, param.Value);
        }
    }

    /// <summary>
    /// Arguments for transferring funds to the accounts of other users
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/request-payment-docpage/"/>
    /// </summary>
    public class P2PRequestPaymentParams : RequestParams
    {
        /// <summary>
        /// Constant value: p2p
        /// </summary>
        [ParamName("pattern_id")]
        public string PatternID { get { return "p2p"; } }

        /// <summary>
        /// ID of the transfer recipient (account number, phone number, or email)
        /// </summary>
        [ParamName("to")]
        public string To { get; set; }

        /// <summary>
        ///	Amount to receive
        /// </summary>
        [ParamName("amount_due")]
        public string AmountDue { get; set; }

        /// <summary>
        /// Payment comment, displayed in the sender's history
        /// </summary>
        [ParamName("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Comments on the transfer (displayed to the recipient)
        /// </summary>
        [ParamName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Payment label. Optional parameter
        /// </summary>
        [ParamName("label")]
        public string Label { get; set; }
    }

    /// <summary>
    /// Arguments for transferring funds to the accounts of other users
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/request-payment-docpage/"/>
    /// </summary>
    public class P2PCodeProRequestPaymentParams : P2PRequestPaymentParams
    {
        /// <summary>
        /// an indication that the transfer is protected by code
        /// </summary>
        [ParamName("codepro")]
        public bool Codepro { get { return true; } }

        /// <summary>
        /// Number of days during which: a transfer recipient can enter the secret code and receive the transfer to the account a deferred transfer recipient can receive the transfer
        /// The parameter value must be between 1 and 365. Optional parameter. By default, 1.
        /// </summary>
        [ParamName("expire_period")]
        public string ExpirePeriod { get; set; }
    }

    /// <summary>
    /// Arguments for transferring funds to the accounts of other users
    /// </summary>
    public class P2PHoldForPickupRequestPaymentParams : P2PCodeProRequestPaymentParams
    {
        /// <summary>
        /// Indicates that deferred transfer
        /// </summary>
        [ParamName("hold_for_pickup")]
        public bool HoldForPickup { get { return true; } }
    }

    /// <summary>
    /// Input parameters for payment for mobile communication
    /// </summary>
    public class PhoneTopupRequestPaymentParams : RequestParams
    {
        /// <summary>
        /// Constant value: phone-topup
        /// </summary>
        [ParamName("pattern_id")]
        public string PatternID { get { return "phone-topup"; } }

        /// <summary>
        ///  Phone number in the format of ITU-T E. 164 full number starting with 7. Supported accommodation only Russian cellular operators. Example: 79219990099
        /// </summary>
        [ParamName("phone-number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Payment amount
        /// </summary>
        [ParamName("amount")]
        public string Amount { get; set; }
    }
}
