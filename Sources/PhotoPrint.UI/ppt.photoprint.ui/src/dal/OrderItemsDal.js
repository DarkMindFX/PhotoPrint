


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class OrderItemsDal extends DalBase {

    constructor() {
        super();
    }

    async insertOrderItem(newOrderItem) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/orderitems`, newOrderItem);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateOrderItem(updatedOrderItem) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/orderitems`, updatedOrderItem);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteOrderItem(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/orderitems/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getOrderItems()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/orderitems`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getOrderItem(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/orderitems/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default OrderItemsDal;