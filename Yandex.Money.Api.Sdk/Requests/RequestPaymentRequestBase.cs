using System;
using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Base class for payment requests.
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/request-payment-docpage/"/>
    /// </summary>
    public abstract class RequestPaymentRequestBase<TResult> : JsonRequest<TResult>
    {
	    protected readonly Dictionary<string, string> _paymentParams;

		/// <summary>
		/// Initializes new instance of <see cref="RequestPaymentRequestBase{TResult}"/> class.
		/// </summary>
		/// <param name="patternId">Pattern id (same as showcase id)</param>
		/// <param name="paymentParams">Payment parameters</param>
	    public RequestPaymentRequestBase(string patternId, Dictionary<string, string> paymentParams)
	    {
			Argument.NotNullOrEmpty(patternId, "PatternId is required.");
			Argument.NotNull(paymentParams, "Payment params are required.");

		    _paymentParams = paymentParams;
			_paymentParams.Add("pattern_id", patternId);
	    }

		/// <summary>
		/// Initializes new instance of <see cref="RequestPaymentRequestBase{TResult}"/> class.
		/// </summary>
		/// <param name="paymentParams">Payment parameters. Should contain a pattern_id param.</param>
	    public RequestPaymentRequestBase(IParams paymentParams)
	    {
			Argument.NotNull(paymentParams, "Payment params are required.");

			_paymentParams = paymentParams.GetParams();

			if (!_paymentParams.ContainsKey("pattern_id"))
				throw new InvalidOperationException("Payment params you supplied do not contain a [pattern_id] field which is required.");
	    }


		#region Overrides

		public override string RelativeUri
		{
			get { return @"api/request-payment"; }
		}

		public override IEnumerable<KeyValuePair<string, string>> GetRequestParams()
		{
			return _paymentParams;
		}
		
		#endregion

    }
}
