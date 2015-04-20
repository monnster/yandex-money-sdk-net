using System;

namespace Yandex.Money.Api.Sdk.Responses
{
    [Flags]
    public enum ResponseStatus
    {
        Unknown = 0,
        /// <summary>
        /// Success
        /// </summary>
        Success = 1,
        /// <summary>
        /// Payment processing was refused
        /// </summary>
        Refused = 2,
        /// <summary>
        /// Payment authorization was not completed. The application should repeat the request with the same parameters later
        /// </summary>
        InProgress = 4,
        HoldForPickup = 8,
        ExtAuthRequired = 16,
        /// <summary>
        /// fictitious
        /// </summary>
        ExtActionRequired = 32,
        Update = 64,
        LimitExceeded = 128
    }
}