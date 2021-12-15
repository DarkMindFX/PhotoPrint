


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class UserTypesDal extends DalBase {

    constructor() {
        super();
    }

    async insertUserType(newUserType) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/usertypes`, newUserType);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateUserType(updatedUserType) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/usertypes`, updatedUserType);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteUserType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/usertypes/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserTypes()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/usertypes`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/usertypes/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default UserTypesDal;