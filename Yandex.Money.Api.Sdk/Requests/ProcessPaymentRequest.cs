using Yandex.Money.Api.Sdk.Responses;

namespace Yandex.Money.Api.Sdk.Requests
{
	/// <summary>
	/// Confirms a payment that was created using the request-payment method.
	/// <see cref="http://tech.yandex.ru/money/doc/dg/reference/process-payment-docpage/"/>
	/// </summary>
	public class ProcessPaymentRequest: ProcessPaymentRequestBase<ProcessPaymentResult>
	{
		/// <summary>
		/// Do not allow to construct this request directly.
		/// </summary>
		protected ProcessPaymentRequest(string requestId, string moneySource, string csc, string extAuthSuccessUri, string extAuthFailUri) 
			: base(requestId, moneySource, csc, extAuthSuccessUri, extAuthFailUri)
		{
		}

		/// <summary>
		/// Initializes <see cref="ProcessPaymentRequest"/> for payment using wallet as money source.
		/// </summary>
		/// <param name="requestId">Request id obtained via <see cref="RequestPaymentRequest"/>.</param>
		/// <returns>ProcessPaymentRequest instance.</returns>
		public static ProcessPaymentRequest FromWallet([NotNull] string requestId)
		{
			return new ProcessPaymentRequest(requestId, "wallet", null, null, null);
		}

		/// <summary>
		/// Initializes new instance of <see cref="ProcessPaymentRequest"/> for payment using bank card.
		/// </summary>
		/// <param name="requestId">Request id obtained via <see cref="RequestPaymentRequest"/>.</param>
		/// <param name="cardId">Id of the card which should be used as money source.</param>
		/// <param name="csc">[optional] card security code.</param>
		/// <param name="extAuthSuccessUri">[optional] Uri to redirect in case of successfull authorization.</param>
		/// <param name="extAuthFailUri">[optional] Uri to redirect in case authorization failed.</param>
		/// <returns></returns>
		public static ProcessPaymentRequest FromCard(
			[NotNull] string requestId,
			[NotNull] string cardId,
			[NotNull] string extAuthSuccessUri,
			[NotNull] string extAuthFailUri,
			[CanBeNull] string csc = null
			)
		{
			Argument.NotNull(cardId, "Card id is required.");
			Argument.NotNullOrEmpty(extAuthFailUri, "Redirect uri for fail requests is required.");
			Argument.NotNullOrEmpty(extAuthSuccessUri, "Redirect uri for success requests is required.");

			return new ProcessPaymentRequest(requestId, cardId, csc, extAuthSuccessUri, extAuthFailUri);
		}
	}
}