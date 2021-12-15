

import HateosDto from './HateosDto'

class OrderItemDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get orderid() { return this.OrderID; }
		set orderid(val) { this.OrderID = val; }

		
		get imageid() { return this.ImageID; }
		set imageid(val) { this.ImageID = val; }

		
		get width() { return this.Width; }
		set width(val) { this.Width = val; }

		
		get height() { return this.Height; }
		set height(val) { this.Height = val; }

		
		get sizeid() { return this.SizeID; }
		set sizeid(val) { this.SizeID = val; }

		
		get frametypeid() { return this.FrameTypeID; }
		set frametypeid(val) { this.FrameTypeID = val; }

		
		get framesizeid() { return this.FrameSizeID; }
		set framesizeid(val) { this.FrameSizeID = val; }

		
		get matid() { return this.MatID; }
		set matid(val) { this.MatID = val; }

		
		get materialtypeid() { return this.MaterialTypeID; }
		set materialtypeid(val) { this.MaterialTypeID = val; }

		
		get mountingtypeid() { return this.MountingTypeID; }
		set mountingtypeid(val) { this.MountingTypeID = val; }

		
		get itemcount() { return this.ItemCount; }
		set itemcount(val) { this.ItemCount = val; }

		
		get priceamountperitem() { return this.PriceAmountPerItem; }
		set priceamountperitem(val) { this.PriceAmountPerItem = val; }

		
		get pricecurrencyid() { return this.PriceCurrencyID; }
		set pricecurrencyid(val) { this.PriceCurrencyID = val; }

		
		get comments() { return this.Comments; }
		set comments(val) { this.Comments = val; }

		
		get printinghouseid() { return this.PrintingHouseID; }
		set printinghouseid(val) { this.PrintingHouseID = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

		
		get createddate() { return this.CreatedDate; }
		set createddate(val) { this.CreatedDate = val; }

		
		get createdbyid() { return this.CreatedByID; }
		set createdbyid(val) { this.CreatedByID = val; }

		
		get modifieddate() { return this.ModifiedDate; }
		set modifieddate(val) { this.ModifiedDate = val; }

		
		get modifiedbyid() { return this.ModifiedByID; }
		set modifiedbyid(val) { this.ModifiedByID = val; }

				
}

export default OrderItemDto;