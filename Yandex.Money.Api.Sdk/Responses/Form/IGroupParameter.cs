using System.Runtime.Serialization;

namespace Yandex.Money.Api.Sdk.Responses.Form
{
	[DataContract]
	public enum GroupLayout
	{
		[EnumMember(Value = "HBox")]
		VBox,

		[EnumMember(Value = "VBox")]
		HBox,
	}


	public interface IGroupParameter
	{
		FormParameter[] Items { get; }

		string Label { get; }

		GroupLayout Layout { get; }
	}
}