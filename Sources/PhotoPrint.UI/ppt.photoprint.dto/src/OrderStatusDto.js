

import HateosDto from './HateosDto'

class OrderStatusDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get orderstatusname() { return this.OrderStatusName; }
		set orderstatusname(val) { this.OrderStatusName = val; }

		
		get isdeleted() { return this.IsDeleted; }
		set isdeleted(val) { this.IsDeleted = val; }

				
}

export default OrderStatusDto;