


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class UserConfirmationsDal extends DalBase {

    constructor() {
        super();
    }

    async insertUserConfirmation(newUserConfirmation) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/userconfirmations`, newUserConfirmation);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateUserConfirmation(updatedUserConfirmation) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/userconfirmations`, updatedUserConfirmation);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteUserConfirmation(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/userconfirmations/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserConfirmations()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/userconfirmations`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserConfirmation(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/userconfirmations/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = UserConfirmationsDal;