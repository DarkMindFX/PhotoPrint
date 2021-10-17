

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class Size : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("SizeName")]
        public System.String SizeName { get; set; }

        [JsonPropertyName("Width")]
        public System.Int32 Width { get; set; }

        [JsonPropertyName("Height")]
        public System.Int32 Height { get; set; }

        [JsonPropertyName("IsDeleted")]
        public System.Int64 IsDeleted { get; set; }

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
