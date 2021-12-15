


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class PrintingHouseAddressesDal extends DalBase {

    constructor() {
        super();
    }

    async insertPrintingHouseAddress(newPrintingHouseAddress) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/printinghouseaddresses`, newPrintingHouseAddress);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updatePrintingHouseAddress(updatedPrintingHouseAddress) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/printinghouseaddresses`, updatedPrintingHouseAddress);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deletePrintingHouseAddress(printinghouseid,addressid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/printinghouseaddresses/${printinghouseid}/${addressid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getPrintingHouseAddresses()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/printinghouseaddresses`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getPrintingHouseAddress(printinghouseid,addressid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/printinghouseaddresses/${printinghouseid}/${addressid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default PrintingHouseAddressesDal;