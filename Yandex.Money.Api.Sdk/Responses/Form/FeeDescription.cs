using System.Runtime.Serialization;

namespace Yandex.Money.Api.Sdk.Responses.Form
{
	[DataContract]
	public enum FeeType
	{
		/// <summary>
		/// Fee is calculated by std formula.
		/// </summary>
		[EnumMember(Value = "std")]
		Std,

		/// <summary>
		/// Fee is calculated externally by partner.
		/// </summary>
		[EnumMember(Value = "custom")]
		Custom,
	}

	[DataContract]
	public enum AmountType
	{
		/// <summary>
		/// Fee should be paid by customer
		/// </summary>
		[EnumMember(Value = "custom")]
		Amount,

		/// <summary>
		/// Fee should be paid by merchant.
		/// </summary>
		[EnumMember(Value = "netAmount")]
		NetAmount,
	}

	/// <summary>
	/// Describes fee.
	/// </summary>
	[DataContract]
	public class FeeDescription
	{
		[DataMember(Name = "type")]
		public FeeType Type { get; set; }

		/// <summary>
		/// Determines algorithm which should be used to calculate payment fee.
		/// </summary>
		[DataMember(Name = "amount_type")]
		public AmountType AmountType { get; set; }

		/// <summary>
		/// Fee that goes to merchant.
		/// </summary>
		[DataMember(Name = "a")]
		public decimal? Var { get; set; }

		/// <summary>
		/// Fixed amount per operation.
		/// </summary>
		[DataMember(Name = "b")]
		public decimal? Fix { get; set; }

		/// <summary>
		/// Min fee amount per operation.
		/// </summary>
		[DataMember(Name = "c")]
		public decimal? Min { get; set; }

		/// <summary>
		/// Max fee amount per operation.
		/// </summary>
		[DataMember(Name = "d")]
		public decimal? Max { get; set; }
	}
}