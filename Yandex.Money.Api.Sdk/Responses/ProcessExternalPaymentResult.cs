using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yandex.Money.Api.Sdk.Responses.Base;

namespace Yandex.Money.Api.Sdk.Responses
{
    /// <summary>
    /// Ru: https://tech.yandex.ru/money/doc/dg/reference/process-external-payment-docpage/
    /// En: https://tech.yandex.com/money/doc/dg/reference/process-external-payment-docpage/
    /// </summary>
    [DataContract]
    public class ProcessExternalPaymentResult : ApiResultBase
    {
        /// <summary>
        /// The transaction number in Yandex.Money. Present when payment was successfully made to a merchant.
        /// </summary>
        [DataMember(Name = "invoice_id")]
        public String InvoiceId { get; set; }

        /// <summary>
        /// Address of the authorization page. 
        /// This field is present if authorization is needed in order to complete the transaction (entering card data or authentication via 3-D Secure)
        /// </summary>
        [DataMember(Name = "acs_uri")]
        public String AcsUri { get; set; }

        /// <summary>
        /// Authorization parameters as a collection of "name"-"value" pairs. 
        /// This field is present if authorization is needed in order to complete the transaction (entering card data or authentication via 3-D Secure).
        /// </summary>
        [DataMember(Name = "acs_params")]
        public Dictionary<string, string> AcsParams { get; set; }

        /// <summary>
        /// The recommended length of time (in milliseconds) before repeating the request. 
        /// This field is present when status=in_progress.
        /// </summary>
        [DataMember(Name = "next_retry")]
        public int NextRetry { get; set; }

        /// <summary>
        /// Bank card data for repeated payments
        /// </summary>
        [DataMember(Name = "money_source")]
        public Source MoneySource { get; set; }

        public override ResponseStatus GetStatus()
        {
            var status = base.GetStatus();

            switch (StatusName)
            {
                case "ext_auth_required":
                    return ResponseStatus.ExtAuthRequired;
                default:
                    return status;
            }
        }
    }

    /// <summary>
    /// Bank card data for repeated payments
    /// </summary>
    [DataContract]
    public class Source
    {
        /// <summary>
        /// Type of funding-source: payment-card — bank card.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The type of card; may be omitted if unknown. Possible values: Visa, MasterCard, AmericanExpress, JCB.
        /// </summary>
        [DataMember(Name = "payment_card_type")]
        public string PaymentCardType { get; set; }

        /// <summary>
        /// Masked card number; the last four digits are visible.
        /// </summary>
        [DataMember(Name = "pan_fragment")]
        public string PanFragment { get; set; }

        /// <summary>
        /// Generated token for repeated payments.
        /// </summary>
        [DataMember(Name = "money_source_token")]
        public string MoneySourceToken { get; set; }
    }
}