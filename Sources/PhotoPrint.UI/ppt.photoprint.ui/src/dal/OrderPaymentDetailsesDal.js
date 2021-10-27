


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class OrderPaymentDetailsesDal extends DalBase {

    constructor() {
        super();
    }

    async insertOrderPaymentDetails(newOrderPaymentDetails) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/orderpaymentdetailses`, newOrderPaymentDetails);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateOrderPaymentDetails(updatedOrderPaymentDetails) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/orderpaymentdetailses`, updatedOrderPaymentDetails);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteOrderPaymentDetails(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/orderpaymentdetailses/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getOrderPaymentDetailses()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/orderpaymentdetailses`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getOrderPaymentDetails(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/orderpaymentdetailses/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = OrderPaymentDetailsesDal;