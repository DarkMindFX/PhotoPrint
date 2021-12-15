

import HateosDto from './HateosDto'

class CityDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get cityname() { return this.CityName; }
		set cityname(val) { this.CityName = val; }

		
		get regionid() { return this.RegionID; }
		set regionid(val) { this.RegionID = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

				
}

export default CityDto;