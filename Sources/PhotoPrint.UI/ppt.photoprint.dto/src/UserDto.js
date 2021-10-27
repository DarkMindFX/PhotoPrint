

const HateosDto = require('./HateosDto')

class UserDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get login() { return this.Login; }
		set login(val) { this.Login = val; }

		
		get pwdhash() { return this.PwdHash; }
		set pwdhash(val) { this.PwdHash = val; }

		
		get salt() { return this.Salt; }
		set salt(val) { this.Salt = val; }

		
		get firstname() { return this.FirstName; }
		set firstname(val) { this.FirstName = val; }

		
		get middlename() { return this.MiddleName; }
		set middlename(val) { this.MiddleName = val; }

		
		get lastname() { return this.LastName; }
		set lastname(val) { this.LastName = val; }

		
		get friendlyname() { return this.FriendlyName; }
		set friendlyname(val) { this.FriendlyName = val; }

		
		get userstatusid() { return this.UserStatusID; }
		set userstatusid(val) { this.UserStatusID = val; }

		
		get usertypeid() { return this.UserTypeID; }
		set usertypeid(val) { this.UserTypeID = val; }

		
		get createddate() { return this.CreatedDate; }
		set createddate(val) { this.CreatedDate = val; }

		
		get modifieddate() { return this.ModifiedDate; }
		set modifieddate(val) { this.ModifiedDate = val; }

		
		get modifiedbyid() { return this.ModifiedByID; }
		set modifiedbyid(val) { this.ModifiedByID = val; }

				
}

module.exports = UserDto;