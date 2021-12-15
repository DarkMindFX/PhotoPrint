

import HateosDto from './HateosDto'

class PaymentMethodDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get name() { return this.Name; }
		set name(val) { this.Name = val; }

		
		get description() { return this.Description; }
		set description(val) { this.Description = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

				
}

export default PaymentMethodDto;