

import HateosDto from './HateosDto'

class OrderTrackingDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get orderid() { return this.OrderID; }
		set orderid(val) { this.OrderID = val; }

		
		get orderstatusid() { return this.OrderStatusID; }
		set orderstatusid(val) { this.OrderStatusID = val; }

		
		get setdate() { return this.SetDate; }
		set setdate(val) { this.SetDate = val; }

		
		get setbyid() { return this.SetByID; }
		set setbyid(val) { this.SetByID = val; }

		
		get comment() { return this.Comment; }
		set comment(val) { this.Comment = val; }

				
}

export default OrderTrackingDto;