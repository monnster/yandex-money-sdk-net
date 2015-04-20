namespace Yandex.Money.Api.Sdk.Interfaces
{
    public interface IGenericSerializer<T> : ISerializer<T>, IDeserializer<T>
    {
    }
}
