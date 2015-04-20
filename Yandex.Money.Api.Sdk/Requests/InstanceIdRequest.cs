using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Registration application instance
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/instance-id-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class InstanceIdRequest<TResult> : JsonRequest<TResult>
    {
        public string ClientId { get; set; }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.InstanceIdRequest class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer"></param>
        public InstanceIdRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer) : base(client, jsonSerializer)
        {
        }

        public override string RelativeUri
        {
            get { return @"api/instance-id"; }
        }

        public override void AppendItemsTo(Dictionary<string, string> uiParams)
        {
            if (uiParams == null)
                return;

            uiParams.Add("client_id", ClientId);
        }
    }
}
