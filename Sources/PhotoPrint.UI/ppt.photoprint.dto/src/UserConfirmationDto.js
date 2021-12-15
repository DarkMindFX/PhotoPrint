

import HateosDto from './HateosDto'

class UserConfirmationDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get userid() { return this.UserID; }
		set userid(val) { this.UserID = val; }

		
		get confirmationcode() { return this.ConfirmationCode; }
		set confirmationcode(val) { this.ConfirmationCode = val; }

		
		get comfirmed() { return this.Comfirmed; }
		set comfirmed(val) { this.Comfirmed = val; }

		
		get expiresdate() { return this.ExpiresDate; }
		set expiresdate(val) { this.ExpiresDate = val; }

		
		get confirmationdate() { return this.ConfirmationDate; }
		set confirmationdate(val) { this.ConfirmationDate = val; }

				
}

export default UserConfirmationDto;