


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class DeliveryServicesDal extends DalBase {

    constructor() {
        super();
    }

    async insertDeliveryService(newDeliveryService) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/deliveryservices`, newDeliveryService);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateDeliveryService(updatedDeliveryService) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/deliveryservices`, updatedDeliveryService);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteDeliveryService(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/deliveryservices/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getDeliveryServices()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/deliveryservices`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getDeliveryService(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/deliveryservices/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default DeliveryServicesDal;