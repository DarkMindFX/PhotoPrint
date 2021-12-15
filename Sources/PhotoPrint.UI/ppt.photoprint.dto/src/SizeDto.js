

import HateosDto from './HateosDto'

class SizeDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get sizename() { return this.SizeName; }
		set sizename(val) { this.SizeName = val; }

		
		get width() { return this.Width; }
		set width(val) { this.Width = val; }

		
		get height() { return this.Height; }
		set height(val) { this.Height = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

		
		get createddate() { return this.CreatedDate; }
		set createddate(val) { this.CreatedDate = val; }

		
		get createdbyid() { return this.CreatedByID; }
		set createdbyid(val) { this.CreatedByID = val; }

		
		get modifieddate() { return this.ModifiedDate; }
		set modifieddate(val) { this.ModifiedDate = val; }

		
		get modifiedbyid() { return this.ModifiedByID; }
		set modifiedbyid(val) { this.ModifiedByID = val; }

				
}

export default SizeDto;