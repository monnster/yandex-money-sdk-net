using System;
using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// exchange of temporary token on the authorization token
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/obtain-access-token-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class TokenRequest<TResult> : JsonRequest<TResult>
    {
        /// <summary>
        /// Temporary token
        /// </summary>
        public String Code { get; set; }

        /// <summary>
        /// The client_id that was assigned to the application during registration
        /// <see cref="http://tech.yandex.ru/money/doc/dg/tasks/register-client-docpage/"/>
        /// </summary>
        public String ClientId { get; set; }

        /// <summary>
        /// Constant value: authorization_code
        /// </summary>
        public String GrantType
        {
            get { return " authorization_code"; }
        }

        /// <summary>
        /// URI that the OAuth server sends the authorization result to
        /// </summary>
        public String RedirectUri { get; set; }

        /// <summary>
        /// A secret word for verifying the application's authenticity
        /// </summary>
        public String ClientSecret { get; set; }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.TokenRequest class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer"></param>
        public TokenRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer)
            : base(client, jsonSerializer)
        {
        }

        public override string RelativeUri
        {
            get { return @"oauth/token"; }
        }

        public override void AppendItemsTo(Dictionary<string, string> items)
        {
            if (items == null)
                return;

            items.Add("client_id", ClientId);
            items.Add("code", Code);
            items.Add("grant_type", GrantType);

            if (!String.IsNullOrEmpty(RedirectUri))
                items.Add("redirect_uri", Uri.EscapeDataString(RedirectUri));

            if (!String.IsNullOrEmpty(ClientSecret))
                items.Add("client_secret", Uri.EscapeDataString(ClientSecret));
        }
    }
}
