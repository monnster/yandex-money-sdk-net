using System;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Money.Api.Sdk.Responses.Form
{
	[DataContract]
	public class FormParameter: 
		ITextParameter, 
		INumberParameter,
		IEmailParameter, 
		ITelParameter, 
		ICheckboxParameter,
		IAmountParameter, 
		IDateParameter, 
		IMonthParameter, 
		ISelectParameter,
		ITextAreaParameter,
		ISubmitParameter,
		IGroupParameter
	{
		#region Common paramerters

		[DataMember(Name = "type")]
		public FormParameterType Type { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "value")]
		public string Value { get; set; }

		[DataMember(Name = "value_autofill")]
		public string ValueAutofill { get; set; }

		[DataMember(Name = "label")]
		public string Label { get; set; }

		[DataMember(Name = "hint")]
		public string Hint { get; set; }

		[DataMember(Name = "alert")]
		public string Alert { get; set; }

		[DataMember(Name = "required")]
		public bool Required { get; set; }

		[DataMember(Name = "readonly")]
		public bool Readonly { get; set; }
		
		#endregion

		#region Multi-purpose optional params

		[DataMember(Name = "min")]
		public string Min { get; set; }

		[DataMember(Name = "max")]
		public string Max { get; set; }

		[DataMember(Name = "step")]
		public decimal? Step { get; set; }

		[DataMember(Name = "minlength")]
		public int? MinLength { get; set; }

		[DataMember(Name = "maxlength")]
		public int? MaxLength { get; set; }

		#endregion

		#region ITextParameter implementation

		[DataMember(Name = "pattern")]
		public string Pattern { get; set; }

		[DataMember(Name = "keyboard_suggest")]
		public string KeyboardSuggest { get; set; }

		#endregion

		#region IAmountParameter implementation

		public decimal? MinAmount
		{
			get
			{
				decimal value;
				return Decimal.TryParse(Min, out value)
					? (decimal?) value
					: null;
			}
		}

		public decimal? MaxAmount
		{
			get
			{
				decimal value;
				return Decimal.TryParse(Max, out value)
					? (decimal?) value
					: null;
			}
		}
		

		[DataMember(Name = "currency")]
		public string Currency { get; set; }

		[DataMember(Name = "fee")]
		public FeeDescription Fee { get; set; }

		#endregion

		#region INumberParameter implementation

		public decimal? MinNumber
		{
			get { return MinAmount; }
		}


		public  decimal? MaxNumber
		{
			get { return MaxAmount; }
		}

		#endregion

		#region IDateParameter implementation

		public DateTime? MinDate
		{
			get
			{
				DateTime result;

				return DateTime.TryParseExact(Min, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result)
					? (DateTime?) result
					: null;
			}
			
		}

		public DateTime? MaxDate
		{
			get
			{
				DateTime result;

				return DateTime.TryParseExact(Max, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result)
					? (DateTime?)result
					: null;
			}
		}

		#endregion

		#region IMonthFormParameter omplementation

		public DateTime? MinMonth
		{
			get
			{
				DateTime result;

				return DateTime.TryParseExact(Min, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out result)
					? (DateTime?)result
					: null;
			}

		}

		public DateTime? MaxMonth
		{
			get
			{
				DateTime result;

				return DateTime.TryParseExact(Max, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out result)
					? (DateTime?)result
					: null;
			}
		}

		#endregion

		#region ISelectParameter implementation

		[DataMember(Name="style")]
		public SelectParameterStyle Style { get; private set; }

		[DataMember(Name = "options")]
		public SelectParameterOption[] Options { get; set; }

		#endregion

		#region ICheckboxParameter implementation

		[DataMember(Name = "checked")]
		public bool Checked { get; set; }

		#endregion

		#region IGroupParameter implementation

		[DataMember(Name = "items")]
		public FormParameter[] Items { get; set; }

		[DataMember(Name = "layout")]
		public GroupLayout Layout { get; set; }

		#endregion
	}
}