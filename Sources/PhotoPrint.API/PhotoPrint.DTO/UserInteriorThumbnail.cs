


using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class UserInteriorThumbnail : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("UserID")]
		public System.Int64 UserID { get; set; }

				[JsonPropertyName("Url")]
		public System.String Url { get; set; }

				
    }
}
