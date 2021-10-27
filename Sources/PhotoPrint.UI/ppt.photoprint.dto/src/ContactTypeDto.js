

const HateosDto = require('./HateosDto')

class ContactTypeDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get contacttypename() { return this.ContactTypeName; }
		set contacttypename(val) { this.ContactTypeName = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

				
}

module.exports = ContactTypeDto;