

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class UserContact : HateosDto
    {
        [JsonPropertyName("UserID")]
        public System.Int64 UserID { get; set; }

        [JsonPropertyName("ContactID")]
        public System.Int64 ContactID { get; set; }

        [JsonPropertyName("IsPrimary")]
        public System.Boolean IsPrimary { get; set; }


    }
}
