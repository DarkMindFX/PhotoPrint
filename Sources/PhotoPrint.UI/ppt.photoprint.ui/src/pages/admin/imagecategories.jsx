



const React = require("react";
const { Link, withRouter } = require('react-router-dom'
const { DataGrid } = require('@material-ui/data-grid';
const Alert = require('@material-ui/lab/Alert';
const { Button } = require('@material-ui/core';
const constants = require("../../constants";

const PageHelper = require("../../helpers/PageHelper");
const ImageCategoriesDal = require('../../dal/ImageCategoriesDal');

const ImagesDal = require('../../dal/ImagesDal');

const CategoriesDal = require('../../dal/CategoriesDal');


class ImageCategoriesPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            imagecategories: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}imagecategories`,
            urlNewEntity: `${rooPath}imagecategory/new`,
            urlEditEntity: `${rooPath}imagecategory/edit/`,
        };
        this._initColumns();
       
        this._getImages = this._getImages.bind(this);
        this._getCategories = this._getCategories.bind(this);
        this._getImageCategories = this._getImageCategories.bind(this);
        this._redirectToLogin = this._redirectToLogin.bind(this);

        this.onRowClick = this.onRowClick.bind(this);
    }

    onRowClick(event) {
        const row = event.row;
        if(row) {
            this.props.history.push(this.state.urlEditEntity + row.ImageID + "/" + row.CategoryID);
        }

    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getImages().then( () => {
			obj._getCategories().then( () => {
			obj._getImageCategories().then( () => {} );
			});});
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
                <h3>ImageCategories</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ ImageCategory</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'Image', headerName: 'ImageID', width: 250 },
                { field: 'Category', headerName: 'CategoryID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.imagecategories);

        for(let c in cs) {

            let r = {
                id: c,
                ImageID: cs[c].ImageID,
                CategoryID: cs[c].CategoryID,
                Image: cs[c].ImageID ? this.state.images[ cs[c].ImageID ].Title : "",
                Category: cs[c].CategoryID ? this.state.categories[ cs[c].CategoryID ].CategoryName : "",

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
    async _getCategories() {
        let updatedState = this.state;
        updatedState.categories = {};
        let dalCategories = new CategoriesDal();
        let response = await dalCategories.getCategories();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.categories[response.data[s].ID] = response.data[s];             
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
    

    async _getImageCategories() {
        let updatedState = this.state;
        updatedState.imagecategories = {};
        let dalImageCategories = new ImageCategoriesDal();
        let response = await dalImageCategories.getImageCategories();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.imagecategories[[
                    response.data[s].ImageID, 
                    response.data[s].CategoryID
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

export default withRouter(ImageCategoriesPage);