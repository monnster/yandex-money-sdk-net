using System;
using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Utils;
using System.Runtime.Serialization;

namespace Yandex.Money.Api.Sdk.Responses.Base
{
    /// <summary>
    /// the basis of all responses
    /// </summary>
    [DataContract]
    public class ResultBase : RequestParams
    {
        /// <summary>
        /// Error code. Present if an error occurred.
        /// </summary>
        [DataMember(Name = "error")]
        public string Error { get; set; }

        /// <summary>
        /// the response has error code
        /// </summary>
        public bool HasError
        {
            get { return !String.IsNullOrEmpty(Error); }
        }

        /// <summary>
        /// Error code
        /// </summary>
        /// <returns></returns>
        public Error ErrorCode()
        {
            return Errors.GetError(Error);
        }
    }

    /// <summary>
    /// the basis of all responses, including the response status
    /// </summary>
    [DataContract]
    public class ApiResultBase : ResultBase
    {
        /// <summary>
        /// Operation result code
        /// </summary>
        [DataMember(Name = "status")]
        public string StatusName { get; set; }

        /// <summary>
        /// The address to send the user to in order to complete necessary actions if the ext_action_required error occurs.
        /// </summary>
        public virtual Uri ActionUri
        {
            get { return null; }
        }

        /// <summary>
        /// Operation result code
        /// </summary>
        public ResponseStatus Status
        {
            get { return GetStatus(); }
        }

