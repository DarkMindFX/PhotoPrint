


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class ImageCategoriesDal extends DalBase {

    constructor() {
        super();
    }

    async insertImageCategory(newImageCategory) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/imagecategories`, newImageCategory);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateImageCategory(updatedImageCategory) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/imagecategories`, updatedImageCategory);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteImageCategory(imageid,categoryid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/imagecategories/${imageid}/${categoryid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getImageCategories()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/imagecategories`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getImageCategory(imageid,categoryid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/imagecategories/${imageid}/${categoryid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = ImageCategoriesDal;