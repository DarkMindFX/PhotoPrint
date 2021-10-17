

using System.Text.Json.Serialization;

namespace PhotoPrint.DTO
{
    public class Address : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("AddressTypeID")]
        public System.Int64 AddressTypeID { get; set; }

        [JsonPropertyName("Title")]
        public System.String Title { get; set; }

        [JsonPropertyName("CityID")]
        public System.Int64 CityID { get; set; }

        [JsonPropertyName("Street")]
        public System.String Street { get; set; }

        [JsonPropertyName("BuildingNo")]
        public System.String BuildingNo { get; set; }

        [JsonPropertyName("ApartmentNo")]
        public System.String ApartmentNo { get; set; }

        [JsonPropertyName("Comment")]
        public System.String Comment { get; set; }

        [JsonPropertyName("CreatedByID")]
        public System.Int64 CreatedByID { get; set; }

        [JsonPropertyName("CreatedDate")]
        public System.DateTime CreatedDate { get; set; }

        [JsonPropertyName("ModifiedByID")]
        public System.Int64? ModifiedByID { get; set; }

        [JsonPropertyName("ModifiedDate")]
        public System.DateTime? ModifiedDate { get; set; }

        [JsonPropertyName("IsDeleted")]
        public System.Boolean IsDeleted { get; set; }


    }
}
