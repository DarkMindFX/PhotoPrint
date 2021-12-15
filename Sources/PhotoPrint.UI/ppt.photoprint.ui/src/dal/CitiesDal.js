


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class CitiesDal extends DalBase {

    constructor() {
        super();
    }

    async insertCity(newCity) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/cities`, newCity);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateCity(updatedCity) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/cities`, updatedCity);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteCity(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/cities/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getCities()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/cities`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getCity(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/cities/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default CitiesDal;