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

namespace Yandex.Money.Api.Sdk.Net
{
    /// <summary>
    /// Represents an interface implementation by default
    /// </summary>
    public class DefaultHttpPostClient : IDisposable, IHttpClient
    {
        private readonly IHostProvider _hostProvider;

        private readonly IAuthenticator _authenticator;

        private static readonly string[] Accepts = { @"application/xml", @"application/json" };

        private const string DefaultContentType = @"application/x-www-form-urlencoded";

        private HttpClient _httpClient;

        private HttpClientHandler _handler;

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Interfaces.IHttpClient interface.
        /// </summary>
        /// <param name="hostProvider">an instance of IHostProvider implementation</param>
        /// <param name="auth">an instance of IAuthenticator implementation</param>
        public DefaultHttpPostClient(IHostProvider hostProvider, IAuthenticator auth)
        {
            _hostProvider = hostProvider;
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
        public async Task<Stream> UploadDataAsync(IRequest request)
        {
            return await UploadDataAsync(request, CancellationToken.None);
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
        public async Task<Stream> UploadDataAsync(IRequest request, CancellationToken token)
        {
            if (_httpClient == null || request == null)
                throw new ArgumentNullException();

            var prms = new Dictionary<string, string>();

            request.AppendItemsTo(prms);

            _httpClient.DefaultRequestHeaders.Authorization =
                (_authenticator != null && !String.IsNullOrEmpty(_authenticator.Token))
                ? new AuthenticationHeaderValue(_authenticator.AuthenticationScheme, _authenticator.Token)
                : null;

            HttpContent content = new FormUrlEncodedContent(prms.Select(x => new KeyValuePair<string, string>(x.Key, x.Value)));
            content.Headers.ContentType = new MediaTypeHeaderValue(DefaultContentType);

            var response = await _httpClient.PostAsync(_hostProvider.BuildUri(request.RelativeUri), content, token);
  
            if (response == null)
                throw new IOException();

            if (response.StatusCode == HttpStatusCode.OK)
                return await response.Content.ReadAsStreamAsync();

            if (response.Headers == null || response.Headers.WwwAuthenticate == null)
                return null;

            var responseError = string.Empty;

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
