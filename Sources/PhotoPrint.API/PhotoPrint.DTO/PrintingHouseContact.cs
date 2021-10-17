

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class PrintingHouseContact : HateosDto
    {
				[JsonPropertyName("PrintingHouseID")]
		public System.Int64 PrintingHouseID { get; set; }

				[JsonPropertyName("ContactID")]
		public System.Int64 ContactID { get; set; }

				[JsonPropertyName("IsPrimary")]
		public System.Boolean IsPrimary { get; set; }

				
    }
}
