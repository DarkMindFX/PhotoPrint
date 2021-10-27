

const HateosDto = require('./HateosDto')

class CurrencyDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get iso() { return this.ISO; }
		set iso(val) { this.ISO = val; }

		
		get currencyname() { return this.CurrencyName; }
		set currencyname(val) { this.CurrencyName = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

				
}

module.exports = CurrencyDto;