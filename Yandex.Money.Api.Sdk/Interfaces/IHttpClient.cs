using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Yandex.Money.Api.Sdk.Interfaces
{
    /// <summary>
    /// Represent interface to send http requests
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// Executes request
        /// </summary>
        /// <param name="request">an instance of IRequest implementation</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
        /// <returns></returns>
		Task<HttpServerResponse> GetResponseAsync(IRequest request, CancellationToken cancellationToken = default(CancellationToken));
    }

	/// <summary>
	/// Contains remote server response, headers and status code.
	/// </summary>
	public class HttpServerResponse
	{
		/// <summary>
		/// HTTP status code (successful only, 2xx or 3xx).
		/// </summary>
		public HttpStatusCode Status { get; set; }

		/// <summary>
		/// Response headers send by server.
		/// </summary>
		public HttpResponseHeaders Headers { get; set; }  

		/// <summary>
		/// Stream containing response body.
		/// </summary>
		public Stream Stream { get; set; }

	}
}