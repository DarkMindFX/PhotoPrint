

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class Unit : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("UnitName")]
		public System.String UnitName { get; set; }

				[JsonPropertyName("UnitAbbr")]
		public System.String UnitAbbr { get; set; }

				[JsonPropertyName("Description")]
		public System.String Description { get; set; }

				[JsonPropertyName("IsDeleted")]
		public System.Boolean IsDeleted { get; set; }

				
    }
}
