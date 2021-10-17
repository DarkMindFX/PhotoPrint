

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class Region : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("RegionName")]
		public System.String RegionName { get; set; }

				[JsonPropertyName("CountryID")]
		public System.Int64 CountryID { get; set; }

				[JsonPropertyName("IsDeleted")]
		public System.Int64 IsDeleted { get; set; }

				
    }
}
