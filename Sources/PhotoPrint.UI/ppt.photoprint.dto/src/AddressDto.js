

import HateosDto from './HateosDto'

class AddressDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get addresstypeid() { return this.AddressTypeID; }
		set addresstypeid(val) { this.AddressTypeID = val; }

		
		get title() { return this.Title; }
		set title(val) { this.Title = val; }

		
		get cityid() { return this.CityID; }
		set cityid(val) { this.CityID = val; }

		
		get street() { return this.Street; }
		set street(val) { this.Street = val; }

		
		get buildingno() { return this.BuildingNo; }
		set buildingno(val) { this.BuildingNo = val; }

		
		get apartmentno() { return this.ApartmentNo; }
		set apartmentno(val) { this.ApartmentNo = val; }

		
		get comment() { return this.Comment; }
		set comment(val) { this.Comment = val; }

		
		get createdbyid() { return this.CreatedByID; }
		set createdbyid(val) { this.CreatedByID = val; }

		
		get createddate() { return this.CreatedDate; }
		set createddate(val) { this.CreatedDate = val; }

		
		get modifiedbyid() { return this.ModifiedByID; }
		set modifiedbyid(val) { this.ModifiedByID = val; }

		
		get modifieddate() { return this.ModifiedDate; }
		set modifieddate(val) { this.ModifiedDate = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

				
}

export default AddressDto;