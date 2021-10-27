

const HateosDto = require('./HateosDto')

class UnitDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get unitname() { return this.UnitName; }
		set unitname(val) { this.UnitName = val; }

		
		get unitabbr() { return this.UnitAbbr; }
		set unitabbr(val) { this.UnitAbbr = val; }

		
		get description() { return this.Description; }
		set description(val) { this.Description = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

				
}

module.exports = UnitDto;