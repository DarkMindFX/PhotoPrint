

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class OrderStatus : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("OrderStatusName")]
		public System.String OrderStatusName { get; set; }

				[JsonPropertyName("IsDeleted")]
		public System.Int64 IsDeleted { get; set; }

				
    }
}
