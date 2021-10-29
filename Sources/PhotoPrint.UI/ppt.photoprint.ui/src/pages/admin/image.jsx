


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
const ImagesDal = require('../../dal/ImagesDal');

const CurrenciesDal = require('../../dal/CurrenciesDal');

const UsersDal = require('../../dal/UsersDal');
const { ImageDto } = require('ppt.photoprint.dto')


class ImagePage extends React.Component {

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
            image: this._createEmptyImageObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}images`,
            urlThis: `${rooPath}image/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onTitleChanged = this.onTitleChanged.bind(this);
        this.onDescriptionChanged = this.onDescriptionChanged.bind(this);
        this.onOriginUrlChanged = this.onOriginUrlChanged.bind(this);
        this.onMaxWidthChanged = this.onMaxWidthChanged.bind(this);
        this.onMaxHeightChanged = this.onMaxHeightChanged.bind(this);
        this.onPriceAmountChanged = this.onPriceAmountChanged.bind(this);
        this.onPriceCurrencyIDChanged = this.onPriceCurrencyIDChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this.onCreatedByIDChanged = this.onCreatedByIDChanged.bind(this);
        this.onCreatedDateChanged = this.onCreatedDateChanged.bind(this);
        this.onModifiedByIDChanged = this.onModifiedByIDChanged.bind(this);
        this.onModifiedDateChanged = this.onModifiedDateChanged.bind(this);
        this._getImage = this._getImage.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onTitleChanged = this.onTitleChanged.bind(this);
        this.onDescriptionChanged = this.onDescriptionChanged.bind(this);
        this.onOriginUrlChanged = this.onOriginUrlChanged.bind(this);
        this.onMaxWidthChanged = this.onMaxWidthChanged.bind(this);
        this.onMaxHeightChanged = this.onMaxHeightChanged.bind(this);
        this.onPriceAmountChanged = this.onPriceAmountChanged.bind(this);
        this.onPriceCurrencyIDChanged = this.onPriceCurrencyIDChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this.onCreatedByIDChanged = this.onCreatedByIDChanged.bind(this);
        this.onCreatedDateChanged = this.onCreatedDateChanged.bind(this);
        this.onModifiedByIDChanged = this.onModifiedByIDChanged.bind(this);
        this.onModifiedDateChanged = this.onModifiedDateChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getCurrencies().then( () => {
			obj._getUsers().then( () => {
			obj._getImage().then( () => {} );
			});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onTitleChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.image.Title = newVal;

        this.setState(updatedState);
    }

    onDescriptionChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.image.Description = newVal;

        this.setState(updatedState);
    }

    onOriginUrlChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.image.OriginUrl = newVal;

        this.setState(updatedState);
    }

    onMaxWidthChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.image.MaxWidth = newVal;

        this.setState(updatedState);
    }

    onMaxHeightChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.image.MaxHeight = newVal;

        this.setState(updatedState);
    }

    onPriceAmountChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseFloat(event.target.value);
        updatedState.image.PriceAmount = newVal;

        this.setState(updatedState);
    }

    onPriceCurrencyIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.image.PriceCurrencyID = newVal;

        this.setState(updatedState);
    }

    onIsDeletedChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.checked;
        updatedState.image.IsDeleted = newVal;

        this.setState(updatedState);
    }

    onCreatedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.image.CreatedByID = newVal;

        this.setState(updatedState);
    }

    onCreatedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.image.CreatedDate = newVal;

        this.setState(updatedState);
    }

    onModifiedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.image.ModifiedByID = newVal;

        this.setState(updatedState);
    }

    onModifiedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.image.ModifiedDate = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving Image: ", this.state.image);
        
        if(this._validateForm()) {
            const reqImage = new ImageDto();
            reqImage.ID = this.state.id;
            reqImage.Title = this.state.image.Title;
            reqImage.Description = this.state.image.Description;
            reqImage.OriginUrl = this.state.image.OriginUrl;
            reqImage.MaxWidth = this.state.image.MaxWidth;
            reqImage.MaxHeight = this.state.image.MaxHeight;
            reqImage.PriceAmount = this.state.image.PriceAmount;
            reqImage.PriceCurrencyID = this.state.image.PriceCurrencyID;
            reqImage.IsDeleted = this.state.image.IsDeleted;
            reqImage.CreatedByID = this.state.image.CreatedByID;
            reqImage.CreatedDate = this.state.image.CreatedDate;
            reqImage.ModifiedByID = this.state.image.ModifiedByID;
            reqImage.ModifiedDate = this.state.image.ModifiedDate;

            console.log("Saving Image: ", reqImage); 
        
            let dalImages = new ImagesDal();

            let obj = this;

            function upsertImageThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `Image was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `Image was updated`;                
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
                dalImages.updateImage(reqImage)
                                        .then( (res) => { upsertImageThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalImages.insertImage(reqImage)
                                        .then( (res) => { upsertImageThen(res); } )
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
        
        let dalImages = new ImagesDal();
        let obj = this;

        dalImages.deleteImage(this.state.id).then( (response) => {
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

        const lstPriceCurrencyIDsFields = ["Name"];
        const lstPriceCurrencyIDs = this._prepareOptionsList( this.state.currencies 
                                                                    ? Object.values(this.state.currencies) : null, 
                                                                    lstPriceCurrencyIDsFields,
                                                                    true );
        const lstCreatedByIDsFields = ["Name"];
        const lstCreatedByIDs = this._prepareOptionsList( this.state.users 
                                                                    ? Object.values(this.state.users) : null, 
                                                                    lstCreatedByIDsFields,
                                                                    false );
        const lstModifiedByIDsFields = ["Name"];
        const lstModifiedByIDs = this._prepareOptionsList( this.state.users 
                                                                    ? Object.values(this.state.users) : null, 
                                                                    lstModifiedByIDsFields,
                                                                    true );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>Image: { this.state.image.toString() }</h2>
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
                                <TextField  id="Title" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Title" 
                                            value={this.state.image.Title}
                                            onChange={ (event) => { this.onTitleChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Description" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Description" 
                                            value={this.state.image.Description}
                                            onChange={ (event) => { this.onDescriptionChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="OriginUrl" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="OriginUrl" 
                                            value={this.state.image.OriginUrl}
                                            onChange={ (event) => { this.onOriginUrlChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="MaxWidth" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="MaxWidth" 
                                            value={this.state.image.MaxWidth}
                                            onChange={ (event) => { this.onMaxWidthChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="MaxHeight" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="MaxHeight" 
                                            value={this.state.image.MaxHeight}
                                            onChange={ (event) => { this.onMaxHeightChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="PriceAmount" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="PriceAmount" 
                                            value={this.state.image.PriceAmount}
                                            onChange={ (event) => { this.onPriceAmountChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbPriceCurrencyID" 
                                            fullWidth
                                            select 
                                            label="PriceCurrencyID" 
                                            value={ (this.state.image && this.state.image.PriceCurrencyID) ? 
                                                        this.state.image.PriceCurrencyID : '-1' }
                                                        onChange={ (event) => this.onPriceCurrencyIDChanged(event) }>
                                        {
                                            lstPriceCurrencyIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <FormControlLabel
                                    key="lblIsDeleted"                        
                                    control = {
                                        <Checkbox   checked={ this.state.image.IsDeleted ? true : false } 
                                                    onChange={(event) => this.onIsDeletedChanged(event)} 
                                                    name="IsDeleted" />
                                        }
                                    label="IsDeleted"
                                />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbCreatedByID" 
                                            fullWidth
                                            select 
                                            label="CreatedByID" 
                                            value={ (this.state.image && this.state.image.CreatedByID) ? 
                                                        this.state.image.CreatedByID : '-1' }
                                                        onChange={ (event) => this.onCreatedByIDChanged(event) }>
                                        {
                                            lstCreatedByIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="CreatedDate" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="CreatedDate" 
                                            value={this.state.image.CreatedDate}
                                            onChange={ (event) => { this.onCreatedDateChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbModifiedByID" 
                                            fullWidth
                                            select 
                                            label="ModifiedByID" 
                                            value={ (this.state.image && this.state.image.ModifiedByID) ? 
                                                        this.state.image.ModifiedByID : '-1' }
                                                        onChange={ (event) => this.onModifiedByIDChanged(event) }>
                                        {
                                            lstModifiedByIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="ModifiedDate" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="ModifiedDate" 
                                            value={this.state.image.ModifiedDate}
                                            onChange={ (event) => { this.onModifiedDateChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete Image</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this Image?
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

    _createEmptyImageObj() {
        let image = new ImageDto();

        return image;
    }

    async _getImage()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalImages = new ImagesDal();
            let response = await dalImages.getImage(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.image = response.data;                
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

    async _getCurrencies() {
        let updatedState = this.state;
        updatedState.currencies = {};
        let dalCurrencies = new CurrenciesDal();
        let response = await dalCurrencies.getCurrencies();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.currencies[response.data[s].ID] = response.data[s];             
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

    async _getUsers() {
        let updatedState = this.state;
        updatedState.users = {};
        let dalUsers = new UsersDal();
        let response = await dalUsers.getUsers();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.users[response.data[s].ID] = response.data[s];             
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

export default withRouter(ImagePage);