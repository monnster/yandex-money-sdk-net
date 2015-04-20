using System;

namespace Yandex.Money.Api.Sdk.Interfaces
{
    public interface IOperation
    {
        Int64 Id { get; }
        Boolean IsNotOperationId { get; }
    }
}
