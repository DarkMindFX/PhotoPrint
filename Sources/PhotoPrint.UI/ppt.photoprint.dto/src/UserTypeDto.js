

import HateosDto from './HateosDto'

class UserTypeDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get usertypename() { return this.UserTypeName; }
		set usertypename(val) { this.UserTypeName = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

				
}

export default UserTypeDto;