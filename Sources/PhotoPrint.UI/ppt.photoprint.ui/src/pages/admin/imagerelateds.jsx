



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../../constants";

const PageHelper = require("../../helpers/PageHelper");
const ImageRelatedsDal = require('../../dal/ImageRelatedsDal');

const ImagesDal = require('../../dal/ImagesDal');


class ImageRelatedsPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            imagerelateds: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}imagerelateds`,
            urlNewEntity: `${rooPath}imagerelated/new`,
            urlEditEntity: `${rooPath}imagerelated/edit/`,
        };
        this._initColumns();
       
        this._getImages = this._getImages.bind(this);
        this._getImageRelateds = this._getImageRelateds.bind(this);
        this._redirectToLogin = this._redirectToLogin.bind(this);

        this.onRowClick = this.onRowClick.bind(this);
    }

    onRowClick(event) {
        const row = event.row;
        if(row) {
            this.props.history.push(this.state.urlEditEntity + row.ImageID + "/" + row.RelatedImageID);
        }

    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getImages().then( () => {
			obj._getImageRelateds().then( () => {} );
			});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    render() {
        let records = this._getRecords();

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        return (
            <div style={{ height: 500, width: '100%' }}>
                <h3>ImageRelateds</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ ImageRelated</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'Image', headerName: 'ImageID', width: 250 },
                { field: 'RelatedImage', headerName: 'RelatedImageID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.imagerelateds);

        for(let c in cs) {

            let r = {
                id: c,
                ImageID: cs[c].ImageID,
                RelatedImageID: cs[c].RelatedImageID,
                Image: cs[c].ImageID ? this.state.images[ cs[c].ImageID ].Title : "",
                RelatedImage: cs[c].RelatedImageID ? this.state.images[ cs[c].RelatedImageID ].Title : "",

            };

            records.push(r);
        }

        return records;
    }

    async _getImages() {
        let updatedState = this.state;
        updatedState.images = {};
        let dalImages = new ImagesDal();
        let response = await dalImages.getImages();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.images[response.data[s].ID] = response.data[s];             
            }
        }
        else if(response.status == constants.HTTP_Unauthorized) {
            this._redirectToLogin();            
        }
        else {
            this._showError(updatedState, response);                        
        }

        this.setState(updatedState);
    }
    

    async _getImageRelateds() {
        let updatedState = this.state;
        updatedState.imagerelateds = {};
        let dalImageRelateds = new ImageRelatedsDal();
        let response = await dalImageRelateds.getImageRelateds();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.imagerelateds[[
                    response.data[s].ImageID,
                    response.data[s].RelatedImageID
                ]] = response.data[s];             
            }
        }
        else if(response.status == constants.HTTP_Unauthorized) {
            this._redirectToLogin();            
        }
        else {
            this._showError(updatedState, response);                        
        }

        this.setState(updatedState);
    }

    _showError(updatedState, response) {
        var error = JSON.parse(response.data.response);
        updatedState.showError = true;
        updatedState.error = error.Message;
    }

    _redirectToLogin()
    {        
        this._pageHelper.redirectToLogin(this.state.urlThis);  
    }
}

export default withRouter(ImageRelatedsPage);