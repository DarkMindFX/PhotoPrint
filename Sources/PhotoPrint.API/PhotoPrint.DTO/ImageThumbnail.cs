

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class ImageThumbnail : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("Url")]
        public System.String Url { get; set; }

        [JsonPropertyName("Order")]
        public System.Int32? Order { get; set; }

        [JsonPropertyName("ImageID")]
        public System.Int64 ImageID { get; set; }


    }
}
