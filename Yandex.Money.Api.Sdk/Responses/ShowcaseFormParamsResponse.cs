using System.Collections.Generic;
using System.Runtime;
using System.Runtime.Serialization;
using Yandex.Money.Api.Sdk.Responses.Form;

namespace Yandex.Money.Api.Sdk.Responses
{
	[DataContract]
	public enum MoneySources
	{
		[EnumMember(Value = "wallet")]
		Wallet,

		[EnumMember(Value = "cards")]
		Cards,

		[EnumMember(Value = "payment-card")]
		Payment_card,

		[EnumMember(Value = "cash")]
		Cash,
	}

	[DataContract]
	public class ShowcaseFormParamsResponse
	{
		[DataMember(Name = "title")]
		public string Title { get; set; }

		[DataMember(Name = "hidden_fields")]
		public Dictionary<string, string> HiddenFields { get; set; }

		[DataMember(Name = "money_source")]
		public MoneySources[] MoneySource { get; set; }

		[DataMember(Name = "form")]
		public FormParameter[] Form { get; set; }

		[DataMember(Name = "response_uri")]
		public string ResponseUri { get; set; }
	}

	

	
}