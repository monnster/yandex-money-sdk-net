using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;
using Yandex.Money.Api.Sdk.Responses;
using KV = System.Collections.Generic.KeyValuePair<string, string>;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Register application instance.
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/instance-id-docpage/"/>
    /// </summary>
    public class InstanceIdRequest : JsonRequest<InstanceIdResult>
    {
	    private readonly string _clientId;

		/// <summary>
		/// Initializes new instance of <see cref="InstanceIdRequest"/> class.
		/// </summary>
		/// <param name="clientId">Your client app identifier.</param>
	    public InstanceIdRequest(string clientId)
	    {
			Argument.NotNullOrEmpty(clientId, "Client id is required.");

		    _clientId = clientId;
	    }

	    public override string RelativeUri
        {
            get { return @"api/instance-id"; }
        }

	    public override IEnumerable<KeyValuePair<string, string>> GetRequestParams()
	    {
		    yield return new KV("client_id", _clientId);
	    }

    }
}
