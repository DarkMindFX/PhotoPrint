


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class UserStatusesDal extends DalBase {

    constructor() {
        super();
    }

    async insertUserStatus(newUserStatus) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/userstatuses`, newUserStatus);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateUserStatus(updatedUserStatus) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/userstatuses`, updatedUserStatus);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteUserStatus(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/userstatuses/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserStatuses()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/userstatuses`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserStatus(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/userstatuses/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = UserStatusesDal;