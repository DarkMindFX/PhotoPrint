

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class Contact : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("ContactTypeID")]
		public System.Int64 ContactTypeID { get; set; }

				[JsonPropertyName("Title")]
		public System.String Title { get; set; }

				[JsonPropertyName("Comment")]
		public System.String Comment { get; set; }

				[JsonPropertyName("Value")]
		public System.String Value { get; set; }

				[JsonPropertyName("IsDeleted")]
		public System.Boolean IsDeleted { get; set; }

				[JsonPropertyName("CreatedByID")]
		public System.Int64 CreatedByID { get; set; }

				[JsonPropertyName("CreatedDate")]
		public System.DateTime CreatedDate { get; set; }

				[JsonPropertyName("ModifiedByID")]
		public System.Int64? ModifiedByID { get; set; }

				[JsonPropertyName("ModifiedDate")]
		public System.DateTime? ModifiedDate { get; set; }

				
    }
}
