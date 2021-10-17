

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class ImageRelated : HateosDto
    {
        [JsonPropertyName("ImageID")]
        public System.Int64 ImageID { get; set; }

        [JsonPropertyName("RelatedImageID")]
        public System.Int64 RelatedImageID { get; set; }


    }
}
