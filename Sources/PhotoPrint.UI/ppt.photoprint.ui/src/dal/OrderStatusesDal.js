


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class OrderStatusesDal extends DalBase {

    constructor() {
        super();
    }

    async insertOrderStatus(newOrderStatus) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/orderstatuses`, newOrderStatus);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateOrderStatus(updatedOrderStatus) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/orderstatuses`, updatedOrderStatus);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteOrderStatus(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/orderstatuses/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getOrderStatuses()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/orderstatuses`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getOrderStatus(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/orderstatuses/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = OrderStatusesDal;