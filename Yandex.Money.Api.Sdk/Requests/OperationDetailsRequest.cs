using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;
using Yandex.Money.Api.Sdk.Responses;
using KV = System.Collections.Generic.KeyValuePair<string, string>;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Obtaining detailed information about the operation from the history.
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/operation-details-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class OperationDetailsRequest: JsonRequest<OperationDetailsResult>
    {
	    private readonly string _operationId;
       
		/// <summary>
		/// Initializes new instance of <see cref="OperationDetailsRequest"/> class.
		/// </summary>
		/// <param name="operationId">Operation identifier you want to get details for.</param>
	    public OperationDetailsRequest(string operationId)
	    {
			Argument.NotNullOrEmpty(operationId, "Operation id is required.");

		    _operationId = operationId;
	    }

		#region Overrides

		public override string RelativeUri
		{
			get { return @"api/operation-details"; }
		}

		public override IEnumerable<KeyValuePair<string, string>> GetRequestParams()
		{
			yield return new KV("operation_id", _operationId);
		}

		#endregion
    }
}
