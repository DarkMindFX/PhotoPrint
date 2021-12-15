

import HateosDto from './HateosDto'

class AddressTypeDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get addresstypename() { return this.AddressTypeName; }
		set addresstypename(val) { this.AddressTypeName = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

				
}

export default AddressTypeDto;