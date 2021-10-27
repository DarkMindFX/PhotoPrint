


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class UserContactsDal extends DalBase {

    constructor() {
        super();
    }

    async insertUserContact(newUserContact) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/usercontacts`, newUserContact);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateUserContact(updatedUserContact) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/usercontacts`, updatedUserContact);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteUserContact(userid,contactid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/usercontacts/${userid}/${contactid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserContacts()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/usercontacts`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserContact(userid,contactid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/usercontacts/${userid}/${contactid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = UserContactsDal;