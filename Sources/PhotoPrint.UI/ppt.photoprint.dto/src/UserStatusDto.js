

const HateosDto = require('./HateosDto')

class UserStatusDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get statusname() { return this.StatusName; }
		set statusname(val) { this.StatusName = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

				
}

module.exports = UserStatusDto;