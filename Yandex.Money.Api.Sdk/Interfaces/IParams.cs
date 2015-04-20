using System.Collections.Generic;

namespace Yandex.Money.Api.Sdk.Interfaces
{
    public interface IParams
    {
        Dictionary<string, string> GetParams();
    }
}