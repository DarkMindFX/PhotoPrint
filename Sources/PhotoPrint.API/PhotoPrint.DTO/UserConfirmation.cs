

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class UserConfirmation : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("UserID")]
		public System.Int64 UserID { get; set; }

				[JsonPropertyName("ConfirmationCode")]
		public System.String ConfirmationCode { get; set; }

				[JsonPropertyName("Comfirmed")]
		public System.Boolean Comfirmed { get; set; }

				[JsonPropertyName("ExpiresDate")]
		public System.DateTime ExpiresDate { get; set; }

				[JsonPropertyName("ConfirmationDate")]
		public System.DateTime? ConfirmationDate { get; set; }

				
    }
}
