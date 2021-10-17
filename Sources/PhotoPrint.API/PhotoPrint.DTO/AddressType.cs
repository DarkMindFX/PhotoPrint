

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class AddressType : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("AddressTypeName")]
        public System.String AddressTypeName { get; set; }

        [JsonPropertyName("IsDeleted")]
        public System.Boolean IsDeleted { get; set; }


    }
}
