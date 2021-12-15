


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class DeliveryServiceCitiesDal extends DalBase {

    constructor() {
        super();
    }

    async insertDeliveryServiceCity(newDeliveryServiceCity) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/deliveryservicecities`, newDeliveryServiceCity);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateDeliveryServiceCity(updatedDeliveryServiceCity) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/deliveryservicecities`, updatedDeliveryServiceCity);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteDeliveryServiceCity(deliveryserviceid,cityid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/deliveryservicecities/${deliveryserviceid}/${cityid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getDeliveryServiceCities()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/deliveryservicecities`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getDeliveryServiceCity(deliveryserviceid,cityid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/deliveryservicecities/${deliveryserviceid}/${cityid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default DeliveryServiceCitiesDal;