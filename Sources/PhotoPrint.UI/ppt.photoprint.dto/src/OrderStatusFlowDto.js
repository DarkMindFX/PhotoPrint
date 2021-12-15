

import HateosDto from './HateosDto'

class OrderStatusFlowDto extends HateosDto {
		
		get fromstatusid() { return this.FromStatusID; }
		set fromstatusid(val) { this.FromStatusID = val; }

		
		get tostatusid() { return this.ToStatusID; }
		set tostatusid(val) { this.ToStatusID = val; }

				
}

export default OrderStatusFlowDto;