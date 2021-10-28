


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');
const { LoginRequest } = require('ppt.photoprint.dto')


class UsersDal extends DalBase {

    constructor() {
        super();
    }

    async login(login, password) {

        let inst = this.Instance;

        try {

            var reqLogin = new LoginRequest();
            reqLogin.Login = login;
            reqLogin.Password = password;

            let res = await inst.post(`/users/login`, reqLogin);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }

    }

    async insertUser(newUser) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/users`, newUser);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateUser(updatedUser) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/users`, updatedUser);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteUser(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/users/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getUsers()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/users`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getUser(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/users/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = UsersDal;