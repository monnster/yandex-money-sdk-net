using System;

namespace Yandex.Money.Api.Sdk.Interfaces
{
    /// <summary>
    /// It is the provider that returns addresses for all requests
    /// </summary>
    public interface  IHostProvider
    {
        /// <summary>
        /// compose complete address using relative one
        /// </summary>
        /// <param name="relativeUri"></param>
        /// <returns></returns>
        Uri BuildUri(String relativeUri);

        /// <summary>
        /// 
        /// </summary>
        Uri AuthorizationdUri { get; }
    }
}