


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class AddressTypesDal extends DalBase {

    constructor() {
        super();
    }

    async insertAddressType(newAddressType) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/addresstypes`, newAddressType);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateAddressType(updatedAddressType) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/addresstypes`, updatedAddressType);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteAddressType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/addresstypes/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getAddressTypes()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/addresstypes`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getAddressType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/addresstypes/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default AddressTypesDal;