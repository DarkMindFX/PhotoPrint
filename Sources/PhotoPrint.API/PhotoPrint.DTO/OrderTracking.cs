

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class OrderTracking : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("OrderID")]
        public System.Int64 OrderID { get; set; }

        [JsonPropertyName("OrderStatusID")]
        public System.Int64 OrderStatusID { get; set; }

        [JsonPropertyName("SetDate")]
        public System.DateTime SetDate { get; set; }

        [JsonPropertyName("SetByID")]
        public System.Int64 SetByID { get; set; }

        [JsonPropertyName("Comment")]
        public System.String Comment { get; set; }


    }
}