        /// <summary>
        /// Operation processing result codes
        /// </summary>
        /// <returns></returns>
        public virtual ResponseStatus GetStatus()
        {
            switch (StatusName)
            {
                case "success":
                    return ResponseStatus.Success;
                case "refused":
                    return ResponseStatus.Refused;
                case "in_progress":
                    return ResponseStatus.InProgress;
                default:
                    return ResponseStatus.Unknown;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum Error
    {
        /// <summary>
        /// Authorization request was declined by the user
        /// </summary>
        AccessDenied,
        /// <summary>
        /// The user's account has been blocked
        /// </summary>
        AccountBlocked,
        /// <summary>
        /// 
        /// </summary>
        AlreadyAccepted,
        /// <summary>
        /// Authorization of the payment was refused
        /// </summary>
        AlreadyRejected,
        /// <summary>
        /// 
        /// </summary>
        AuthorizationReject,
        /// <summary>
        /// Invalid or non-existent request_id specified
        /// </summary>
        ContractNotFound,
        /// <summary>
        /// 
        /// </summary>
        DataExpired,
        /// <summary>
        /// 
        /// </summary>
        ExtActionRequired,
        /// <summary>
        /// 
        /// </summary>
        FavoriteDuplicate,
        /// <summary>
        /// Invalid value for the amount parameter
        /// </summary>
        IllegalParamAmount,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamAmountDue,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamClientID,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamComment,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamCsc,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamExpirePeriod,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamExtAuthFailUri,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamExtAuthSuccessUri,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamFavoriteID,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamFrom,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamInstanceID,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamLabel,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamMessage,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamMoneySourceToken,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamOperationID,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamProtectionCode,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamQuery,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamRecords,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamRequestID,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamStartRecord,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamTill,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamTitle,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamTo,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParamType,
        /// <summary>
        /// Invalid value for...
        /// </summary>
        IllegalParams,
        /// <summary>
        /// The access_token could not be issued
        /// </summary>
        InvalidGrant,
        /// <summary>
        /// 
        /// </summary>
        InvalidImage,
        /// <summary>
        /// The request is missing required parameters, or parameters have unsupported or invalid values
        /// </summary>
        InvalidRequest,
        /// <summary>
        /// The scope parameter is missing, or it has an invalid value or a contradiction in logic
        /// </summary>
        InvalidScope,
        /// <summary>
        /// The payer's account does not have sufficient funds to make the payment
        /// </summary>
        NotEnoughFunds,
        /// <summary>
        /// One of the operation limits was exceeded
        /// </summary>
        LimitExceeded,
        /// <summary>
        /// 
        /// </summary>
        MoneySourceNotAvailable,
        /// <summary>
        /// The transfer recipient was not found
        /// </summary>
        PayeeNotFound,
        /// <summary>
        /// 
        /// </summary>
        PaymentExpired,
        /// <summary>
        /// The merchant refused to accept the payment
        /// </summary>
        PaymentRefused,
        /// <summary>
        /// Technical error; repeat the operation again later
        /// </summary>
        TechnicalError,
        /// <summary>
        /// 
        /// </summary>
        TooManyRecords,
        /// <summary>
        /// The client_id or client_secret value is invalid
        /// </summary>
        UnauthorizedClient,
        /// <summary>
        /// 
        /// </summary>
        Unknown
    }

    /// <summary>
    ///  known error codes
    /// </summary>
    public class Errors
    {
        /// <summary>
        ///  a list of all known error codes
        /// </summary>
        public static Dictionary<string, Error> Codes = new Dictionary<string, Error>
        {
            {"access_denied", Error.AccessDenied},
            {"account_blocked", Error.AccountBlocked},
            {"already_accepted", Error.AlreadyAccepted},
            {"already_rejected", Error.AlreadyRejected},
            {"authorization_reject", Error.AuthorizationReject},
            {"contract_not_found", Error.ContractNotFound},
            {"data_expired", Error.DataExpired},
            {"ext_action_required", Error.ExtActionRequired},
            {"favourite_duplicate", Error.FavoriteDuplicate},
            {"illegal_param_amount", Error.IllegalParamAmount},
            {"illegal_param_amount_due", Error.IllegalParamAmountDue},
            {"illegal_param_client_id", Error.IllegalParamClientID},
            {"illegal_param_comment", Error.IllegalParamComment},
            {"illegal_param_csc", Error.IllegalParamCsc},
            {"illegal_param_expire_period", Error.IllegalParamExpirePeriod},
            {"illegal_param_ext_auth_fail_uri", Error.IllegalParamExtAuthFailUri},
            {"illegal_param_ext_auth_success_uri", Error.IllegalParamExtAuthSuccessUri},
            {"illegal_param_favourite_id", Error.IllegalParamFavoriteID},
            {"illegal_param_from", Error.IllegalParamFrom},
            {"illegal_param_instance_id", Error.IllegalParamInstanceID},
            {"illegal_param_label", Error.IllegalParamLabel},
            {"illegal_param_message", Error.IllegalParamMessage},
            {"illegal_param_money_source_token", Error.IllegalParamMoneySourceToken},
            {"illegal_param_operation_id", Error.IllegalParamOperationID},
            {"illegal_param_protection_code", Error.IllegalParamProtectionCode},
            {"illegal_param_query", Error.IllegalParamQuery},
            {"illegal_param_records", Error.IllegalParamRecords},
            {"illegal_param_request_id", Error.IllegalParamRequestID},
            {"illegal_param_start_record", Error.IllegalParamStartRecord},
            {"illegal_param_till", Error.IllegalParamTill},
            {"illegal_param_title", Error.IllegalParamTitle},
            {"illegal_param_to", Error.IllegalParamTo},
            {"illegal_param_type", Error.IllegalParamType},
            {"illegal_params", Error.IllegalParams},
            {"invalid_grant", Error.InvalidGrant},
            {"invalid_image", Error.InvalidImage},
            {"invalid_request", Error.InvalidRequest},
            {"invalid_scope", Error.InvalidScope},
            {"not_enough_funds", Error.NotEnoughFunds},
            {"limit_exceeded", Error.LimitExceeded},
            {"money_source_not_available", Error.MoneySourceNotAvailable},
            {"payee_not_found", Error.PayeeNotFound},
            {"payment_expired", Error.PaymentExpired},
            {"payment_refused", Error.PaymentRefused},
            {"technical_error", Error.TechnicalError},
            {"too_many_records", Error.TooManyRecords},
            {"unauthorized_client", Error.UnauthorizedClient},
            {"unknown", Error.Unknown}
        };

        /// <summary>
        /// get the error code by its code name
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Error GetError(String code)
        {
            return (String.IsNullOrEmpty(code) || !Codes.ContainsKey(code)) ? Error.Unknown : Codes[code];
        }

        /// <summary>
        /// override it to show user-friendly message
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual string ErrorMesage(Error code)
        {
            return null;
        }
    }

}