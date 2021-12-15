


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class OrderStatusFlowsDal extends DalBase {

    constructor() {
        super();
    }

    async insertOrderStatusFlow(newOrderStatusFlow) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/orderstatusflows`, newOrderStatusFlow);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateOrderStatusFlow(updatedOrderStatusFlow) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/orderstatusflows`, updatedOrderStatusFlow);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteOrderStatusFlow(fromstatusid,tostatusid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/orderstatusflows/${fromstatusid}/${tostatusid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getOrderStatusFlows()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/orderstatusflows`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getOrderStatusFlow(fromstatusid,tostatusid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/orderstatusflows/${fromstatusid}/${tostatusid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default OrderStatusFlowsDal;