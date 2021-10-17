

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class ContactType : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("ContactTypeName")]
		public System.String ContactTypeName { get; set; }

				[JsonPropertyName("IsDeleted")]
		public System.Boolean IsDeleted { get; set; }

				
    }
}
