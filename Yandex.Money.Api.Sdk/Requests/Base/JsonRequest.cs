using System;
using System.IO;
using System.Threading.Tasks;
using Yandex.Money.Api.Sdk.Interfaces;

namespace Yandex.Money.Api.Sdk.Requests.Base
{
    /// <summary>
    /// basic constructor for http requests that return results in json format
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class JsonRequest<TResult> : Request<TResult>
    {
        private readonly IGenericSerializer<TResult> _jsonSerializer;

        /// <summary>
        /// basic constructor for http requests that return results in json format
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer"></param>
        public JsonRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer)
            : base(client)
        {
            _jsonSerializer = jsonSerializer;
        }

        public override Task<TResult> Parse(Stream stream)
        {
            return Task.Factory.StartNew(() =>
            {
                if (_jsonSerializer == null || stream == null)
                    throw new ArgumentNullException();

                return _jsonSerializer.Deserialize(stream);
            });
        }
    }
}