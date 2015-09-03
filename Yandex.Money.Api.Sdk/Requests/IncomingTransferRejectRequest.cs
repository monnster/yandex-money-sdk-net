using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;
using Yandex.Money.Api.Sdk.Responses;
using KV = System.Collections.Generic.KeyValuePair<string, string>;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Cancel incoming code-protected and on-demand transfers.
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/incoming-transfer-reject-docpage/"/>
    /// </summary>
    public class IncomingTransferRejectRequest : JsonRequest<IncomingTransferResult>
    {
	    private readonly string _operationId;

        /// <summary>
        /// Initializes a new instance of the <see cref="IncomingTransferRejectRequest"/> class.
        /// </summary>
		/// <param name="operationId">Operation identifier.</param>
        public IncomingTransferRejectRequest([NotNull]string operationId)
        {
			Argument.NotNullOrEmpty(operationId, "Operation id is required.");

	        _operationId = operationId;
        }

		#region Overrides

		public override string RelativeUri
		{
			get { return @"api/incoming-transfer-reject"; }
		}

		public override IEnumerable<KeyValuePair<string, string>> GetRequestParams()
		{
			yield return new KV("operation_id", _operationId);
		}
		
		#endregion
    }
}
