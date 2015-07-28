using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Yandex.Money.Api.Sdk.Interfaces
{
    /// <summary>
    /// Represent interface to send http requests
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// Executes request
        /// </summary>
        /// <param name="request">an instance of IRequest implementation</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
        /// <returns></returns>
        Task<Stream> UploadDataAsync(IRequest request, CancellationToken cancellationToken = default(CancellationToken));
    }
}