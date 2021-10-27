


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class CurrenciesDal extends DalBase {

    constructor() {
        super();
    }

    async insertCurrency(newCurrency) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/currencies`, newCurrency);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateCurrency(updatedCurrency) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/currencies`, updatedCurrency);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteCurrency(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/currencies/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getCurrencies()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/currencies`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getCurrency(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/currencies/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = CurrenciesDal;