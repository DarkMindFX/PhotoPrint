

using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class OrderItem : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("OrderID")]
		public System.Int64 OrderID { get; set; }

				[JsonPropertyName("ImageID")]
		public System.Int64 ImageID { get; set; }

				[JsonPropertyName("Width")]
		public System.Int32? Width { get; set; }

				[JsonPropertyName("Height")]
		public System.Int32? Height { get; set; }

				[JsonPropertyName("SizeID")]
		public System.Int64? SizeID { get; set; }

				[JsonPropertyName("FrameTypeID")]
		public System.Int64 FrameTypeID { get; set; }

				[JsonPropertyName("FrameSizeID")]
		public System.Int64? FrameSizeID { get; set; }

				[JsonPropertyName("MatID")]
		public System.Int64 MatID { get; set; }

				[JsonPropertyName("MaterialTypeID")]
		public System.Int64 MaterialTypeID { get; set; }

				[JsonPropertyName("MountingTypeID")]
		public System.Int64 MountingTypeID { get; set; }

				[JsonPropertyName("ItemCount")]
		public System.Int32 ItemCount { get; set; }

				[JsonPropertyName("PriceAmountPerItem")]
		public System.Decimal PriceAmountPerItem { get; set; }

				[JsonPropertyName("PriceCurrencyID")]
		public System.Int64 PriceCurrencyID { get; set; }

				[JsonPropertyName("Comments")]
		public System.String Comments { get; set; }

				[JsonPropertyName("PrintingHouseID")]
		public System.Int64? PrintingHouseID { get; set; }

				[JsonPropertyName("IsDeleted")]
		public System.Boolean IsDeleted { get; set; }

				[JsonPropertyName("CreatedDate")]
		public System.DateTime CreatedDate { get; set; }

				[JsonPropertyName("CreatedByID")]
		public System.Int64 CreatedByID { get; set; }

				[JsonPropertyName("ModifiedDate")]
		public System.DateTime? ModifiedDate { get; set; }

				[JsonPropertyName("ModifiedByID")]
		public System.Int64? ModifiedByID { get; set; }

				
    }
}
