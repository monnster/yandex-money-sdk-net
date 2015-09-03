using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Yandex.Money.Api.Sdk.Interfaces
{
    /// <summary>
    /// Describes API request.
    /// </summary>
    public interface IRequest
    {
		/// <summary>
		/// Gets HTTP request method (GET|POST).
		/// </summary>
		HttpMethod RequestMethod { get; }

        /// <summary>
        /// Relative url of the request.
        /// </summary>
        string RelativeUri { get; }

		/// <summary>
		/// Additional request parameters.
		/// </summary>
		Dictionary<string, string> RequestParams { get; } 

	    /// <summary>
	    /// Allows to append additional headers to request.
	    /// </summary>
	    /// <param name="headerCollection"></param>
	    void AppendRequestHeaders(HttpContentHeaders headerCollection);
    }
}