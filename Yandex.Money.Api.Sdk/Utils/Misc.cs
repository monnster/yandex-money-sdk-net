using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Money.Api.Sdk.Utils
{
    /// <summary>
    ///  helpers
    /// </summary>
    public static class Misc
    {
        /// <summary>
        /// transform a dictionary into the query string that is translated in byte array then
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static byte[] PostBytes(Dictionary<string, string> items)
        {
            if (items == null || !items.Any())
                return null;

            var resultStringSeed = String.Empty;

            var queryString =
                items.Aggregate(resultStringSeed,
                    (resultString, item) =>
                        String.Format("{0}&{1}={2}",
                            resultString,
                            Uri.EscapeDataString(item.Key),
                            Uri.EscapeDataString(item.Value))
                    )
                    .Trim(new[] { '&' });

            var bytes = Encoding.UTF8.GetBytes(queryString);

            return bytes.Any() ? bytes : null;
        }

        /// <summary>
        /// convert the query string into the dictionary
        /// </summary>
        /// <param name="query">?p1=v1...</param>
        /// <returns>dictionary</returns>
        public static Dictionary<string, string> QueryParamsToDictionary(String query)
        {
            return String.IsNullOrEmpty(query)
                ? new Dictionary<string, string>()
                : query
                    .TrimStart(new[] { ' ', '?' })
                    .Split(new[] { '&' })
                    .Select(item => item.Split(new[] { '=' }))
                    .Where(array => array.Length == 2)
                    .ToDictionary(array => array[0].Trim(), array => array[1].Trim());
        }
    }
}
