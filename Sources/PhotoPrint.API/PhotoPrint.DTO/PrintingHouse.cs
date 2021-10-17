

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class PrintingHouse : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("Name")]
        public System.String Name { get; set; }

        [JsonPropertyName("Description")]
        public System.String Description { get; set; }

        [JsonPropertyName("IsDeleted")]
        public System.Boolean IsDeleted { get; set; }

        [JsonPropertyName("CreatedDate")]
        public System.DateTime CreatedDate { get; set; }

        [JsonPropertyName("CreatedByID")]
        public System.Int64 CreatedByID { get; set; }

        [JsonPropertyName("ModifiedDate")]
        public System.DateTime? ModifiedDate { get; set; }

        [JsonPropertyName("ModifiedByID")]
        public System.Int64? ModifiedByID { get; set; }


    }
}
