using System;
using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;
using Yandex.Money.Api.Sdk.Responses;
using KV = System.Collections.Generic.KeyValuePair<string, string>;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Incoming transfers, protected by code, and transfer demand.
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/incoming-transfer-accept-docpage/"/>
    /// </summary>
    public class IncomingTransferAcceptRequest : JsonRequest<IncomingTransferResult>
    {
	    private readonly string _operationId;
	    private readonly string _protectionCode;

        /// <summary>
        /// Initializes a new instance of <see cref="IncomingTransferAcceptRequest"/> class.
        /// </summary>
        /// <param name="operationId">Operation identifier</param>
        /// <param name="protectionCode">[optional] special code in case money transfer if protected by code.</param>
        public IncomingTransferAcceptRequest([NotNull] string operationId, [CanBeNull]string protectionCode = null)
        {
			Argument.NotNullOrEmpty(operationId, "Operation id is required.");

	        _operationId = operationId;
	        _protectionCode = protectionCode;
        }

		#region Overrides

		/// <summary>
		/// Represents an interface implementation
		/// </summary>
		public override string RelativeUri
		{
			get { return @"api/incoming-transfer-accept"; }
		}

		public override IEnumerable<KeyValuePair<string, string>> GetRequestParams()
		{
			yield return new KV("operation_id", _operationId);

			if (!string.IsNullOrEmpty(_protectionCode))
				yield return new KV("protection_code", _protectionCode);
		}
		
		#endregion
    }
}