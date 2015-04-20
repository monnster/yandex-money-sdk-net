using System;

namespace Yandex.Money.Api.Sdk.Utils
{
    public sealed class ParamNameAttribute : Attribute
    {
        public String Name { get; set; }

        public ParamNameAttribute(String name)
        {
            Name = name;
        }
    }
}