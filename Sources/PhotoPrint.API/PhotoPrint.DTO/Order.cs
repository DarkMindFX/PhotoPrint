

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class Order : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("ManagerID")]
		public System.Int64? ManagerID { get; set; }

				[JsonPropertyName("UserID")]
		public System.Int64 UserID { get; set; }

				[JsonPropertyName("ContactID")]
		public System.Int64 ContactID { get; set; }

				[JsonPropertyName("DeliveryAddressID")]
		public System.Int64 DeliveryAddressID { get; set; }

				[JsonPropertyName("DeliveryServiceID")]
		public System.Int64 DeliveryServiceID { get; set; }

				[JsonPropertyName("Comments")]
		public System.String Comments { get; set; }

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
