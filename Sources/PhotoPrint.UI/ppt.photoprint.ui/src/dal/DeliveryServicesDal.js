


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


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

module.exports = DeliveryServicesDal;