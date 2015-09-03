using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Responses;

namespace Yandex.Money.Api.Sdk.Requests
{
	/// <summary>
	/// Creates a payment which may then be executed.
	/// <see cref="http://tech.yandex.ru/money/doc/dg/reference/request-payment-docpage/"/>
	/// </summary>
	public class RequestPaymentRequest: RequestPaymentRequestBase<RequestPaymentResult>
	{
		/// <summary>
		/// Initializes new instance of <see cref="RequestPaymentRequest"/> class.
		/// </summary>
		/// <param name="patternId">Pattern id (same as showcase id)</param>
		/// <param name="paymentParams">Payment parameters</param>
		public RequestPaymentRequest(string patternId, Dictionary<string, string> paymentParams) 
			: base(patternId, paymentParams)
		{
		}

		/// <summary>
		/// Initializes new instance of <see cref="RequestPaymentRequest"/> class.
		/// </summary>
		/// <param name="paymentParams">Payment parameters. Should contain a <code>pattern_id</code> param.</param>
		public RequestPaymentRequest(IParams paymentParams) 
			: base(paymentParams)
		{
		}
	}
}