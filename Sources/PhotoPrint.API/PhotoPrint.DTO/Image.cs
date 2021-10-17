

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class Image : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("Title")]
        public System.String Title { get; set; }

        [JsonPropertyName("Description")]
        public System.String Description { get; set; }

        [JsonPropertyName("OriginUrl")]
        public System.String OriginUrl { get; set; }

        [JsonPropertyName("MaxWidth")]
        public System.Int32? MaxWidth { get; set; }

        [JsonPropertyName("MaxHeight")]
        public System.Int32? MaxHeight { get; set; }

        [JsonPropertyName("PriceAmount")]
        public System.Decimal? PriceAmount { get; set; }

        [JsonPropertyName("PriceCurrencyID")]
        public System.Int64? PriceCurrencyID { get; set; }

        [JsonPropertyName("IsDeleted")]
        public System.Boolean IsDeleted { get; set; }

        [JsonPropertyName("CreatedByID")]
        public System.Int64 CreatedByID { get; set; }

        [JsonPropertyName("CreatedDate")]
        public System.DateTime CreatedDate { get; set; }

        [JsonPropertyName("ModifiedByID")]
        public System.Int64? ModifiedByID { get; set; }

        [JsonPropertyName("ModifiedDate")]
        public System.DateTime? ModifiedDate { get; set; }


    }
}
