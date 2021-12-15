


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class FrameTypesDal extends DalBase {

    constructor() {
        super();
    }

    async insertFrameType(newFrameType) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/frametypes`, newFrameType);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateFrameType(updatedFrameType) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/frametypes`, updatedFrameType);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteFrameType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/frametypes/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getFrameTypes()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/frametypes`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getFrameType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/frametypes/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default FrameTypesDal;