

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class OrderPaymentDetails : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("OrderID")]
		public System.Int64 OrderID { get; set; }

				[JsonPropertyName("PaymentMethodID")]
		public System.Int64 PaymentMethodID { get; set; }

				[JsonPropertyName("PaymentTransUID")]
		public System.String PaymentTransUID { get; set; }

				[JsonPropertyName("PaymentDateTime")]
		public System.DateTime? PaymentDateTime { get; set; }

				[JsonPropertyName("IsDeleted")]
		public System.Boolean IsDeleted { get; set; }

				[JsonPropertyName("CreatedDate")]
		public System.DateTime CreatedDate { get; set; }

				[JsonPropertyName("CreatedByID")]
		public System.Int64 CreatedByID { get; set; }

				[JsonPropertyName("ModifiedDate")]
		public System.DateTime? ModifiedDate { get; set; }

				[JsonPropertyName("ModifiedByID")]
		public System.Int64? ModifiedByID { get; set; }

				
    }
}
