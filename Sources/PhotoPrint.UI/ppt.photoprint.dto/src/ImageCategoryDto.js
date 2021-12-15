

import HateosDto from './HateosDto'

class ImageCategoryDto extends HateosDto {
		
		get imageid() { return this.ImageID; }
		set imageid(val) { this.ImageID = val; }

		
		get categoryid() { return this.CategoryID; }
		set categoryid(val) { this.CategoryID = val; }

				
}

export default ImageCategoryDto;