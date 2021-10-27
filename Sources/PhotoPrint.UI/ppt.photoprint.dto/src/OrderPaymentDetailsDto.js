

const HateosDto = require('./HateosDto')

class OrderPaymentDetailsDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get orderid() { return this.OrderID; }
		set orderid(val) { this.OrderID = val; }

		
		get paymentmethodid() { return this.PaymentMethodID; }
		set paymentmethodid(val) { this.PaymentMethodID = val; }

		
		get paymenttransuid() { return this.PaymentTransUID; }
		set paymenttransuid(val) { this.PaymentTransUID = val; }

		
		get paymentdatetime() { return this.PaymentDateTime; }
		set paymentdatetime(val) { this.PaymentDateTime = val; }

		
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

module.exports = OrderPaymentDetailsDto;