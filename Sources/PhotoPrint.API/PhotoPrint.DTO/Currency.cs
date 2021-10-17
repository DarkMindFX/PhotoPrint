

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class Currency : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("ISO")]
		public System.String ISO { get; set; }

				[JsonPropertyName("CurrencyName")]
		public System.String CurrencyName { get; set; }

				[JsonPropertyName("IsDeleted")]
		public System.Boolean IsDeleted { get; set; }

				
    }
}
