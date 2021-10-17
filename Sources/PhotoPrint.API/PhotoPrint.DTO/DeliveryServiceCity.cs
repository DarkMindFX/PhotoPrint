

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class DeliveryServiceCity : HateosDto
    {
				[JsonPropertyName("DeliveryServiceID")]
		public System.Int64 DeliveryServiceID { get; set; }

				[JsonPropertyName("CityID")]
		public System.Int64 CityID { get; set; }

				
    }
}
