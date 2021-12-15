

import HateosDto from './HateosDto'

class ImageThumbnailDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get url() { return this.Url; }
		set url(val) { this.Url = val; }

		
		get order() { return this.Order; }
		set order(val) { this.Order = val; }

		
		get imageid() { return this.ImageID; }
		set imageid(val) { this.ImageID = val; }

				
}

export default ImageThumbnailDto;