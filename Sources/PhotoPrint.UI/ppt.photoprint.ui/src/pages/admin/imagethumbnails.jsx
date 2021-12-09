



const React = require("react";
const { Link, withRouter } = require('react-router-dom'
const { DataGrid } = require('@material-ui/data-grid';
const Alert = require('@material-ui/lab/Alert';
const { Button } = require('@material-ui/core';
const constants = require("../../constants";

const PageHelper = require("../../helpers/PageHelper");
const ImageThumbnailsDal = require('../../dal/ImageThumbnailsDal');

const ImagesDal = require('../../dal/ImagesDal');


class ImageThumbnailsPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            imagethumbnails: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}imagethumbnails`,
            urlNewEntity: `${rooPath}imagethumbnail/new`,
            urlEditEntity: `${rooPath}imagethumbnail/edit/`,
        };
        this._initColumns();
       
        this._getImages = this._getImages.bind(this);
        this._getImageThumbnails = this._getImageThumbnails.bind(this);
        this._redirectToLogin = this._redirectToLogin.bind(this);

        this.onRowClick = this.onRowClick.bind(this);
    }

    onRowClick(event) {
        const row = event.row;
        if(row) {
            this.props.history.push(this.state.urlEditEntity + row.id);
        }

    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getImages().then( () => {
			obj._getImageThumbnails().then( () => {} );
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
                <h3>ImageThumbnails</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ ImageThumbnail</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'Url', headerName: 'Url', width: 250 },
                { field: 'Order', headerName: 'Order', width: 250 },
                { field: 'ImageID', headerName: 'ImageID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.imagethumbnails);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                Url: cs[c].Url,
                Order: cs[c].Order,
                ImageID: cs[c].ImageID ? this.state.images[ cs[c].ImageID ].Title : "",

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
    

    async _getImageThumbnails() {
        let updatedState = this.state;
        updatedState.imagethumbnails = {};
        let dalImageThumbnails = new ImageThumbnailsDal();
        let response = await dalImageThumbnails.getImageThumbnails();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.imagethumbnails[response.data[s].ID] = response.data[s];             
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

export default withRouter(ImageThumbnailsPage);