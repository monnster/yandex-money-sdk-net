using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Requests.Base;
using KV = System.Collections.Generic.KeyValuePair<string, string>;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// Base class which is used to confirm payments.
    /// </summary>
    /// <typeparam name="TResult">Type of result.</typeparam>
    public abstract class ProcessPaymentRequestBase<TResult> : JsonRequest<TResult>
    {
	    private readonly string _requestId;
	    private readonly string _moneySource;
	    private readonly string _csc;
	    private readonly string _extAuthSuccessUri;
	    private readonly string _extAuthFailUri;

		/// <summary>
		/// Default ctor.
		/// </summary>
		/// <param name="requestId">Request id returned by <see cref="RequestPaymentRequest"/>.</param>
		/// <param name="moneySource">Money source. Supported values: "wallet" or linked card identifier.</param>
		/// <param name="csc">Card security code, for card payments only.</param>
		/// <param name="extAuthSuccessUri">Address of the page to return to when card payment has been successfully authorized using 3-D Secure technology.</param>
		/// <param name="extAuthFailUri">Address of the page to return to when authorization has been denied for card payment using 3-D Secure technology.</param>
	    protected ProcessPaymentRequestBase(
			[NotNull] string requestId, 
			[CanBeNull] string moneySource, 
			[CanBeNull] string csc, 
			[CanBeNull] string extAuthSuccessUri, 
			[CanBeNull] string extAuthFailUri)
	    {
			Argument.NotNullOrEmpty(requestId, "Request id is required.");

		    _requestId = requestId;
		    _moneySource = moneySource;
		    _csc = csc;
		    _extAuthSuccessUri = extAuthSuccessUri;
		    _extAuthFailUri = extAuthFailUri;
	    }
		#region Overrides

		public override string RelativeUri
		{
			get { return @"api/process-payment"; }
		}

	    public override IEnumerable<KV> GetRequestParams()
	    {
		    yield return new KV("request_id", _requestId);

			// external payments do not contain this param.
			if(!string.IsNullOrEmpty(_moneySource))
				yield return new KV("money_source", _moneySource);

		    if (!string.IsNullOrEmpty(_csc))
			    yield return new KV("csc", _csc);

			if(!string.IsNullOrEmpty(_extAuthFailUri))
				yield return new KV("ext_auth_fail_uri", _extAuthFailUri);

			if(!string.IsNullOrEmpty(_extAuthSuccessUri))
				yield return new KV("ext_auth_success_uri", _extAuthSuccessUri);
	    }

	    #endregion
    }
}