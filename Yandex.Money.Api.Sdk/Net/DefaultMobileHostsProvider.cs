using System;
using Yandex.Money.Api.Sdk.Interfaces;

namespace Yandex.Money.Api.Sdk.Net
{
    /// <summary>
    /// Represents an interface implementation by default
    /// </summary>
    public class DefaultMobileHostsProvider : IHostProvider
    {
        private static readonly Uri RequestsBaseUri = new Uri(@"https://money.yandex.ru");
        private static readonly Uri TokenBaseUri = new Uri(@"https://m.sp-money.yandex.ru");
        private static readonly Uri AuthorizeBaseUri = new Uri(@"https://m.sp-money.yandex.ru");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativeUri"></param>
        /// <returns></returns>
        public Uri BuildUri(string relativeUri)
        {
            return relativeUri.Contains(@"oauth/token")
                ? new Uri(TokenBaseUri, relativeUri)
                : new Uri(RequestsBaseUri, relativeUri);
        }

        public Uri AuthorizationdUri
        {
            get { return new Uri(AuthorizeBaseUri, @"oauth/authorize"); }
        }
    }
}
