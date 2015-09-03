using System;
using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Requests.Base;
using Yandex.Money.Api.Sdk.Responses;
using KV = System.Collections.Generic.KeyValuePair<string, string>;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Exchange temporary token with permanent authorization token.
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/obtain-access-token-docpage/"/>
    /// </summary>
    public class TokenRequest : JsonRequest<TokenResult>
    {
	    private readonly string _code;
	    private readonly string _clientId;
	    private readonly string _redirectUri;
	    private readonly string _clientSecret;
	    private readonly string _grantType = "authorization_code";

		/// <summary>
		/// Initializes new instance of <see cref="TokenRequest"/> class.
		/// </summary>
		/// <param name="code">Temporary token.</param>
		/// <param name="clientId">
		/// Client id assigned to your application.
		/// <see cref="http://tech.yandex.ru/money/doc/dg/tasks/register-client-docpage/"/>
		/// </param>
		/// <param name="redirectUri">Where to redirect user after successfull authorization.</param>
		/// <param name="clientSecret">A secret word for verifying the application's authenticity.</param>
	    public TokenRequest([NotNull]string code, [NotNull]string clientId, [CanBeNull]string redirectUri, [CanBeNull]string clientSecret)
	    {
			Argument.NotNullOrEmpty(code, "Temporary token is required.");
			Argument.NotNullOrEmpty(clientId, "Client identifier is required.");

		    _code = code;
		    _clientId = clientId;
		    _redirectUri = redirectUri;
		    _clientSecret = clientSecret;
	    }

	    public override string RelativeUri
        {
            get { return @"oauth/token"; }
        }

	    public override IEnumerable<KV> GetRequestParams()
	    {
		    yield return new KV("client_id", _clientId);
			yield return new KV("code", _code);
			yield return new KV("grant_type", _grantType);

			if(!string.IsNullOrEmpty(_redirectUri))
				yield return new KV("redirect_uri", Uri.EscapeDataString(_redirectUri));

			if(!string.IsNullOrEmpty(_clientSecret))
				yield return new KV("client_secret", Uri.EscapeDataString(_clientSecret));
	    }
    }
}
