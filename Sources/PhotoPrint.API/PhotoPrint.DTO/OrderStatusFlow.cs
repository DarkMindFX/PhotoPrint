

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class OrderStatusFlow : HateosDto
    {
        [JsonPropertyName("FromStatusID")]
        public System.Int64 FromStatusID { get; set; }

        [JsonPropertyName("ToStatusID")]
        public System.Int64 ToStatusID { get; set; }


    }
}
