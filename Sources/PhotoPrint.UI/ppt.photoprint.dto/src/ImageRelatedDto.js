

const HateosDto = require('./HateosDto')

class ImageRelatedDto extends HateosDto {
		
		get imageid() { return this.ImageID; }
		set imageid(val) { this.ImageID = val; }

		
		get relatedimageid() { return this.RelatedImageID; }
		set relatedimageid(val) { this.RelatedImageID = val; }

				
}

module.exports = ImageRelatedDto;