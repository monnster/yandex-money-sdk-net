using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Yandex.Money.Api.Sdk.Responses
{
	[DataContract]
	public class ShowcaseFormValidateResponse
	{
		[DataMember(Name = "is_final_step")]
		public bool IsFinalStep { get; set; }

		[DataMember(Name = "success")]
		public bool Success { get; set; }

		[DataMember(Name = "redirect_uri")]
		public string RedirectUri { get; set; }

		[DataMember(Name = "params")]
		public Dictionary<string, string> Params { get; set; }

		[DataMember(Name = "error")]
		public ShowcaseFormValidationError[] Errors { get; set; }


	}
	
	[DataContract]
	public class ShowcaseFormValidationError
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "alert")]
		public string Alert { get;set; }
	}
}