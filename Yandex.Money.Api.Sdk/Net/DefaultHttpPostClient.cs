using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Yandex.Money.Api.Sdk.Exceptions;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Utils;

namespace Yandex.Money.Api.Sdk.Net
{
    /// <summary>
    /// Represents an interface implementation by default
    /// </summary>
    public class DefaultHttpPostClient : IDisposable, IHttpClient
    {
        private readonly IHostProvider _hostProvider;

        private readonly IAuthenticator _authenticator;

        private static readonly string[] Accepts = { @"application/json", @"application/xml",  };

        private const string DefaultContentType = @"application/x-www-form-urlencoded";

        private HttpClient _httpClient;

        private HttpClientHandler _handler;

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultHttpPostClient"/> class. Requests will be done without authentication.
		/// </summary>
	    public DefaultHttpPostClient()
			: this(null, null)
	    {
	    }

	    /// <summary>
		/// Initializes a new instance of the <see cref="DefaultHttpPostClient"/> class.
		/// </summary>
		/// <param name="auth">Instance of IAuthenticator. If not specified request will not use authentication.</param>
	    public DefaultHttpPostClient([CanBeNull] IAuthenticator auth) 
			: this(null, auth)
	    {
	    }

	    /// <summary>
        /// Initializes a new instance of the <see cref="DefaultHttpPostClient"/> class.
        /// </summary>
        /// <param name="hostProvider">Instance of IHostProvider. If not specified, default one will be used.</param>
        /// <param name="auth">Instance of IAuthenticator. If not specified request will not use authentication.</param>
        public DefaultHttpPostClient([CanBeNull] IHostProvider hostProvider, [CanBeNull] IAuthenticator auth)
        {
            _hostProvider = hostProvider ?? new DefaultHostsProvider();
            _authenticator = auth;

            Init();
        }

        private void Init()
        {
            try
            {
                _handler = new HttpClientHandler
                {
                    CookieContainer = new CookieContainer(),
                    AllowAutoRedirect = false,
                    UseCookies = true
                };

                _httpClient = new HttpClient(_handler);

                foreach (var accept in Accepts)
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));

                _httpClient.DefaultRequestHeaders.ExpectContinue = false;
            }
            catch
            {
                _httpClient = null;
            }
        }

        public void Dispose()
        {
            if (_httpClient == null)
                return;

            _httpClient.Dispose();
            _httpClient = null;
        }

        /// <summary>
        /// performs the http request and returns the result 
        /// </summary>
        /// <param name="request">an instance of IRequest implementation</param>
        /// <returns></returns>
		public async Task<HttpServerResponse> GetResponseAsync(IRequest request)
        {
			return await GetResponseAsync(request, CancellationToken.None);
        }

        /// <summary>
        /// performs the http request and returns the result 
        /// </summary>
        /// <param name="request">an instance of IRequest implementation</param>
        /// <param name="token">a cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidRequestException"></exception>
        /// <exception cref="InvalidTokenException"></exception>
        /// <exception cref="InsufficientScopeException"></exception>
		public async Task<HttpServerResponse> GetResponseAsync(IRequest request, CancellationToken token)
        {
			if(null == _httpClient)
				throw new InvalidOperationException("HttpClient is either disposed or was not created successfully.");

            if (null == request)
                throw new ArgumentNullException("request", "Request is required.");

            var prms = request.RequestParams ?? new Dictionary<string, string>();

            _httpClient.DefaultRequestHeaders.Authorization =
                (_authenticator != null && !string.IsNullOrEmpty(_authenticator.Token))
                ? new AuthenticationHeaderValue(_authenticator.AuthenticationScheme, _authenticator.Token)
                : null;

            HttpContent content = new FormUrlEncodedContent(prms);
            content.Headers.ContentType = new MediaTypeHeaderValue(DefaultContentType);

	        var response = request.RequestMethod == HttpMethod.Post
		        ? await _httpClient.PostAsync(_hostProvider.BuildUri(request.RelativeUri), content, token)
		        : await _httpClient.GetAsync(_hostProvider.BuildUri(request.RelativeUri) + "?" + prms.ToQueryString(), token);
  
            if (response == null)
                throw new IOException("Unable to get response from server.");

			// according to yandex API 2xx and 3xx are successful codes.
	        if ((int)response.StatusCode < 400)
			{
				return new HttpServerResponse
		        {
			        Status = response.StatusCode,
			        Headers = response.Headers,
			        Stream = await response.Content.ReadAsStreamAsync(),
		        };
			}

            if (response.Headers == null || response.Headers.WwwAuthenticate == null)
                return null;

            var responseError = "Error response received from server, status code " + response.StatusCode;

            var authenticationHeaderValue = response
                .Headers
                .WwwAuthenticate
                .FirstOrDefault(x => x.Scheme == _authenticator.AuthenticationScheme);

            if (authenticationHeaderValue != null)
                responseError = authenticationHeaderValue.Parameter;

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new InvalidRequestException(responseError);

                case HttpStatusCode.Unauthorized:
                    throw new InvalidTokenException(responseError);

                case HttpStatusCode.Forbidden:
                    throw new InsufficientScopeException(responseError);

                default:
                    throw new IOException(responseError);
            }
        }
    }
}
