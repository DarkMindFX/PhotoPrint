


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


class ImageThumbnailsDal extends DalBase {

    constructor() {
        super();
    }

    async insertImageThumbnail(newImageThumbnail) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/imagethumbnails`, newImageThumbnail);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateImageThumbnail(updatedImageThumbnail) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/imagethumbnails`, updatedImageThumbnail);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteImageThumbnail(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/imagethumbnails/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getImageThumbnails()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/imagethumbnails`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getImageThumbnail(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/imagethumbnails/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

export default ImageThumbnailsDal;