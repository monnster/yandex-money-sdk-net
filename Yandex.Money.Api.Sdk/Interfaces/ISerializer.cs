using System.IO;

namespace Yandex.Money.Api.Sdk.Interfaces
{
    public interface ISerializer<T>
    {
        /// <summary>Serialize to stream.
        /// </summary>
        /// <param name="value">Instance of <typeparam name="T"/> to serialize.</param>
        /// <param name="outStream">Output stream.</param>
        void Serialize(T value, Stream outStream);

        /// <summary>Serialize to string.
        /// </summary>
        /// <param name="value">Instance of <typeparam name="T"/> to serialize.</param>
        /// <returns>String representation of <param name="value"/>.</returns>
        string Serialize(T value);
    }
}
