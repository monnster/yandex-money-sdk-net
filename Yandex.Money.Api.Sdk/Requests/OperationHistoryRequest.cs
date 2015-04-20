using System;
using System.Collections.Generic;
using Yandex.Money.Api.Sdk.Interfaces;
using Yandex.Money.Api.Sdk.Requests.Base;
using Yandex.Money.Api.Sdk.Utils;

namespace Yandex.Money.Api.Sdk.Requests
{
    /// <summary>
    /// view the history of transactions
    /// <see cref="http://tech.yandex.ru/money/doc/dg/reference/operation-history-docpage/"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class OperationHistoryRequest<TResult> : JsonRequest<TResult>
    {
        /// <summary>
        /// List of operation types to display 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Filtering payments by the label value
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Output operations FROM a timestamp
        /// </summary>
        public DateTime? From { get; set; }

        /// <summary>
        /// Output operations TO a timestamp
        /// </summary>
        public DateTime? Till { get; set; }

        /// <summary>
        /// If this parameter is present, displays all operations starting from the number start_record. Operations are numbered starting from 0 
        /// </summary>
        public string StartRecord { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int Records { get; set; }

        /// <summary>
        /// Show operation details
        /// </summary>
        public bool Details { get; set; }

        /// <summary>
        /// Initializes a new instance of the Yandex.Money.Api.Sdk.Requests.OperationHistoryRequest class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jsonSerializer"></param>
        public OperationHistoryRequest(IHttpClient client, IGenericSerializer<TResult> jsonSerializer)
            : base(client, jsonSerializer)
        {
        }

        public override string RelativeUri
        {
            get { return @"api/operation-history"; }
        }

        public override void AppendItemsTo(Dictionary<string, string> uiParams)
        {
            if (uiParams == null)
                return;

            if (!String.IsNullOrEmpty(Type))
                uiParams.Add("type", Type);

            if (!String.IsNullOrEmpty(Label))
                uiParams.Add("label", Label);

            if (From.HasValue)
                uiParams.Add("from", From.Value.ToServerTime(true));

            if (Till.HasValue)
                uiParams.Add("till", Till.Value.ToServerTime(true));

            if (!String.IsNullOrEmpty(StartRecord))
                uiParams.Add("start_record", StartRecord);

            uiParams.Add("records", ((Records < 1 || Records > 100) ? 30 : Records).ToString());

            if (Details)
                uiParams.Add("details", "true");
        }
    }
}
