


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class UnitsDal extends DalBase {

    constructor() {
        super();
    }

    async insertUnit(newUnit) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/units`, newUnit);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateUnit(updatedUnit) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/units`, updatedUnit);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteUnit(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/units/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getUnits()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/units`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getUnit(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/units/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = UnitsDal;