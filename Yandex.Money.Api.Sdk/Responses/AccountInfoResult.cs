using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Yandex.Money.Api.Sdk.Responses
{
    public enum AccountStatus
    {
        Unknown = 0,
        Anonymous = 1,
        Named = 2,
        Identified = 3,
    }

    public enum AccountType
    {
        Unknown = 0,
        Personal = 1,
        Professional = 2,
    }

    [Flags]
    public enum CardType
    {
        Unknown = 0,
        Visa = 1,
        MasterCard = 2,
        AmericanExpress = 4,
        Jcb = 8
    }

    /// <summary>
    /// Parameters of the BalanceDetails of the AccountInfoResult
    /// </summary>
    [DataContract]
    public class BalanceDetails
    {
        /// <summary>
        /// The amount of funds pending deposit
        /// </summary>
        [DataMember(Name = "deposition_pending")]
        public double DepositionPending { get; set; }

        /// <summary>
        /// The amount of funds blocked by authorities
        /// </summary>
        [DataMember(Name = "blocked")]
        public double Blocked { get; set; }

        /// <summary>
        /// The amount owed (the negative balance on the account)
        /// </summary>
        [DataMember(Name = "debt")]
        public double Debt { get; set; }

        /// <summary>
        /// Total account balance
        /// </summary>
        [DataMember(Name = "total")]
        public double Total { get; set; }

        /// <summary>
        /// Amount available for payments
        /// </summary>
        [DataMember(Name = "available")]
        public double Available { get; set; }
    }

    /// <summary>
    /// Parameters for the Avatar of the AccountInfoResult
    /// </summary>
    [DataContract]
    public class Avatar
    {
        /// <summary>
        /// Link to the user's avatar
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Timestamp of the last change to the avatar
        /// </summary>
        [DataMember(Name = "ts")]
        public string Ts { get; set; }
    }

    /// <summary>
    /// Parameters for the CardsLinked of the AccountInfoResult
    /// </summary>
    [DataContract]
    public class CardsLinked
    {
        /// <summary>
        /// Masked card number
        /// </summary>
        [DataMember(Name = "pan_fragment")]
        public string PanFragment { get; set; }

        /// <summary>
        /// Card type. May be omitted if unknown
        /// </summary>
        [DataMember(Name = "type")]
        public string TypeOfCard { get; set; }

        public CardType Type
        {
            get
            {
                switch (TypeOfCard)
                {
                    case "VISA":
                        return CardType.Visa;
                    case "MasterCard":
                        return CardType.MasterCard;
                    case "AmericanExpress":
                        return CardType.AmericanExpress;
                    case "JCB":
                        return CardType.Jcb;
                    default: return CardType.Unknown;
                }
            }
        }
    }

    /// <summary>
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/account-info-docpage/"/>
    /// </summary>
    [DataContract]
    public class AccountInfoResult
    {
        /// <summary>
        /// The user's account
        /// </summary>
        [DataMember(Name = "account")]
        public string Account { get; set; }

        /// <summary>
        /// The account balance
        /// </summary>
        [DataMember(Name = "balance")]
        public double Balance { get; set; }

        /// <summary>
        /// The currency code of the account user
        /// </summary>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// The user's status
        /// </summary>
        [DataMember(Name = "account_status")]
        public string StatusOfAccount { get; set; }

        /// <summary>
        /// User's account type
        /// </summary>
        [DataMember(Name = "account_type")]
        public string AccountType { get; set; }

        /// <summary>
        ///  The link to the avatar of the user.
        /// </summary>
        [DataMember(Name = "avatar")]
        public Avatar Avatar { get; set; }

        /// <summary>
        /// Extended information on the balance
        /// </summary>
        [DataMember(Name = "balance_details")]
        public BalanceDetails BalanceDetails { get; set; }

        /// <summary>
        /// Information about attached Bank cards.
        /// </summary>
        [DataMember(Name = "cards_linked")]
        public List<CardsLinked> CardsLinked { get; set; }

        /// <summary>
        /// The list of connected services.
        /// </summary>
        [DataMember(Name = "services_additional")]
        public List<string> ServicesAdditional { get; set; }

        public override string ToString()
        {
            return String.Format("AccountId = {0}, balance={1}", Account, Balance);
        }

        /// <summary>
        /// account type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static AccountType GetAccountType(String type)
        {
            switch (type)
            {
                case "personal":
                    return Responses.AccountType.Personal;
                case "professional":
                    return Responses.AccountType.Professional;
                default:
                    return Responses.AccountType.Unknown;
            }
        }

        /// <summary>
        /// account status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static AccountStatus GetAccountStatus(String status)
        {
            switch (status)
            {
                case "anonymous":
                    return AccountStatus.Anonymous;
                case "named":
                    return AccountStatus.Named;
                case "identified":
                    return AccountStatus.Identified;
                default:
                    return AccountStatus.Unknown;
            }            
        }

        public AccountStatus Status
        {
            get { return GetAccountStatus(StatusOfAccount); }
        }

        public static string GetStatusDesc(AccountStatus status)
        {
            switch (status)
            {
                case AccountStatus.Anonymous:
                case AccountStatus.Named:
                case AccountStatus.Identified:
                    return String.Empty;
                default:
                    return String.Empty;
            }            
        }
    }
}