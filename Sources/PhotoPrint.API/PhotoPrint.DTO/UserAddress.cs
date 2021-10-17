

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class UserAddress : HateosDto
    {
        [JsonPropertyName("UserID")]
        public System.Int64 UserID { get; set; }

        [JsonPropertyName("AddressID")]
        public System.Int64 AddressID { get; set; }

        [JsonPropertyName("IsPrimary")]
        public System.Boolean IsPrimary { get; set; }


    }
}
