using System.IO;
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
        /// <returns></returns>
        Task<Stream> UploadDataAsync(IRequest request);
    }
}