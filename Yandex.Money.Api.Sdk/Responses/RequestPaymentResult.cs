using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yandex.Money.Api.Sdk.Responses.Base;

namespace Yandex.Money.Api.Sdk.Responses
{
    /// <summary>
    /// Payment using funds on the user's account
    /// </summary>
    [DataContract]
    public class Wallet
    {
        /// <summary>
        /// Flag indicating whether this payment method is allowed by the user
        /// </summary>
        /// <returns>true, if the  payment method is allowed; otherwise, false.</returns>
        [DataMember(Name = "allowed")]
        public bool Allowed { get; set; }

        /// <summary>
        /// name of the payment method. It must be specified in the method process-payment for payment from the account.
        /// </summary>
        /// <returns>"wallet"</returns>
        public static string Id
        {
            get { return "wallet"; }
        }
    }

    /// <summary>
    /// Description of the bank card linked to the account
    /// </summary>
    [DataContract]
    public class Item
    {
        /// <summary>
        /// A fragment of the bank card number
        /// </summary>
        [DataMember(Name = "pan_fragment")]
        public string PanFragment { get; set; }

        /// <summary>
        ///  Identifier of the bank card linked to the account
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        public override string ToString()
        {
            return PanFragment;
        }

        /// <summary>
        /// Indicates whether the CVV2/CVC2 code is required for authorizing payment using a bank card
        /// </summary>
        public bool CscRequired { get; set; }
    }

    /// <summary>
    /// Payment using bank cards that are linked to the account
    /// </summary>
    [DataContract]    
    public class Cards
    {
        /// <summary>
        /// Flag indicating whether this payment method is allowed by the user
        /// </summary>
        [DataMember(Name = "Allowed")]
        public bool Allowed { get; set; }

        /// <summary>
        /// Description of the bank card linked to the account
        /// </summary>
        [DataMember(Name = "items")]
        public Item[] Items { get; set; }

        /// <summary>
        /// Indicates whether the CVV2/CVC2 code is required for authorizing payment using a bank card
        /// </summary>
        [DataMember(Name = "csc_required")]
        public bool CscRequired { get; set; }
    }

    /// <summary>
    /// Possible payment methods
    /// </summary>
    [DataContract]
    public class MoneySource
    {
        /// <summary>
        /// Payment using funds on the user's account
        /// </summary>
        [DataMember(Name = "wallet")]
        public Wallet Wallet { get; set; }

        /// <summary>
        /// Payment using bank cards that are linked to the account
        /// </summary>
        [DataMember(Name = "cards")]
        public Cards Cards { get; set; }
    }

    /// <summary>
    /// response of the request-payment method http://api.yandex.com/money/doc/dg/reference/request-payment.xml
    /// </summary>
    [DataContract]
    public class RequestPaymentResult : ApiResultBase
    {
        /// <summary>
        /// Possible payment methods
        /// </summary>
        [DataMember(Name = "money_source")]
        public MoneySource MoneySource { get; set; }

        /// <summary>
        /// ID of the payment request
        /// </summary>
        [DataMember(Name = "request_id")]
        public string RequestID { get; set; }

        /// <summary>
        /// The amount to deduct from the account, in the currency used on the payer's account
        /// </summary>
        [DataMember(Name = "contract_amount")]
        public double ContractAmount { get; set; }

        /// <summary>
        /// Current balance on the user's account
        /// </summary>
        [DataMember(Name = "balance")]
        public double Balance { get; set; }

        /// <summary>
        /// The user's status
        /// </summary>
        [DataMember(Name = "recipient_account_status")]
        public string RecipientAccountStatus { get; set; }
        
        /// <summary>
        /// User account type in Yandex.Money
        /// </summary>
        [DataMember(Name = "recipient_account_type")]
        public string RecipientAccountType { get; set; }

        /// <summary>
        /// The secret code for this transfer
        /// </summary>
        [DataMember(Name = "protection_code")]
        public string ProtectionCode { get; set; }

        /// <summary>
        /// The address to send the user to in order to unblock an account
        /// </summary>
        [DataMember(Name = "account_unblock_uri")]
        public string AccountUnblockUri { get; set; }

        /// <summary>
        /// The address to send the user to in order to complete necessary actions if the ext_action_required error occurs
        /// </summary>
        [DataMember(Name = "ext_action_uri")]
        public string ExtActionUri { get; set; }

        public override Uri ActionUri
        {
            get
            {
                Uri uri;

                switch (Error)
                {
                    case "account_blocked":
                        return Uri.TryCreate(AccountUnblockUri, UriKind.RelativeOrAbsolute, out uri) ? uri : null;

                    case "ext_action_required":
                        return Uri.TryCreate(ExtActionUri, UriKind.RelativeOrAbsolute, out uri) ? uri : null;

                    default:
                        return base.ActionUri;
                }
            }
        }

        public override ResponseStatus GetStatus()
        {
            var status = base.GetStatus();

            if (status == ResponseStatus.Refused &&
                (Error == "ext_action_required" || Error == "account_blocked"))
                return ResponseStatus.ExtActionRequired;

            if (status == ResponseStatus.Refused && Error == "limit_exceeded")
                return ResponseStatus.LimitExceeded;

            switch (StatusName)
            {
                case "hold_for_pickup":
                    return ResponseStatus.HoldForPickup;
                default:
                    return status;
            }
        }

        /// <summary>
        /// A list of all available payment methods.
        /// </summary>
        /// <returns></returns>
        public List<object> MoneySources
        {
            get
            {
                var list = new List<object>();

                if (MoneySource == null)
                    return list;

                if (MoneySource.Wallet != null && MoneySource.Wallet.Allowed && Balance >= ContractAmount)
                    list.Add(MoneySource .Wallet);

                if (MoneySource.Cards == null || !MoneySource.Cards.Allowed)
                    return list;

                foreach (var card in MoneySource.Cards.Items)
                    card.CscRequired = MoneySource.Cards.CscRequired;

                list.AddRange(MoneySource.Cards.Items);

                return list;
            }
        }
    }
}