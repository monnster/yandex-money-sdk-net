using System;
using System.Runtime.Serialization;
using Yandex.Money.Api.Sdk.Responses.Base;

namespace Yandex.Money.Api.Sdk.Responses
{
    /// <summary>
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/incoming-transfer-accept-docpage/"/>
    /// </summary>
    public class IncomingTransferResult : ApiResultBase
    {
        [DataMember(Name = "protection_code_attempts_available")]
        public int ProtectionCodeAttemptsAvailable { get; set; }

        [DataMember(Name = "ext_action_uri")]
        public string ExtActionUri { get; set; }

        public override Uri ActionUri
        {
            get
            {
                Uri uri;

                if (Error == "ext_action_required")
                    return Uri.TryCreate(ExtActionUri, UriKind.RelativeOrAbsolute, out uri) ? uri : null;

                return base.ActionUri;
            }
        }

        public override ResponseStatus GetStatus()
        {
            var status = base.GetStatus();

            if (status == ResponseStatus.Refused && Error == "ext_action_required")
                return ResponseStatus.ExtActionRequired;

            switch (StatusName)
            {
                case "illegal_param_operation_id":
                    return ResponseStatus.Update;
                default:
                    return status;
            }
        }
    }
}
