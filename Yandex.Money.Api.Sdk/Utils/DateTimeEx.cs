using System;

namespace Yandex.Money.Api.Sdk.Utils
{
    /// <summary>
    /// DateTime class extension method
    /// </summary>
    public static class DateTimeEx
    {
        /// <summary>
        /// time format
        /// </summary>
        public const string TimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'";

        /// <summary>
        /// time to send in the request
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="toUniversal"></param>
        /// <returns>a string in a special format</returns>
        public static string ToServerTime(this DateTime dt, Boolean toUniversal)
        {
            return toUniversal ? dt.ToUniversalTime().ToString(TimeFormat) : dt.ToString(TimeFormat);
        }
    }
}