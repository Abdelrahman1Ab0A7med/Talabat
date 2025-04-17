using System.Runtime.Serialization;

namespace Talabat.Core.Models.Order
{
	public enum OrderStatus
	{
		[EnumMember(Value = "Pending")]
		Pending,
		[EnumMember(Value = "PaymentReceived")]
		PaymentReceived,
		[EnumMember(Value = "PaymentFaileds")]
		PaymentFailed
	}
}
