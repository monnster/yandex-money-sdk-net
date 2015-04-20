using System;
using System.Linq;

namespace Yandex.Money.Api.Sdk.Authorization
{

    /// <summary>
    /// Represents the money-source permissions
    /// <see cref="http://tech.yandex.ru/money/doc/dg/concepts/protocol-rights-docpage/"/>
    /// </summary>
    [Flags]
    public enum Source
    {
        None = 0,
        /// <summary>
        /// The requested method for making a payment with the user's bank cards that are linked to the account
        /// </summary>
        Card = 1,
        /// <summary>
        /// The requested method for making a payment from a Yandex.Money account
        /// </summary>
        Wallet = 2,
        /// <summary>
        /// Both
        /// </summary>
        CardAndWallet = Card | Wallet
    }

    /// <summary>
    /// Represents a list of requested permissions 
    /// <see cref="http://tech.yandex.ru/money/doc/dg/concepts/protocol-rights-docpage/"/>
    /// </summary>
    public class Scopes
    {
        /// <summary>
        /// permission
        /// </summary>
        public static string AccountInfo
        {
            get { return @"account-info"; }
        }

        /// <summary>
        /// permission
        /// </summary>
        public static string OperationHistory
        {
            get { return @"operation-history"; }
        }

        /// <summary>
        /// permission
        /// </summary>
        public static string OperationDetails
        {
            get { return @"operation-details"; }
        }

        /// <summary>
        /// permission
        /// </summary>
        public static string IncomingTransfers
        {
            get { return @"incoming-transfers"; }
        }

        /// <summary>
        /// permission
        /// </summary>
        /// <returns></returns>
        public static string PaymentToShop()
        {
            return @"payment-shop";
        }

        /// <summary>
        /// permission
        /// </summary>
        /// <returns></returns>
        public static string PaymentP2P()
        {
            return @"payment-p2p";
        }

        /// <summary>
        /// permission
        /// </summary>
        /// <param name="patternId"></param>
        /// <returns></returns>
        public static string PaymentToPattern(String patternId)
        {
            return String.Format("payment.to-pattern(\"{0}\")", patternId);
        }

        /// <summary>
        /// permission
        /// </summary>
        /// <param name="patternId"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static string PaymentToPattern(String patternId, String sum)
        {
            return String.Format("{0}.Limit(,{1})", PaymentToPattern(patternId), sum);
        }

        /// <summary>
        /// permission
        /// </summary>
        /// <param name="patternId"></param>
        /// <param name="sum">Total amount for all payments over the period in duration, in the currency used for the account.</param>
        /// <param name="duration">Period of time, in days</param>
        /// <returns></returns>
        public static string PaymentToPattern(String patternId, String sum, String duration)
        {
            return String.Format("{0}.Limit({1},{2})", PaymentToPattern(patternId), duration, sum);
        }

        /// <summary>
        /// permission
        /// </summary>
        /// <param name="to">The transfer recipient's account ID, phone number linked to the account, or email. Mandatory parameter</param>
        /// <returns></returns>
        public static string PaymentToAccount(String to)
        {
            return String.Format("payment.to-account(\"{0}\")", to);
        }

        /// <summary>
        /// permission
        /// </summary>
        /// <param name="to">The transfer recipient's account ID, phone number linked to the account, or email. Mandatory parameter</param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static string PaymentToAccount(String to, String sum)
        {
            return String.Format("{0}.Limit(,{1})", PaymentToAccount(to), sum);
        }

        /// <summary>
        /// permission
        /// </summary>
        /// <param name="to">The transfer recipient's account ID, phone number linked to the account, or email. Mandatory parameter</param>
        /// <param name="sum">Total amount for all payments over the period in duration, in the currency used for the account.</param>
        /// <param name="duration">Period of time, in days</param>
        /// <returns></returns>
        public static string PaymentToAccount(String to, String sum, String duration)
        {
            return String.Format("{0}.Limit({1},{2})", PaymentToAccount(to), duration, sum);
        }

        /// <summary>
        /// permission
        /// </summary>
        /// <param name="sum">Total amount for all payments over the period in duration, in the currency used for the account.</param>
        /// <returns></returns>
        public static string PaymentToShop(String sum)
        {
            return String.Format("payment-shop.Limit(,{0})", sum);
        }

        /// <summary>
        /// permission
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="duration">Period of time, in days</param>
        /// <returns></returns>
        public static string PaymentToShop(String sum, String duration)
        {
            return String.Format("payment-shop.Limit({0},{1})", duration, sum);
        }

        /// <summary>
        /// permission
        /// </summary>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static string PaymentP2P(String sum)
        {
            return String.Format("payment-p2p.Limit(,{0})", sum);
        }

        /// <summary>
        /// permission
        /// </summary>
        /// <param name="sum">Total amount for all payments over the period in duration, in the currency used for the account.</param>
        /// <param name="duration">Period of time, in days</param>
        /// <returns></returns>
        public static string PaymentP2P(String sum, String duration)
        {
            return String.Format("payment-p2p.Limit({0},{1})", duration, sum);
        }

        /// <summary>
        /// Represents available payment methods
        /// </summary>
        /// <param name="source">payment method</param>
        /// <returns></returns>
        public static string MoneySource(Source source)
        {
            if (source == Source.None)
                return null;

            var s = String.Empty;

            if ((source & Source.Card) == Source.Card)
                s = "\"card\"";

            if ((source & Source.Wallet) == Source.Wallet)
                s = String.IsNullOrEmpty(s) ? "\"wallet\"" : String.Format("{0}, {1}", s, "\"wallet\"");

            return String.Format("money-source({0})", s);
        }

        /// <summary>
        ///  Compose the string of permissions to pass it  as request query string
        /// </summary>
        /// <param name="scopes"></param>
        /// <returns></returns>
        public static string Compose(string[] scopes)
        {
            return scopes.Aggregate(String.Empty, (x, y) => String.Format("{0} {1}", x, y)).Trim();
        }
    }
}