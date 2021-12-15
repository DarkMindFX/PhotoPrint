

import HateosDto from './HateosDto'

class UserContactDto extends HateosDto {
		
		get userid() { return this.UserID; }
		set userid(val) { this.UserID = val; }

		
		get contactid() { return this.ContactID; }
		set contactid(val) { this.ContactID = val; }

		
		get isprimary() { return this.IsPrimary; }
		set isprimary(val) { this.IsPrimary = val; }

				
}

export default UserContactDto;