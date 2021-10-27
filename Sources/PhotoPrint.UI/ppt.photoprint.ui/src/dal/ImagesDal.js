


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class ImagesDal extends DalBase {

    constructor() {
        super();
    }

    async insertImage(newImage) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/images`, newImage);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateImage(updatedImage) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/images`, updatedImage);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteImage(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/images/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getImages()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/images`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getImage(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/images/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = ImagesDal;