

import HateosDto from './HateosDto'

class DeliveryServiceCityDto extends HateosDto {
		
		get deliveryserviceid() { return this.DeliveryServiceID; }
		set deliveryserviceid(val) { this.DeliveryServiceID = val; }

		
		get cityid() { return this.CityID; }
		set cityid(val) { this.CityID = val; }

				
}

export default DeliveryServiceCityDto;