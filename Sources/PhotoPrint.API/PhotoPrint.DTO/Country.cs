

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class Country : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("CountryName")]
		public System.String CountryName { get; set; }

				[JsonPropertyName("ISO")]
		public System.String ISO { get; set; }

				[JsonPropertyName("IsDeleted")]
		public System.Boolean IsDeleted { get; set; }

				
    }
}
