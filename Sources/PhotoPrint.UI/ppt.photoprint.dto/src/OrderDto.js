

import HateosDto from './HateosDto'

class OrderDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get managerid() { return this.ManagerID; }
		set managerid(val) { this.ManagerID = val; }

		
		get userid() { return this.UserID; }
		set userid(val) { this.UserID = val; }

		
		get contactid() { return this.ContactID; }
		set contactid(val) { this.ContactID = val; }

		
		get deliveryaddressid() { return this.DeliveryAddressID; }
		set deliveryaddressid(val) { this.DeliveryAddressID = val; }

		
		get deliveryserviceid() { return this.DeliveryServiceID; }
		set deliveryserviceid(val) { this.DeliveryServiceID = val; }

		
		get comments() { return this.Comments; }
		set comments(val) { this.Comments = val; }

		
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

export default OrderDto;