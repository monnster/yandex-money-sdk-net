using System;
using System.Collections.Generic;

namespace Yandex.Money.Api.Sdk.Interfaces
{
    /// <summary>
    /// Represent interface to obtain parameters of the request
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// relative url of the request
        /// </summary>
        String RelativeUri { get; }

        /// <summary>
        /// additional post parameters of the request
        /// </summary>
        /// <param name="items">request parameters</param>
        void AppendItemsTo(Dictionary<string, string> items);
    }
}