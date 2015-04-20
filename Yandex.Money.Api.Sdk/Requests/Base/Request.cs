using System;
using System.Collections.Generic;
using System.IO;
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
        /// <param name="stream">the output stream of the http response</param>
        /// <returns></returns>
        public abstract Task<TResult> Parse(Stream stream);
    }

    /// <summary>
    /// this is the base class for all requests
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class Request<TResult> : ResponseParser<TResult>, IRequest
    {
        protected readonly IHttpClient Client;

        /// <summary>
        /// base constructor
        /// </summary>
        /// <param name="client"></param>
        protected Request(IHttpClient client)
        {
            Client = client;
        }

        /// <summary>
        /// perform the http request and return the result
        /// </summary>
        /// <exception cref="InsufficientScopeException">Thrown when the requested operation is that the token has no rights</exception>
        /// <exception cref="InvalidRequestException">Thrown when the HTTP request does not conform to protocol format</exception>
        /// <exception cref="InvalidTokenException">Thrown when nonexistent, expired, or revoked token specified</exception>
        /// <returns></returns>
        public async Task<TResult> Perform()
        {
            if (Client == null)
                throw new ArgumentNullException();

            var stream = await Client.UploadDataAsync(this);

            using (stream)
                return await Parse(stream);
        }

        #region interface implementation by default

        /// <summary>
        /// IRequest interface implementation
        /// </summary>
        public virtual string RelativeUri
        {
            get { return null; }
        }

        /// <summary>
        ///IRequest interface implementation
        /// </summary>
        /// <param name="items"></param>
        public virtual void AppendItemsTo(Dictionary<string, string> items)
        {
        }

        #endregion
    }
}