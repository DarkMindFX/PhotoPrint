

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class UserStatus : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("StatusName")]
		public System.String StatusName { get; set; }

				[JsonPropertyName("IsDeleted")]
		public System.Boolean IsDeleted { get; set; }

				
    }
}
