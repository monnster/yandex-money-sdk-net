using System;
using System.Collections.Generic;
using System.Linq;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Responses;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Creates a payment request which may be paid via bank card without authorization.
	/// <see cref="https://tech.yandex.ru/money/doc/dg/reference/request-external-payment-docpage/"/>.
    /// </summary>
    public class RequestExternalPaymentRequest : RequestPaymentRequestBase<RequestExternalPaymentResult>
    {
	    /// <summary>
	    /// Initializes new instance of <see cref="RequestExternalPaymentRequest"/> class.
	    /// </summary>
	    /// <param name="patternId">Pattern id (same as showcase id)</param>
	    /// <param name="instanceId">Application instance id. <see cref="InstanceIdRequest"/></param>
	    /// <param name="paymentParams">Payment parameters.</param>
	    public RequestExternalPaymentRequest(string patternId, string instanceId, Dictionary<string, string> paymentParams) 
			: base(patternId, paymentParams)
	    {
			Argument.NotNullOrEmpty(instanceId, "Instance id is required.");

			_paymentParams["instance_id"] = instanceId;
	    }

		/// <summary>
		/// Initializes new instance of <see cref="RequestPaymentRequest"/> class.
		/// </summary>
		/// <param name="instanceId">Application instance id. <see cref="InstanceIdRequest"/></param>
		/// <param name="paymentParams">Payment parameters. Should contain <code>pattern_id</code> param.</param>
	    public RequestExternalPaymentRequest(string instanceId, IParams paymentParams) : base(paymentParams)
	    {
			Argument.NotNullOrEmpty(instanceId, "Instance id is required.");

			_paymentParams["instance_id"] = instanceId;
	    }

		#region Overrides

		public override string RelativeUri
		{
			get { return @"api/request-external-payment"; }
		}

		#endregion
    }
}