using System;
using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Responses;
using KV = System.Collections.Generic.KeyValuePair<string, string>;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
	/// Confirms a payment that was created using the <code>request-payment</code> method, using payment
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/request-external-payment-docpage/"/>
    /// </summary>
    public class ProcessExternalPaymentRequest: ProcessPaymentRequestBase<ProcessExternalPaymentResult>
    {
	    private readonly string _instanceId;
	    private readonly bool _requestToken;
	    private readonly string _moneySourceToken;
	    private readonly string _csc;

        public string InstanceId { get; set; }

        public Boolean RequestToken { get; set; }

        public string MoneySourceToken { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="ProcessExternalPaymentRequest"/> class.
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="instanceId"></param>
		/// <param name="csc"></param>
		/// <param name="requestToken"></param>
		/// <param name="moneySourceToken"></param>
		/// <param name="extAuthSuccessUri"></param>
		/// <param name="extAuthFailUri"></param>
	    protected ProcessExternalPaymentRequest(
			string requestId, 
			string instanceId, 
			string csc, 
			bool requestToken, 
			string moneySourceToken, 
			string extAuthSuccessUri,
			string extAuthFailUri
			) 
			: base(requestId, null, csc, extAuthSuccessUri, extAuthFailUri)
	    {
			Argument.NotNullOrEmpty(instanceId, "Instance id is required.");
			Argument.NotNullOrEmpty(extAuthFailUri, "Redirect uri for fail requests is required.");
			Argument.NotNullOrEmpty(extAuthSuccessUri, "Redirect uri for success requests is required.");

		    _instanceId = instanceId;
		    _requestToken = requestToken;
		    _moneySourceToken = moneySourceToken;
	    }

	    /// <summary>
	    /// Initializes new instance of <see cref="ProcessExternalPaymentRequest"/> for regular payment.
	    /// </summary>
	    /// <param name="requestId">Request id obtained via <see cref="RequestPaymentRequest"/>.</param>
	    /// <param name="instanceId">Application instance id. <see cref="InstanceIdRequest"/></param>
	    /// <param name="extAuthSuccessUri">[optional] Uri to redirect in case of successfull authorization.</param>
	    /// <param name="extAuthFailUri">[optional] Uri to redirect in case authorization failed.</param>
	    /// <param name="requestRecurringToken">Set to true if you want to request recurring token for simplified payments.</param>
	    /// <returns>ProcessExternalPaymentRequest instance.</returns>
	    public static ProcessExternalPaymentRequest Regular(
		    [NotNull] string requestId,
		    [NotNull] string instanceId,
		    [NotNull] string extAuthSuccessUri,
		    [NotNull] string extAuthFailUri,
		    bool requestRecurringToken = false
			)
	    {
		    return new ProcessExternalPaymentRequest(requestId, instanceId, null, requestRecurringToken, null, extAuthSuccessUri, extAuthFailUri);
	    }

		/// <summary>
		/// Initializes new instance of <see cref="ProcessExternalPaymentRequest"/> for recurring payment.
		/// </summary>
		/// <param name="requestId">Request id obtained via <see cref="RequestPaymentRequest"/>.</param>
		/// <param name="instanceId">Application instance id. <see cref="InstanceIdRequest"/></param>
		/// <param name="extAuthSuccessUri">[optional] Uri to redirect in case of successfull authorization.</param>
		/// <param name="extAuthFailUri">[optional] Uri to redirect in case authorization failed.</param>
		/// <param name="moneySourceToken">Token for recurring payments.</param>
		/// <param name="csc">Card security code.</param>
		/// <returns></returns>
	    public static ProcessExternalPaymentRequest Recurring(
		    [NotNull] string requestId,
		    [NotNull] string instanceId,
		    [NotNull] string extAuthSuccessUri,
		    [NotNull] string extAuthFailUri,
		    [NotNull] string moneySourceToken,
		    [NotNull] string csc
		    )
	    {
			Argument.NotNullOrEmpty(moneySourceToken, "Money source token is required.");

			return new ProcessExternalPaymentRequest(requestId, instanceId, csc, false, moneySourceToken, extAuthSuccessUri, extAuthFailUri);
	    }

	    public override IEnumerable<KeyValuePair<string, string>> GetRequestParams()
	    {
		    yield return new KV("instance_id", _instanceId);
			
			if(!string.IsNullOrEmpty(_moneySourceToken))
				yield return new KV("money_source_token", _moneySourceToken);

			if(_requestToken)
				yield return new KV("request_token", "true");

		    foreach (var requestParam in base.GetRequestParams())
		    {
			    yield return requestParam;
		    }
	    }

        /// <summary>
        /// IRequest interface implementation
        /// </summary>
        public override string RelativeUri
        {
            get { return @"/api/process-external-payment"; }
        }
    }
}