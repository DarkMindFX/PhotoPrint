


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class PrintingHouseContactsDal extends DalBase {

    constructor() {
        super();
    }

    async insertPrintingHouseContact(newPrintingHouseContact) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/printinghousecontacts`, newPrintingHouseContact);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updatePrintingHouseContact(updatedPrintingHouseContact) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/printinghousecontacts`, updatedPrintingHouseContact);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deletePrintingHouseContact(printinghouseid,contactid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/printinghousecontacts/${printinghouseid}/${contactid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getPrintingHouseContacts()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/printinghousecontacts`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getPrintingHouseContact(printinghouseid,contactid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/printinghousecontacts/${printinghouseid}/${contactid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = PrintingHouseContactsDal;