


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class CategoriesDal extends DalBase {

    constructor() {
        super();
    }

    async insertCategory(newCategory) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/categories`, newCategory);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateCategory(updatedCategory) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/categories`, updatedCategory);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteCategory(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/categories/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getCategories()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/categories`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getCategory(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/categories/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = CategoriesDal;