


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class ContactTypesDal extends DalBase {

    constructor() {
        super();
    }

    async insertContactType(newContactType) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/contacttypes`, newContactType);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateContactType(updatedContactType) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/contacttypes`, updatedContactType);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteContactType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/contacttypes/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getContactTypes()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/contacttypes`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getContactType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/contacttypes/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default ContactTypesDal;