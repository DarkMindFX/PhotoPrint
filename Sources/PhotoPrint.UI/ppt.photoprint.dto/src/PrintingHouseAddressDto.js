

import HateosDto from './HateosDto'

class PrintingHouseAddressDto extends HateosDto {
		
		get printinghouseid() { return this.PrintingHouseID; }
		set printinghouseid(val) { this.PrintingHouseID = val; }

		
		get addressid() { return this.AddressID; }
		set addressid(val) { this.AddressID = val; }

		
		get isprimary() { return this.IsPrimary; }
		set isprimary(val) { this.IsPrimary = val; }

				
}

export default PrintingHouseAddressDto;