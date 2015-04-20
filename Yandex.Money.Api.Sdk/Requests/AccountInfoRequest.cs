using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Getting information about the status of the user account 
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/account-info-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class AccountInfoRequest<TResult> : JsonRequest<TResult>
    {
        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.AccountInfoRequest class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer">the serializer instance</param>
        public AccountInfoRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer) : base(client, jsonSerializer)
        {
        }

        public override string RelativeUri
        {
            get { return @"api/account-info"; }
        }
    }
}
