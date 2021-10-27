

const HateosDto = require('./HateosDto')

class ImageDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get title() { return this.Title; }
		set title(val) { this.Title = val; }

		
		get description() { return this.Description; }
		set description(val) { this.Description = val; }

		
		get originurl() { return this.OriginUrl; }
		set originurl(val) { this.OriginUrl = val; }

		
		get maxwidth() { return this.MaxWidth; }
		set maxwidth(val) { this.MaxWidth = val; }

		
		get maxheight() { return this.MaxHeight; }
		set maxheight(val) { this.MaxHeight = val; }

		
		get priceamount() { return this.PriceAmount; }
		set priceamount(val) { this.PriceAmount = val; }

		
		get pricecurrencyid() { return this.PriceCurrencyID; }
		set pricecurrencyid(val) { this.PriceCurrencyID = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

		
		get createdbyid() { return this.CreatedByID; }
		set createdbyid(val) { this.CreatedByID = val; }

		
		get createddate() { return this.CreatedDate; }
		set createddate(val) { this.CreatedDate = val; }

		
		get modifiedbyid() { return this.ModifiedByID; }
		set modifiedbyid(val) { this.ModifiedByID = val; }

		
		get modifieddate() { return this.ModifiedDate; }
		set modifieddate(val) { this.ModifiedDate = val; }

				
}

module.exports = ImageDto;