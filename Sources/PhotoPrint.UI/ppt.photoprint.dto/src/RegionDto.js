

const HateosDto = require('./HateosDto')

class RegionDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get regionname() { return this.RegionName; }
		set regionname(val) { this.RegionName = val; }

		
		get countryid() { return this.CountryID; }
		set countryid(val) { this.CountryID = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

				
}

module.exports = RegionDto;