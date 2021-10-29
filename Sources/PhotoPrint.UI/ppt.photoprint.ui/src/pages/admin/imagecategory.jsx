


import React from 'react';
import { Link, withRouter  } from 'react-router-dom'
import { TextField } from '@material-ui/core';
import { Button } from '@material-ui/core';
import Alert from '@material-ui/lab/Alert';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import FormControl from '@material-ui/core/FormControl';
import Checkbox from '@material-ui/core/Checkbox';

const constants = require('../../constants');
const { v4: uuidv4 } = require('uuid');
const PageHelper = require("../../helpers/PageHelper");
const ImageCategoriesDal = require('../../dal/ImageCategoriesDal');

const ImagesDal = require('../../dal/ImagesDal');

const CategoriesDal = require('../../dal/CategoriesDal');
const { ImageCategoryDto } = require('ppt.photoprint.dto')


class ImageCategoryPage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let paramOperation = this.props.match.params.operation;
        let paramId = this.props.match.params.id;
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            operation:  paramOperation,
            id:         paramId ? parseInt(paramId) : null,
            canEdit:    paramOperation ? ( paramOperation.toLowerCase() == 'new' || 
                                        paramOperation.toLowerCase() == 'edit' ? true : false) : false,
            imagecategory: this._createEmptyImageCategoryObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}imagecategories`,
            urlThis: `${rooPath}imagecategory/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onImageIDChanged = this.onImageIDChanged.bind(this);
        this.onCategoryIDChanged = this.onCategoryIDChanged.bind(this);
        this._getImageCategory = this._getImageCategory.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onImageIDChanged = this.onImageIDChanged.bind(this);
        this.onCategoryIDChanged = this.onCategoryIDChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getImages().then( () => {
			obj._getCategories().then( () => {
			obj._getImageCategory().then( () => {} );
			});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onImageIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.imagecategory.ImageID = newVal;

        this.setState(updatedState);
    }

    onCategoryIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.imagecategory.CategoryID = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving ImageCategory: ", this.state.imagecategory);
        
        if(this._validateForm()) {
            const reqImageCategory = new ImageCategoryDto();
            reqImageCategory.ID = this.state.id;
            reqImageCategory.ImageID = this.state.imagecategory.ImageID;
            reqImageCategory.CategoryID = this.state.imagecategory.CategoryID;

            console.log("Saving ImageCategory: ", reqImageCategory); 
        
            let dalImageCategories = new ImageCategoriesDal();

            let obj = this;

            function upsertImageCategoryThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `ImageCategory was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `ImageCategory was updated`;                
                    }

                    obj.setState(updatedState);
                }
                else {
                    obj._showError(updatedState, response); 
                
                    obj.setState(updatedState);
                }
            }  

            function upsertCatch(err) {
                const updatedState = obj.state;
                const errMsg = `Error: ${err}`
                updatedState.showSuccess = false;
                updatedState.showError = true;
                updatedState.error = errMsg; 
                obj.setState(updatedState);
            }

            if(this.state.id != null) {
                dalImageCategories.updateImageCategory(reqImageCategory)
                                        .then( (res) => { upsertImageCategoryThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalImageCategories.insertImageCategory(reqImageCategory)
                                        .then( (res) => { upsertImageCategoryThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });        
            }

        }
        
    }

    onDeleteClicked() {
        const updatedState = this.state;
        updatedState.showDeleteConfirm = true;
        this.setState(updatedState);
    }

    onDeleteCancel() {
        const updatedState = this.state;
        updatedState.showDeleteConfirm = false;
        this.setState(updatedState);
    }

    onDeleteConfirm() {  
        
        let dalImageCategories = new ImageCategoriesDal();
        let obj = this;

        dalImageCategories.deleteImageCategory(this.state.id).then( (response) => {
            if(response.status == constants.HTTP_OK) {
                obj.props.history.push(this.state.urlEntities);                
            }
            else {
                const updatedState = obj.state;
                updatedState.showDeleteConfirm = false;
                obj._showError(updatedState, response);                
                obj.setState(updatedState);               
            }
        })
    }

    render() {

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        const styleSuccess = {
            display: this.state.showSuccess ? "block" : "none"
        }   
        
        const styleDeleteBtn = {
            display: this.state.id ? "block" : "none"
        }

        const lstImageIDsFields = ["Name"];
        const lstImageIDs = this._prepareOptionsList( this.state.images 
                                                                    ? Object.values(this.state.images) : null, 
                                                                    lstImageIDsFields,
                                                                    false );
        const lstCategoryIDsFields = ["Name"];
        const lstCategoryIDs = this._prepareOptionsList( this.state.categories 
                                                                    ? Object.values(this.state.categories) : null, 
                                                                    lstCategoryIDsFields,
                                                                    false );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>ImageCategory: { this.state.imagecategory.toString() }</h2>
                            </td>
                            <td>
                                <Button variant="contained" color="primary"
                                        onClick={ () => this.onSaveClicked() }>Save</Button>

                                <Button variant="contained" color="secondary"
                                        style={styleDeleteBtn}
                                        onClick={ () => this.onDeleteClicked() }>Delete</Button>

                                <Button variant="contained" component={Link} to={this.state.urlEntities}>Cancel</Button>
                            </td>
                        </tr>
                        <tr>
                            <td colSpan={2}>
                                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                                <Alert severity="success" style={styleSuccess}>Success! {this.state.success}</Alert>                                
                            </td>
                        </tr> 
    
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbImageID" 
                                            fullWidth
                                            select 
                                            label="ImageID" 
                                            value={ (this.state.imagecategory && this.state.imagecategory.ImageID) ? 
                                                        this.state.imagecategory.ImageID : '-1' }
                                                        onChange={ (event) => this.onImageIDChanged(event) }>
                                        {
                                            lstImageIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbCategoryID" 
                                            fullWidth
                                            select 
                                            label="CategoryID" 
                                            value={ (this.state.imagecategory && this.state.imagecategory.CategoryID) ? 
                                                        this.state.imagecategory.CategoryID : '-1' }
                                                        onChange={ (event) => this.onCategoryIDChanged(event) }>
                                        {
                                            lstCategoryIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete ImageCategory</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this ImageCategory?
                    </DialogContentText>                    
                    </DialogContent>
                    <DialogActions>
                    <Button onClick={() => { this.onDeleteCancel() }} color="primary">
                        Cancel
                    </Button>
                    <Button onClick={() => { this.onDeleteConfirm() }} color="primary">
                        Delete
                    </Button>
                    </DialogActions>
                </Dialog>
            </div>

        );
    }

    _createEmptyImageCategoryObj() {
        let imagecategory = new ImageCategoryDto();

        return imagecategory;
    }

    async _getImageCategory()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalImageCategories = new ImageCategoriesDal();
            let response = await dalImageCategories.getImageCategory(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.imagecategory = response.data;                
            }
            else if(response.status == constants.HTTP_Unauthorized)
            {
                this._redirectToLogin();
            }
            else 
            {
                this._showError(updatedState, response);
            }
        
            this.setState(updatedState);    
        }
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

    

    _validateForm() {
        let updatedState = this.state;
        let isValid = true;
        
        // TODO: add validation here if needed

        if(isValid) {
            updatedState.showError = false;
        }
        
        this.setState(updatedState);
        
        return isValid;
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

    _prepareOptionsList(objs, fields, hasEmptyVal) 
    {
        var lst = [];
        
        if(hasEmptyVal) {
            lst.push( <option key='-1' value='-1'>[Empty]</option> );
        }

        if(objs) {
            
            lst.push(
                objs.map( (i) => {
                    let optionText = "";
                    for(let f in fields) {
                        optionText += i[fields[f]] + (f + 1 < fields.length ? " " : "");
                    }

                    return(
                        <option key={i.ID} value={i.ID}>
                            { optionText }
                        </option>
                    )
                })
            )
        }

        return lst;
    }
}

export default withRouter(ImageCategoryPage);