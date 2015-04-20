using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yandex.Money.Api.Sdk.Responses.Base;


namespace Yandex.Money.Api.Sdk.Responses
{
    /// <summary>
    ///  https://tech.yandex.ru/money/doc/dg/reference/process-payment-docpage/
    /// </summary>
    [DataContract]
    public class ProcessPaymentResult : ApiResultBase
    {
        /// <summary>
        /// Processed payment ID
        /// </summary>
        [DataMember(Name = "payment_id")]
        public String PaymentId { get; set; }

        /// <summary>
        /// Balance left on the user account after processing the payment
        /// </summary>
        [DataMember(Name = "balance")]
        public double Balance { get; set; }

        /// <summary>
        /// 	The merchant's transaction number in Yandex.Money
        /// </summary>
        [DataMember(Name = "invoice_id")]
        public String InvoiceId { get; set; }

        /// <summary>
        /// Payer's account number
        /// </summary>
        [DataMember(Name = "payer")]
        public String Payer { get; set; }

        /// <summary>
        /// Account number of the user receiving the payment
        /// </summary>
        [DataMember(Name = "payee")]
        public String Payee { get; set; }

        /// <summary>
        /// The payment amount received to the payee's account
        /// </summary>
        [DataMember(Name = "credit_amount")]
        public double CreditAmount { get; set; }

        /// <summary>
        /// The address to send the user to in order to unblock an account
        /// </summary>
        [DataMember(Name = "account_unblock_uri")]
        public String AccountUnblockUri { get; set; }

        /// <summary>
        /// A link to the deferred transfer when sending it via Yandex.Mail
        /// </summary>
        [DataMember(Name = "hold_for_pickup_link")]
        public String HoldForPickupLink { get; set; }

        /// <summary>
        /// 	Address of the card-issuing bank's authentication page for 3-D Secure technology
        /// </summary>
        [DataMember(Name = "acs_uri")]
        public String AcsUri { get; set; }

        /// <summary>
        /// Authentication parameters for 3-D Secure technology in the form of a name-value collection
        /// </summary>
        [DataMember(Name = "acs_params")]
        public Dictionary<string, string> AcsParams { get; set; }

        /// <summary>
        /// 	Recommended time interval to wait before repeating a request, in milliseconds
        /// </summary>
        [DataMember(Name = "next_retry")]
        public int NextRetry { get; set; }

        /// <summary>
        /// Data about a digital product
        /// </summary>
        [DataMember(Name = "digital_goods")]
        public DigitalGoods DigitalGoods { get; set; }

        /// <summary>
        /// The uri to send the user to in order to unblock an account
        /// </summary>
        public override Uri ActionUri
        {
            get
            {
                Uri uri;

                if (Error == "account_blocked")
                    return Uri.TryCreate(AccountUnblockUri, UriKind.RelativeOrAbsolute, out uri) ? uri : null;

                return base.ActionUri;
            }
        }

        /// <summary>
        /// status code
        /// </summary>
        /// <returns></returns>
        public override ResponseStatus GetStatus()
        {
            var status = base.GetStatus();

            if (status == ResponseStatus.Refused && Error == "account_blocked")
                return ResponseStatus.ExtActionRequired;

            switch (StatusName)
            {
                case "ext_auth_required":
                    return ResponseStatus.ExtAuthRequired;
                default:
                    return status;
            }
        }
    }
}