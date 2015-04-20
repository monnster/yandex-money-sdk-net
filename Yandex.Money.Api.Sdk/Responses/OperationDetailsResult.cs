using System;
using System.Runtime.Serialization;
using Yandex.Money.Api.Sdk.Responses.Base;
using Yandex.Money.Api.Sdk.Utils;

namespace Yandex.Money.Api.Sdk.Responses
{
    /// <summary>
    /// detailed information about a particular operation from the history
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/operation-details-docpage/"/>
    /// </summary>
    [DataContract]
    public class OperationDetailsResult : ApiResultBase
    {
        /// <summary>
        /// Operation ID
        /// </summary>
        [DataMember(Name = "operation_id")]
        public string OperationId { get; set; }

        /// <summary>
        /// Payment Pattern ID. Present only for payments
        /// </summary>
        [DataMember(Name = "pattern_id")]
        [ParamName("pattern_id")]
        public string PatternID { get; set; }

        /// <summary>
        /// Amount of the operation
        /// </summary>
        [DataMember(Name = "amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Amount to receive
        /// </summary>
        [DataMember(Name = "amount_due")]
        [ParamName("amount_due")]
        public double AmountDue { get; set; }

        /// <summary>
        /// Commission amount
        /// </summary>
        [DataMember(Name = "fee")]
        public double Fee { get; set; }

        /// <summary>
        /// Direction of financial transaction
        /// </summary>
        [DataMember(Name = "direction")]
        public string Direction { get; set; }

        /// <summary>
        /// Account number that funds were transferred from
        /// </summary>
        [DataMember(Name = "sender")]
        public string Sender { get; set; }

        /// <summary>
        /// ID of the transfer recipient
        /// </summary>
        [DataMember(Name = "recipient")]
        [ParamName("to")]
        public string Recipient { get; set; }

        /// <summary>
        /// Type of ID used for the transfer recipient
        /// </summary>
        [DataMember(Name = "recipient_type")]
        public string RecipientType { get; set; }

        /// <summary>
        /// Operation timestamp
        /// </summary>
        [DataMember(Name = "datetime")]
        public string DateTime { get; set; }

        /// <summary>
        /// Date and time when the secret code expires.
        /// </summary>
        [DataMember(Name = "expires")]
        [ParamName("expires")]
        public string Expires { get; set; }

        /// <summary>
        /// Date and time when a transfer protected by a secret code was accepted or canceled
        /// </summary>
        [DataMember(Name = "answer_datetime")]
        public string AnswerDateTime { get; set; }

        /// <summary>
        /// Brief description of the operation
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Message for the transfer recipient
        /// </summary>
        [DataMember(Name = "message")]
        [ParamName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Comments on the transfer 
        /// </summary>
        [DataMember(Name = "comment")]
        [ParamName("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Payment label
        /// </summary>
        [DataMember(Name = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Detailed payment description
        /// </summary>
        [DataMember(Name = "details")]
        public string Details { get; set; }

        /// <summary>
        /// Secret code
        /// </summary>
        [DataMember(Name = "protection_code")]
        public string ProtectionCode { get; set; }

        /// <summary>
        /// The type of operation
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The transfer is protected by a secret code
        /// </summary>
        [DataMember(Name = "codepro")]
        [ParamName("codepro")]
        public bool Codepro { get; set; }

        /// <summary>
        /// Data about a digital product 
        /// </summary>
        [DataMember(Name = "digital_goods")]
        public DigitalGoods DigitalGoods { get; set; }

        public OperationType OperationType { get { return GetOperationType(Type); } }

        public MoneyFlow MoneyFlow { get { return GetMoneyFlow(Direction); } }

        public static OperationType GetOperationType(OperationDetailsResult detailsResult)
        {
            if (detailsResult == null)
                return OperationType.Unknown;

            return (detailsResult.OperationType == OperationType.IncomingTransfer && detailsResult.Status == ResponseStatus.InProgress)
                ? OperationType.HoldForPickupTransfer
                : detailsResult.OperationType;
        }

        public static OperationType GetOperationType(String type)
        {
            switch (type)
            {
                case "deposition":
                    return OperationType.Deposition;
                case "incoming-transfer":
                    return OperationType.IncomingTransfer;
                case "incoming-transfer-protected":
                    return OperationType.IncomingTransferProtected;
                case "outgoing-transfer":
                    return OperationType.OutgoingTransfer;
                case "payment-shop":
                    return OperationType.PaymentShop;
                default:
                    return OperationType.Unknown;
            }
        }

        public static String GetOperationName(OperationType type)
        {
            switch (type)
            {
                case OperationType.Deposition:
                    return "deposition";
                case OperationType.IncomingTransfer:
                    return "incoming-transfer";
                case OperationType.IncomingTransferProtected:
                    return "incoming-transfer-protected";
                case OperationType.OutgoingTransfer:
                    return "outgoing-transfer";
                case OperationType.PaymentShop:
                    return "payment-shop";
                case OperationType.Out:
                    return "payment";
                case OperationType.UnacceptedIn:
                    return "incoming-transfers-unaccepted";
                default:
                    return null;
            }
        }

        public MoneyFlow GetMoneyFlow(String direction)
        {
            switch (direction)
            {
                case "in":
                    return MoneyFlow.In;
                case "out":
                    return MoneyFlow.Out;
                default:
                    return MoneyFlow.Unknown;
            }            
        }
    }
}