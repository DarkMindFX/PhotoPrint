


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class OrdersDal extends DalBase {

    constructor() {
        super();
    }

    async insertOrder(newOrder) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/orders`, newOrder);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateOrder(updatedOrder) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/orders`, updatedOrder);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteOrder(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/orders/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getOrders()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/orders`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getOrder(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/orders/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default OrdersDal;