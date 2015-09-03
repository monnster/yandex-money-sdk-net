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

            var bytes = Encoding.UTF8.GetBytes(items.ToQueryString());

            return bytes.Any() ? bytes : null;
        }

		/// <summary>
		/// Convert dictionary of query params to query string, suitable for GET requests.
		/// </summary>
		/// <param name="queryParams"></param>
		/// <returns></returns>
	    public static string ToQueryString(this Dictionary<string, string> queryParams)
	    {
			if (queryParams == null || !queryParams.Any())
				return null;

			var resultStringSeed = String.Empty;

			return queryParams.Aggregate(resultStringSeed,
					(resultString, item) =>
						String.Format("{0}&{1}={2}",
							resultString,
							Uri.EscapeDataString(item.Key),
							Uri.EscapeDataString(item.Value))
					)
					.Trim(new[] { '&' });
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

	    public static bool IsLanguageSupported(string langCode)
	    {
			// in case no lang code is specified Yandex api will use default.
		    if (string.IsNullOrEmpty(langCode))
			    return true;

		    var supportedLangs = new[]
		    {
			    "ru",
			    "en"
		    };

		    return supportedLangs.Contains(langCode);
	    }
    }
}
