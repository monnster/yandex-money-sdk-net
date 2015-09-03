using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;
using Yandex.Money.Api.Sdk.Responses;
using KV = System.Collections.Generic.KeyValuePair<string, string>;

namespace Yandex.Money.Api.Sdk.Requests
{
	/// <summary>
	/// Validate form parameters filled by user.
	/// <see cref="http://tech.yandex.ru/money/doc/dg/reference/showcase-submit-docpage/"/>
	/// </summary>
	public class ShowcaseFormValidateRequest: JsonRequest<ShowcaseFormValidateResponse>
	{
		private readonly Dictionary<string, string> _formParams; // = new Dictionary<string, string>();
		private readonly Uri _requestUri;

		/// <summary>
		/// Initializes new instance of <see cref="ShowcaseFormValidateRequest"/> class.
		/// </summary>
		/// <param name="formParams">Form parameters (hidden + user edited).</param>
		/// <param name="validationUri">Uri where to send form for validation (since each from has it's own validation url).</param>
		public ShowcaseFormValidateRequest(Dictionary<string, string> formParams, string validationUri)
		{
			Argument.NotNull(formParams, "Form parameters are required.");
			Argument.NotNullOrEmpty(validationUri, "Validation uri is required.");

			_formParams = formParams;
			_requestUri = new Uri(validationUri);
		}

		public override string RelativeUri
		{
			get { return _requestUri.LocalPath; }
		}

		public override IEnumerable<KeyValuePair<string, string>> GetRequestParams()
		{
			return _formParams;
		}

		public override async Task<ShowcaseFormValidateResponse> Parse(HttpServerResponse response)
		{
			bool isFinalStep = response.Status == HttpStatusCode.OK;
			var result = await base.Parse(response);

			result.IsFinalStep = isFinalStep;
			result.Success = true; // since we cannot handle HTTP 400 status here

			return result;
		}
	}
}