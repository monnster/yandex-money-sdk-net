namespace Yandex.Money.Api.Sdk.Responses.Form
{
	public interface ISelectParameter
	{
		SelectParameterStyle Style { get; }

		SelectParameterOption[] Options { get; }

		string Value { get; }
	}
}