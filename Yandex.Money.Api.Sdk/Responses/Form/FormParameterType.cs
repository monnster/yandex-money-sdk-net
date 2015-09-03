using System.Runtime.Serialization;

namespace Yandex.Money.Api.Sdk.Responses.Form
{
	/// <summary>
	/// Possible form parameter types
	/// <see cref="https://tech.yandex.ru/money/doc/dg/reference/showcase-docpage/"/>
	/// </summary>
	[DataContract]
	public enum FormParameterType
	{
		[EnumMember(Value = "text")]
		Text,

		[EnumMember(Value = "number")]
		Number,

		[EnumMember(Value = "amount")]
		Amount,

		[EnumMember(Value = "email")]
		Email,

		[EnumMember(Value = "tel")]
		Tel,

		[EnumMember(Value = "checkbox")]
		Checkbox,

		[EnumMember(Value = "date")]
		Date,

		[EnumMember(Value = "month")]
		Month,

		[EnumMember(Value = "select")]
		Select,

		[EnumMember(Value = "textarea")]
		Textarea,

		[EnumMember(Value = "submit")]
		Submit,
	}
}