

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class ImageCategory : HateosDto
    {
				[JsonPropertyName("ImageID")]
		public System.Int64 ImageID { get; set; }

				[JsonPropertyName("CategoryID")]
		public System.Int64 CategoryID { get; set; }

				
    }
}
