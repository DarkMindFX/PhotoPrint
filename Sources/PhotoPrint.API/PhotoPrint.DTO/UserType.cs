

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class UserType : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("UserTypeName")]
        public System.String UserTypeName { get; set; }

        [JsonPropertyName("IsDeleted")]
        public System.Boolean IsDeleted { get; set; }


    }
}
