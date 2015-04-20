using System.IO;
using System.Threading.Tasks;
using Yandex.Money.Api.Sdk.Interfaces;

namespace Yandex.Money.Api.Sdk.Requests.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class SimpleRequest<TResult> : Request<TResult> where TResult: new()
    {
        public SimpleRequest(IHttpClient client)
            : base(client)
        {
        }

        public override Task<TResult> Parse(Stream stream)
        {
            return Task.Factory.StartNew(() => new TResult()); 
        }
    }
}