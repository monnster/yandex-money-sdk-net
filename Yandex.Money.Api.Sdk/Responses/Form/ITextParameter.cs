namespace Yandex.Money.Api.Sdk.Responses.Form
{
	public interface ITextParameter
	{
		int? MinLength { get; }

		int? MaxLength { get; }

		string Pattern { get; }

		string KeyboardSuggest { get; }
	}
}