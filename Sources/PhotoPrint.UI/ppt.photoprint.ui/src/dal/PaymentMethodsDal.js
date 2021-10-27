


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class PaymentMethodsDal extends DalBase {

    constructor() {
        super();
    }

    async insertPaymentMethod(newPaymentMethod) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/paymentmethods`, newPaymentMethod);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updatePaymentMethod(updatedPaymentMethod) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/paymentmethods`, updatedPaymentMethod);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deletePaymentMethod(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/paymentmethods/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getPaymentMethods()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/paymentmethods`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getPaymentMethod(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/paymentmethods/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = PaymentMethodsDal;