

const HateosDto = require('./HateosDto')

class UserAddressDto extends HateosDto {
		
		get userid() { return this.UserID; }
		set userid(val) { this.UserID = val; }

		
		get addressid() { return this.AddressID; }
		set addressid(val) { this.AddressID = val; }

		
		get isprimary() { return this.IsPrimary; }
		set isprimary(val) { this.IsPrimary = val; }

				
}

module.exports = UserAddressDto;