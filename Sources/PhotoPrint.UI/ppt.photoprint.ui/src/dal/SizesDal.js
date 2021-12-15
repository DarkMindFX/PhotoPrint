


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class SizesDal extends DalBase {

    constructor() {
        super();
    }

    async insertSize(newSize) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/sizes`, newSize);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateSize(updatedSize) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/sizes`, updatedSize);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteSize(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/sizes/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getSizes()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/sizes`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getSize(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/sizes/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default SizesDal;