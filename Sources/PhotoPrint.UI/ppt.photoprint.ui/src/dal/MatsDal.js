


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class MatsDal extends DalBase {

    constructor() {
        super();
    }

    async insertMat(newMat) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/mats`, newMat);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateMat(updatedMat) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/mats`, updatedMat);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteMat(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/mats/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getMats()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/mats`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getMat(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/mats/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default MatsDal;