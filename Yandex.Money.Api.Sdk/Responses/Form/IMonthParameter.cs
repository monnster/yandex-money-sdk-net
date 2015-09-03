using System;
using System.Security.Cryptography.X509Certificates;

namespace Yandex.Money.Api.Sdk.Responses.Form
{
	public interface IMonthParameter
	{
		DateTime? MinMonth { get; }
		DateTime? MaxMonth { get; }
	}
}