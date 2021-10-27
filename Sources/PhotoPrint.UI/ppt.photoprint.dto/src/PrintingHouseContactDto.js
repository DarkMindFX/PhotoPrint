

const HateosDto = require('./HateosDto')

class PrintingHouseContactDto extends HateosDto {
		
		get printinghouseid() { return this.PrintingHouseID; }
		set printinghouseid(val) { this.PrintingHouseID = val; }

		
		get contactid() { return this.ContactID; }
		set contactid(val) { this.ContactID = val; }

		
		get isprimary() { return this.IsPrimary; }
		set isprimary(val) { this.IsPrimary = val; }

				
}

module.exports = PrintingHouseContactDto;