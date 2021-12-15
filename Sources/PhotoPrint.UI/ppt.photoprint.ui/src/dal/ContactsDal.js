


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class ContactsDal extends DalBase {

    constructor() {
        super();
    }

    async insertContact(newContact) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/contacts`, newContact);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateContact(updatedContact) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/contacts`, updatedContact);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteContact(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/contacts/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getContacts()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/contacts`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getContact(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/contacts/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default ContactsDal;