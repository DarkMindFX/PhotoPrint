

const HateosDto = require('./HateosDto')

class FrameTypeDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get frametypename() { return this.FrameTypeName; }
		set frametypename(val) { this.FrameTypeName = val; }

		
		get description() { return this.Description; }
		set description(val) { this.Description = val; }

		
		get thumbnailurl() { return this.ThumbnailUrl; }
		set thumbnailurl(val) { this.ThumbnailUrl = val; }

		
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

module.exports = FrameTypeDto;