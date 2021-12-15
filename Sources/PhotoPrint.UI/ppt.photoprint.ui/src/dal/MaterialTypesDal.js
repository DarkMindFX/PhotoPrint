


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class MaterialTypesDal extends DalBase {

    constructor() {
        super();
    }

    async insertMaterialType(newMaterialType) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/materialtypes`, newMaterialType);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateMaterialType(updatedMaterialType) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/materialtypes`, updatedMaterialType);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteMaterialType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/materialtypes/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getMaterialTypes()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/materialtypes`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getMaterialType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/materialtypes/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default MaterialTypesDal;