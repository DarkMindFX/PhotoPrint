


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class CountriesDal extends DalBase {

    constructor() {
        super();
    }

    async insertCountry(newCountry) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/countries`, newCountry);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateCountry(updatedCountry) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/countries`, updatedCountry);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteCountry(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/countries/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getCountries()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/countries`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getCountry(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/countries/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = CountriesDal;