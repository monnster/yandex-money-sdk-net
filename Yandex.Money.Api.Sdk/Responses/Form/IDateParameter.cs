using System;

namespace Yandex.Money.Api.Sdk.Responses.Form
{
	public interface IDateParameter
	{
		DateTime? MinDate { get; }
		DateTime? MaxDate { get; }
	}
}