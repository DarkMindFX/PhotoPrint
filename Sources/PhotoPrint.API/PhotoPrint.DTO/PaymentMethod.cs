

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class PaymentMethod : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("Name")]
        public System.String Name { get; set; }

        [JsonPropertyName("Description")]
        public System.String Description { get; set; }

        [JsonPropertyName("IsDeleted")]
        public System.Boolean IsDeleted { get; set; }


    }
}
