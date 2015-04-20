using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;

namespace Yandex.Money.Api.Sdk.Utils
{
    /// <summary>
    /// interface implementation
    /// </summary>
    public class RequestParams : IParams
    {
        /// <summary>
        /// returns all properties marked with the "ParamName" attribute as dictionary
        /// </summary>
        /// <returns>dictionary [property name, property value]</returns>
        public virtual Dictionary<string, string> GetParams()
        {
            var result = new Dictionary<string, string>();

            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                var objects = property.GetCustomAttributes(false);

                foreach (var obj in objects)
                {
                    var attribute = obj as ParamNameAttribute;

                    if (attribute == null)
                        continue;

                    var paramValue = property.GetValue(this, null);

                    if (result.ContainsKey(attribute.Name) || paramValue == null)
                        continue;

                    result.Add(attribute.Name,
                        paramValue is double
                            ? paramValue.ToString().Replace(',', '.')
                            : paramValue.ToString());
                }
            }

            return result;
        }
    }
}