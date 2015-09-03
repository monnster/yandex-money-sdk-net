using System;
using System.Collections.Generic;
using System.Globalization;
using Yandex.Money.Api.Sdk.Requests.Base;
using Yandex.Money.Api.Sdk.Responses;
using Yandex.Money.Api.Sdk.Utils;
using KV = System.Collections.Generic.KeyValuePair<string, string>;

namespace Yandex.Money.Api.Sdk.Requests
{
	[Flags]
	public enum HistoryOperationTypes
	{
		Deposition = 1,
		Payment = 2,
		Incoming_transfers_unaccepted = 4,

		/// <summary>
		/// Do not filter by operation type.
		/// </summary>
		All = Deposition | Payment | Incoming_transfers_unaccepted
	}

    /// <summary>
    /// View transaction history.
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/operation-history-docpage/"/>
    /// </summary>
    public class OperationHistoryRequest : JsonRequest<OperationHistoryResult>
    {
		private readonly HistoryOperationTypes _types;
	    private readonly string _label;
	    private readonly DateTime? _from;
	    private readonly DateTime? _till;
	    private readonly string _startRecord;
	    private readonly int _records;
	    private readonly bool _details;

		/// <summary>
		/// Initializes new instance of <see cref="OperationHistoryRequest"/> class.
		/// </summary>
		/// <param name="types">Filter by operation type.</param>
		/// <param name="label">Filter by label.</param>
		/// <param name="from">Get operations FROM this timestamp.</param>
		/// <param name="till">Get operations TO this timestamp.</param>
		/// <param name="startRecord">In case of multi-page history set startRecord to next_record value of prev response, otherwise leave empty.</param>
		/// <param name="records">Number of records to retrieve per request. Default is 30.</param>
		/// <param name="details">Whether to retrieve extended operation details or not. Requires permission <code>operation-details</code>. Default is false.</param>
		public OperationHistoryRequest(HistoryOperationTypes types, string label, DateTime? @from, DateTime? till, string startRecord = null, int records = 30, bool details = false)
	    {
			Argument.Require(records > 0 && records <= 100, "Valid range for [records] argument is between 1 and 100");

		    _types = types;
		    _label = label;
		    _from = @from;
		    _till = till;
		    _startRecord = startRecord;
		    _records = records;
		    _details = details;
	    }

	    public override string RelativeUri
        {
            get { return @"api/operation-history"; }
        }

	    public override IEnumerable<KV> GetRequestParams()
	    {
		    if (_types != HistoryOperationTypes.All)
		    {
			    yield return new KV("type", BuildOperationTypeFilter(_types));
		    }

		    if(!string.IsNullOrEmpty(_label))
				yield return new KV("label", _label);

			if(_from.HasValue)
				yield return new KV("from", _from.Value.ToServerTime(true));

			if(_till.HasValue)
				yield return new KV("till", _till.Value.ToServerTime(true));

			if(!string.IsNullOrEmpty(_startRecord))
				yield return new KV("start_record", _startRecord);

		    yield return new KV("records", _records.ToString(CultureInfo.InvariantCulture));

			if(_details)
				yield return new KV("details", "true");
	    }

	    private static string BuildOperationTypeFilter(HistoryOperationTypes operationTypes)
	    {
		    string filter = "";

		    if (operationTypes.HasFlag(HistoryOperationTypes.Payment))
			    filter += " payment";

		    if (operationTypes.HasFlag(HistoryOperationTypes.Deposition))
			    filter += " deposition";

		    if (operationTypes.HasFlag(HistoryOperationTypes.Incoming_transfers_unaccepted))
			    filter += " incoming-transfers-unaccepted";

		    return filter.TrimStart();
	    }
	   
    }
}
