using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;
using Yandex.Money.Api.Sdk.Responses;
using Yandex.Money.Api.Sdk.Utils;
using KV = System.Collections.Generic.KeyValuePair<string, string>;

namespace Yandex.Money.Api.Sdk.Requests
{
	/// <summary>
	/// Searches merchants by name.
	/// <see cref="https://tech.yandex.ru/money/doc/dg/reference/showcase-search-docpage/"/>
	/// </summary>
	public class ShowcaseSearchRequest: JsonRequest<ShowcaseSearchResponse>
	{
		private readonly string _query;
		private readonly int _records;
		private readonly string _langCode;

		/// <summary>
		/// Initializes new instance of <see cref="ShowcaseSearchRequest"/> class.
		/// </summary>
		/// <param name="query">Search query.</param>
		/// <param name="records">Max number of records to retrieve. Default is 30.</param>
		/// <param name="langCode">Language code for results (ru|en).</param>
		public ShowcaseSearchRequest([NotNull] string query, int records = 30, string langCode = null)
		{
			Argument.NotNullOrEmpty(query, "Search query is required.");
			Argument.Require(records > 0, "Number of records to retrieve should be a positive number.");
			Argument.Require(Misc.IsLanguageSupported(langCode), "The language code supplied is not supported. Refer to docs.");

			_query = query;
			_records = records;
			_langCode = langCode;
		}

		public override string RelativeUri
		{
			get { return @"api/showcase-search"; }
		}

		public override HttpMethod RequestMethod
		{
			get { return HttpMethod.Get; }
		}

		public override IEnumerable<KeyValuePair<string, string>> GetRequestParams()
		{
			yield return new KV("query", _query);
			yield return new KV("records", _records.ToString(CultureInfo.InvariantCulture));
		}

		public override void AppendRequestHeaders(HttpContentHeaders headerCollection)
		{
			if (!string.IsNullOrEmpty(_langCode))
				headerCollection.TryAddWithoutValidation("Accept-Language", _langCode);
		}
	}
}