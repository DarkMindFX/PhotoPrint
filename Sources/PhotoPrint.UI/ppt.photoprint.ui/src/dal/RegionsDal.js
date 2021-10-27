


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class RegionsDal extends DalBase {

    constructor() {
        super();
    }

    async insertRegion(newRegion) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/regions`, newRegion);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateRegion(updatedRegion) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/regions`, updatedRegion);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteRegion(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/regions/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getRegions()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/regions`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getRegion(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/regions/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = RegionsDal;