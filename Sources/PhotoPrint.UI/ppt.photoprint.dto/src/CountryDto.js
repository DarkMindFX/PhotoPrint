

import HateosDto from './HateosDto'

class CountryDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get countryname() { return this.CountryName; }
		set countryname(val) { this.CountryName = val; }

		
		get iso() { return this.ISO; }
		set iso(val) { this.ISO = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

				
}

export default CountryDto;