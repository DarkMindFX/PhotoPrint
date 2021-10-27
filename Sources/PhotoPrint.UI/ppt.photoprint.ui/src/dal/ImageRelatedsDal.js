


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class ImageRelatedsDal extends DalBase {

    constructor() {
        super();
    }

    async insertImageRelated(newImageRelated) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/imagerelateds`, newImageRelated);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateImageRelated(updatedImageRelated) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/imagerelateds`, updatedImageRelated);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteImageRelated(imageid,relatedimageid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/imagerelateds/${imageid}/${relatedimageid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getImageRelateds()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/imagerelateds`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getImageRelated(imageid,relatedimageid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/imagerelateds/${imageid}/${relatedimageid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = ImageRelatedsDal;