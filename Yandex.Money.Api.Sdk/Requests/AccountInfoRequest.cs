using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;
using Yandex.Money.Api.Sdk.Responses;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Gets information about the status of the user account.
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/account-info-docpage/"/>
    /// </summary>
    public class AccountInfoRequest : JsonRequest<AccountInfoResult>
    {
		#region Overrides

		public override string RelativeUri
		{
			get { return @"api/account-info"; }
		}

		#endregion
    }
}
