using System;

namespace Yandex.Money.Api.Sdk.Responses
{
    /// <summary>
    /// Possible types of operations
    /// </summary>
    [Flags]
    public enum OperationType
    {
        Unknown = 0,
        /// <summary>
        /// Outgoing payment to a merchant
        /// </summary>
        PaymentShop = 1,
        /// <summary>
        /// Any type of outgoing P2P transfer
        /// </summary>
        OutgoingTransfer = 2,
        /// <summary>
        /// Credit
        /// </summary>
        Deposition = 4,
        /// <summary>
        /// Incoming transfer or deferred transfer
        /// </summary>
        IncomingTransfer = 8,
        /// <summary>
        /// Incoming transfer with a secret code
        /// </summary>
        IncomingTransferProtected = 16,

        UnacceptedIn = 32,
        Out = PaymentShop | OutgoingTransfer,
        In = Deposition | IncomingTransfer | IncomingTransferProtected,
        /// <summary>
        /// fictitious
        /// </summary>
        HoldForPickupTransfer = 64,
        Unaccepted = IncomingTransferProtected | HoldForPickupTransfer
    }
}
