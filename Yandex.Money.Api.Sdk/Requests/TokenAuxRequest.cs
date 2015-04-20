using System;
using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Get more token
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/obtain-token-aux-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class TokenAuxRequest<TResult> : JsonRequest<TResult>
    {
        /// <summary>
        /// A list of requested permissions
        /// </summary>
        public String Scope { get; set; }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.TokenAuxRequest class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer"></param>
        public TokenAuxRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer)
            : base(client, jsonSerializer)
        {
        }

        public override string RelativeUri
        {
            get { return @"api/token-aux"; }
        }

        public override void AppendItemsTo(Dictionary<string, string> items)
        {
            if (items == null)
                return;

            items.Add("scope", Scope);
        }
    }

    /// <summary>
    /// Revoking a token 
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/revoke-access-token-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class TokenRevokeRequest<TResult> : SimpleRequest<TResult> where TResult: new()
    {
        /// <summary>
        /// Revoke the primary token and all additional
        /// </summary>
        public Boolean RevokeAll { get; set; }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.TokenRevokeRequest class.
        /// </summary>
        /// <param name="client"></param>
        public TokenRevokeRequest(IHttpClient client)
            : base(client)
        {
        }

        public override string RelativeUri
        {
            get { return @"api/revoke"; }
        }

        public override void AppendItemsTo(Dictionary<string, string> items)
        {
            if (items == null)
                return;

            items.Add("revoke-all", RevokeAll.ToString());
        }
    }
}
