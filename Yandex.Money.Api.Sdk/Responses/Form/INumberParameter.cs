namespace Yandex.Money.Api.Sdk.Responses.Form
{
	public interface INumberParameter
	{
		decimal? MinNumber { get; }
		decimal? MaxNumber { get; }
		decimal? Step { get; }
	}
}