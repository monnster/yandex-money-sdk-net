using System;
using System.IO;
using System.Threading.Tasks;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Utils;

namespace Yandex.Money.Api.Sdk.Requests.Base
{
    /// <summary>
    /// Abstract implementation of Request which uses json serializer.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class JsonRequest<TResult> : Request<TResult>
    {
        private readonly IGenericSerializer<TResult> _serializer;

		/// <summary>
		/// Default ctor for JsonRequest.
		/// </summary>
	    protected JsonRequest()
	    {
			_serializer = new JsonSerializer<TResult>();
	    }

	    /// <summary>
        /// Default ctor which allows to replace standard serializer.
        /// </summary>
        /// <param name="jsonSerializer"></param>
	    protected JsonRequest(IGenericSerializer<TResult> jsonSerializer)
        {
			Argument.NotNull(jsonSerializer,  "Serailizer instance is required.");

			_serializer = jsonSerializer;
        }

		public override Task<TResult> Parse(HttpServerResponse response)
        {
			Argument.NotNull(response, "Server response not present.");

            return Task.Factory.StartNew(() => _serializer.Deserialize(response.Stream));
        }
    }
}