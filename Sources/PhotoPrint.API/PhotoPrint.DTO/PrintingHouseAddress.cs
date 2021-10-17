

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class PrintingHouseAddress : HateosDto
    {
				[JsonPropertyName("PrintingHouseID")]
		public System.Int64 PrintingHouseID { get; set; }

				[JsonPropertyName("AddressID")]
		public System.Int64 AddressID { get; set; }

				[JsonPropertyName("IsPrimary")]
		public System.Boolean IsPrimary { get; set; }

				
    }
}
