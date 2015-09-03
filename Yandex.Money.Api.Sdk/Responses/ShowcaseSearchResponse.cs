using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Yandex.Money.Api.Sdk.Responses
{
	[DataContract]
	public class ShowcaseSearchResponse
	{
		[DataMember(Name = "error")]
		public string Error { get; set; }

		[DataMember(Name = "result")]
		public ShowcaseSearchResult[] Results { get; set; }

		[DataMember(Name = "nextPage")]
		public bool NextPage { get; set; }
	}


	[DataContract]
	public class ShowcaseSearchResult
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "title")]
		public string Title { get; set; }

		[DataMember(Name = "url")]
		public string Url { get; set; }

		[DataMember(Name = "params")]
		public Dictionary<string, string> Params { get; set; }

		[DataMember(Name = "format")]
		public string Format { get; set; }

		[DataMember(Name = "is_form_description_available")]
		public bool IsFormDescriptionAvailable
		{
			get { return Format == "json"; }
		}
	}
}