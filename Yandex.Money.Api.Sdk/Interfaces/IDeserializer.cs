using System.IO;

namespace Yandex.Money.Api.Sdk.Interfaces
{
    public interface IDeserializer<T>
    {
        /// <summary>
        /// Deserializes type T from a stream.
        /// </summary>
        T Deserialize(Stream stream, bool leaveOpen = false);

        /// <summary>
        /// Deserializes type T from a string.
        /// </summary>
        T Deserialize(string value);
    }
}
