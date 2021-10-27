


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class PrintingHousesDal extends DalBase {

    constructor() {
        super();
    }

    async insertPrintingHouse(newPrintingHouse) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/printinghouses`, newPrintingHouse);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updatePrintingHouse(updatedPrintingHouse) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/printinghouses`, updatedPrintingHouse);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deletePrintingHouse(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/printinghouses/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getPrintingHouses()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/printinghouses`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getPrintingHouse(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/printinghouses/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = PrintingHousesDal;