


import axios from 'axios';
import constants from '../constants';

import DalBase from './DalBase';


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

    async uploadThumbnails(id, thumbnails)
    {
        let inst = this.Instance;

        const formData = new FormData();
        for(let i in thumbnails)
        {
            if(thumbnails[i] != null)
            {
                console.log(thumbnails[i]);
                formData.append("files", thumbnails[i]);   
            }         
        }

        try {
            console.log(formData);
            let res = await inst.post(`/thumbnails/images/${id}`, formData);

            return res;
        }
        catch(error) {
            return error.response;
        }        
    }
}

export default ImagesDal;