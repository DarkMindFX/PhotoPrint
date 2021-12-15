

import HateosDto from './HateosDto'

class ContactDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get contacttypeid() { return this.ContactTypeID; }
		set contacttypeid(val) { this.ContactTypeID = val; }

		
		get title() { return this.Title; }
		set title(val) { this.Title = val; }

		
		get comment() { return this.Comment; }
		set comment(val) { this.Comment = val; }

		
		get value() { return this.Value; }
		set value(val) { this.Value = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

		
		get createdbyid() { return this.CreatedByID; }
		set createdbyid(val) { this.CreatedByID = val; }

		
		get createddate() { return this.CreatedDate; }
		set createddate(val) { this.CreatedDate = val; }

		
		get modifiedbyid() { return this.ModifiedByID; }
		set modifiedbyid(val) { this.ModifiedByID = val; }

		
		get modifieddate() { return this.ModifiedDate; }
		set modifieddate(val) { this.ModifiedDate = val; }

				
}

export default ContactDto;