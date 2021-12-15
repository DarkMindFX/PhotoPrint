


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class MountingTypesDal extends DalBase {

    constructor() {
        super();
    }

    async insertMountingType(newMountingType) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/mountingtypes`, newMountingType);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateMountingType(updatedMountingType) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/mountingtypes`, updatedMountingType);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteMountingType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/mountingtypes/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getMountingTypes()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/mountingtypes`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getMountingType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/mountingtypes/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default MountingTypesDal;