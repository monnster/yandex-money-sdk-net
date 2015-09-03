using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Yandex.Money.Api.Sdk.Exceptions;
using Yandex.Money.Api.Sdk.Interfaces;

namespace Yandex.Money.Api.Sdk.Requests.Base
{
    /// <summary>
    /// the http response parser
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ResponseParser<TResult>
    {
        /// <summary>
        /// the http response stream parser (template method)
        /// </summary>
        /// <param name="response">the output stream of the http response</param>
        /// <returns></returns>
		public abstract Task<TResult> Parse(HttpServerResponse response);
    }

    /// <summary>
    /// this is the base class for all requests
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class Request<TResult> : ResponseParser<TResult>, IRequest
    {

        /// <summary>
        /// perform the http request and return the result
        /// </summary>
		/// <param name="client">Http client.</param>
        /// <exception cref="InsufficientScopeException">Thrown when the requested operation is that the token has no rights</exception>
        /// <exception cref="InvalidRequestException">Thrown when the HTTP request does not conform to protocol format</exception>
        /// <exception cref="InvalidTokenException">Thrown when nonexistent, expired, or revoked token specified</exception>
        /// <returns></returns>
        public Task<TResult> Perform(IHttpClient client)
        {
	        return Perform(client, CancellationToken.None);
        }

	    /// <summary>
	    /// overload Perform method
	    /// </summary>
	    /// <param name="client">Http client.</param>
	    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
		/// <exception cref="InsufficientScopeException">Thrown when the requested operation is that the token has no rights</exception>
		/// <exception cref="InvalidRequestException">Thrown when the HTTP request does not conform to protocol format</exception>
		/// <exception cref="InvalidTokenException">Thrown when nonexistent, expired, or revoked token specified</exception>
	    /// <returns></returns>
	    public async Task<TResult> Perform(IHttpClient client, CancellationToken cancellationToken)
        {
			Argument.NotNull(client, "Http client is required.");

			var response = await client.GetResponseAsync(this, cancellationToken);

            using (response.Stream)
                return await Parse(response);
        }

        #region IRequest implementation 

		/// <summary>
		/// Most API methods require POST requests by default.
		/// Override it to GET when required.
		/// </summary>
		public virtual HttpMethod RequestMethod
		{
			get { return HttpMethod.Post; }
		}

        /// <summary>
        /// Request relative uri.
        /// </summary>
        public abstract string RelativeUri { get; }

		/// <summary>
		/// Override if request has any extra parameters.
		/// </summary>
	    public Dictionary<string, string> RequestParams
	    {
			get
			{
				var requestParams = new Dictionary<string, string>();
				foreach (var rp in GetRequestParams())
				{
					requestParams[rp.Key] = rp.Value;
				}

				return requestParams;
			}
	    } 

		/// <summary>
		/// Override if you need extra request headers.
		/// </summary>
	    public virtual void AppendRequestHeaders(HttpContentHeaders headerCollection)
	    {
	    }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
	    public virtual IEnumerable<KeyValuePair<string, string>> GetRequestParams()
	    {
		    yield break;
	    }

	    #endregion
    }
}