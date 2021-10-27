


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class UserAddressesDal extends DalBase {

    constructor() {
        super();
    }

    async insertUserAddress(newUserAddress) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/useraddresses`, newUserAddress);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateUserAddress(updatedUserAddress) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/useraddresses`, updatedUserAddress);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteUserAddress(userid,addressid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/useraddresses/${userid}/${addressid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserAddresses()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/useraddresses`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserAddress(userid,addressid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/useraddresses/${userid}/${addressid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = UserAddressesDal;