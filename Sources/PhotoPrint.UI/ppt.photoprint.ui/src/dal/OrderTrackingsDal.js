


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class OrderTrackingsDal extends DalBase {

    constructor() {
        super();
    }

    async insertOrderTracking(newOrderTracking) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/ordertrackings`, newOrderTracking);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateOrderTracking(updatedOrderTracking) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/ordertrackings`, updatedOrderTracking);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteOrderTracking(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/ordertrackings/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getOrderTrackings()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/ordertrackings`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getOrderTracking(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/ordertrackings/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default OrderTrackingsDal;