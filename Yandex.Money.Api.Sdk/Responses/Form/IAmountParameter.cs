namespace Yandex.Money.Api.Sdk.Responses.Form
{
	public interface IAmountParameter
	{
		decimal? MinAmount { get;  }

		decimal? MaxAmount { get; }

		decimal? Step { get; }

		string Currency { get; }

		FeeDescription Fee { get; }
	}
}