using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;
using Yandex.Money.Api.Sdk.Responses;
using Yandex.Money.Api.Sdk.Utils;

namespace Yandex.Money.Api.Sdk.Requests
{
	/// <summary>
	/// Get merchant form params.
	/// <see cref="http://tech.yandex.ru/money/doc/dg/reference/showcase-request-docpage/"/> 
	/// and
	/// <see cref="http://tech.yandex.ru/money/doc/dg/reference/showcase-submit-predefined-docpage/"/>
	/// </summary>
	public class ShowcaseFormParamsRequest : JsonRequest<ShowcaseFormParamsResponse>
	{
		private readonly Dictionary<string, string> _formParams;
		private readonly string _patternId;
		private readonly string _langCode;
		private readonly DateTime? _lastModified;
		
		/// <summary>
		/// Initializes new instance of <see cref="ShowcaseFormParamsRequest"/> class.
		/// </summary>
		/// <param name="patternId">Pattern identifier (merchant id) for which you'd like to get params.</param>
		/// <param name="formParams">In case of multi-step forms or mandatory merchant params put it to this dictionary.</param>
		/// <param name="langCode">Preferred language for results (ru|en)</param>
		/// <param name="lastModified">
		/// If you have a cached version of form specify date when it was 
		/// cached; in case form was updated you will get updated description. 
		/// Otherwise response will be set to null.
		/// </param>
		public ShowcaseFormParamsRequest(
			[NotNull] string patternId, 
			[CanBeNull] Dictionary<string, string> formParams,
			[CanBeNull] string langCode = null, 
			[CanBeNull] DateTime? lastModified = null)
		{
			Argument.NotNullOrEmpty(patternId, "Pattern identifier is required.");
			Argument.Require(Misc.IsLanguageSupported(langCode), "Unsupported lang code specified.");

			_patternId = patternId;
			_formParams = formParams ??  new Dictionary<string, string>();
			_langCode = langCode;
			_lastModified = lastModified;
		}

		public override HttpMethod RequestMethod
		{
			get
			{
				return _formParams.Any()
					? HttpMethod.Post
					: HttpMethod.Get;
			}
		}

		public override string RelativeUri
		{
			get { return @"api/showcase/" + _patternId; }
		}


		public override IEnumerable<KeyValuePair<string, string>> GetRequestParams()
		{
			return _formParams;
		}


		public override async Task<ShowcaseFormParamsResponse> Parse(HttpServerResponse response)
		{
			if (response.Status == HttpStatusCode.MultipleChoices)
			{
				var result = await base.Parse(response);

				result.ResponseUri = response.Headers.Location.ToString();

				return result;
			}
			if (response.Status == HttpStatusCode.MovedPermanently)
			{
				throw new IOException(string.Format("The form should be requested via new url: {0}", response.Headers.Location));
			}
			if (response.Status == HttpStatusCode.NotModified)
			{
				return null;
			}

			throw new InvalidOperationException("Unsupported response code from server.");
		}

		public override void AppendRequestHeaders(HttpContentHeaders headerCollection)
		{
			if (_lastModified.HasValue)
			{
				headerCollection.TryAddWithoutValidation("If-Modified-Since", _lastModified.Value.ToServerTime(true));
			}

			if (!string.IsNullOrEmpty(_langCode))
			{
				headerCollection.TryAddWithoutValidation("Accept-Language", _langCode);
			}
		}
	}
}