using System.Runtime.Serialization;
using Yandex.Money.Api.Sdk.Responses.Form;

namespace Yandex.Money.Api.Sdk.Responses
{
	[DataContract]
	public enum SelectParameterStyle
	{
		[EnumMember(Value = "RadioGroup")]
		RadioGroup,

		[EnumMember(Value = "Spinner")]
		Spinner,
	}


	[DataContract]
	public class SelectParameterOption
	{
		[DataMember(Name = "value")]
		public string Value { get; set; }

		[DataMember(Name = "label")]
		public string Label { get; set; }

		[DataMember(Name = "group")]
		public FormParameter[] Group { get; set; }
	}
}