


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class AddressesDal extends DalBase {

    constructor() {
        super();
    }

    async insertAddress(newAddress) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/addresses`, newAddress);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateAddress(updatedAddress) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/addresses`, updatedAddress);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteAddress(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/addresses/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getAddresses()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/addresses`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getAddress(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/addresses/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = AddressesDal;