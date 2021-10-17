

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class City : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("CityName")]
        public System.String CityName { get; set; }

        [JsonPropertyName("RegionID")]
        public System.Int64 RegionID { get; set; }

        [JsonPropertyName("IsDeleted")]
        public System.Boolean IsDeleted { get; set; }


    }
}
