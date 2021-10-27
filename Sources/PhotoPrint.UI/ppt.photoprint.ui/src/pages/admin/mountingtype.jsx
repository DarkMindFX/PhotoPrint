


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

const PageHelper = require("../helpers/PageHelper");
const MountingTypesDal = require('../dal/MountingTypesDal');
const { MountingTypeDto } = require('ppt.photoprint.dto')

const constants = require('../constants');
const { v4: uuidv4 } = require('uuid');

class MountingTypePage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let paramOperation = this.props.match.params.operation;
        let paramId = this.props.match.params.id;
        let rooPath = ''; // set the page hierarchy here

        this.state = { 
            operation:  paramOperation,
            id:         paramId ? parseInt(paramId) : null,
            canEdit:    paramOperation ? ( paramOperation.toLowerCase() == 'new' || 
                                        paramOperation.toLowerCase() == 'edit' ? true : false) : false,
            mountingtype: this._createEmptyMountingTypeObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}/mountingtypes`,
            urlThis: `${rooPath}/mountingtype/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onMountingTypeNameChanged = this.onMountingTypeNameChanged.bind(this);
        this.onDescriptionChanged = this.onDescriptionChanged.bind(this);
        this.onThumbnailUrlChanged = this.onThumbnailUrlChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this.onCreatedDateChanged = this.onCreatedDateChanged.bind(this);
        this.onCreatedByIDChanged = this.onCreatedByIDChanged.bind(this);
        this.onModifiedDateChanged = this.onModifiedDateChanged.bind(this);
        this.onModifiedByIDChanged = this.onModifiedByIDChanged.bind(this);
        this._getMountingType = this._getMountingType.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onMountingTypeNameChanged = this.onMountingTypeNameChanged.bind(this);
        this.onDescriptionChanged = this.onDescriptionChanged.bind(this);
        this.onThumbnailUrlChanged = this.onThumbnailUrlChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this.onCreatedDateChanged = this.onCreatedDateChanged.bind(this);
        this.onCreatedByIDChanged = this.onCreatedByIDChanged.bind(this);
        this.onModifiedDateChanged = this.onModifiedDateChanged.bind(this);
        this.onModifiedByIDChanged = this.onModifiedByIDChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getMountingType().then( () => {} );
			
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onMountingTypeNameChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.mountingtype.MountingTypeName = newVal;

        this.setState(updatedState);
    }

    onDescriptionChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.mountingtype.Description = newVal;

        this.setState(updatedState);
    }

    onThumbnailUrlChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.mountingtype.ThumbnailUrl = newVal;

        this.setState(updatedState);
    }

    onIsDeletedChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.mountingtype.IsDeleted = newVal;

        this.setState(updatedState);
    }

    onCreatedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.mountingtype.CreatedDate = newVal;

        this.setState(updatedState);
    }

    onCreatedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.mountingtype.CreatedByID = newVal;

        this.setState(updatedState);
    }

    onModifiedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.mountingtype.ModifiedDate = newVal;

        this.setState(updatedState);
    }

    onModifiedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.mountingtype.ModifiedByID = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving MountingType: ", this.state.mountingtype);
        
        if(this._validateForm()) {
            const reqMountingType = new MountingTypeDto();
            reqMountingType.ID = this.state.id;
            reqMountingType.MountingTypeName = this.state.mountingtype.MountingTypeName;
            reqMountingType.Description = this.state.mountingtype.Description;
            reqMountingType.ThumbnailUrl = this.state.mountingtype.ThumbnailUrl;
            reqMountingType.IsDeleted = this.state.mountingtype.IsDeleted;
            reqMountingType.CreatedDate = this.state.mountingtype.CreatedDate;
            reqMountingType.CreatedByID = this.state.mountingtype.CreatedByID;
            reqMountingType.ModifiedDate = this.state.mountingtype.ModifiedDate;
            reqMountingType.ModifiedByID = this.state.mountingtype.ModifiedByID;

            console.log("Saving MountingType: ", reqMountingType); 
        
            let dalMountingTypes = new MountingTypesDal();

            let obj = this;

            function upsertMountingTypeThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `MountingType was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `MountingType was updated`;                
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
                dalMountingTypes.updateMountingType(reqMountingType)
                                        .then( (res) => { upsertMountingTypeThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalMountingTypes.insertMountingType(reqMountingType)
                                        .then( (res) => { upsertMountingTypeThen(res); } )
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
        
        let dalMountingTypes = new MountingTypesDal();
        let obj = this;

        dalMountingTypes.deleteMountingType(this.state.id).then( (response) => {
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

        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>MountingType: { this.state.mountingtype.toString() }</h2>
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
                                <TextField  id="MountingTypeName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="MountingTypeName" 
                                            value={this.state.mountingtype.MountingTypeName}
                                            onChange={ (event) => { this.onMountingTypeNameChanged(event) } }
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
                                            value={this.state.mountingtype.Description}
                                            onChange={ (event) => { this.onDescriptionChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="ThumbnailUrl" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="ThumbnailUrl" 
                                            value={this.state.mountingtype.ThumbnailUrl}
                                            onChange={ (event) => { this.onThumbnailUrlChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="IsDeleted" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="IsDeleted" 
                                            value={this.state.mountingtype.IsDeleted}
                                            onChange={ (event) => { this.onIsDeletedChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="CreatedDate" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="CreatedDate" 
                                            value={this.state.mountingtype.CreatedDate}
                                            onChange={ (event) => { this.onCreatedDateChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="CreatedByID" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="CreatedByID" 
                                            value={this.state.mountingtype.CreatedByID}
                                            onChange={ (event) => { this.onCreatedByIDChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="ModifiedDate" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="ModifiedDate" 
                                            value={this.state.mountingtype.ModifiedDate}
                                            onChange={ (event) => { this.onModifiedDateChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="ModifiedByID" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="ModifiedByID" 
                                            value={this.state.mountingtype.ModifiedByID}
                                            onChange={ (event) => { this.onModifiedByIDChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete MountingType</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this MountingType?
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

    _createEmptyMountingTypeObj() {
        let mountingtype = new MountingTypeDto();

        return mountingtype;
    }

    async _getMountingType()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalMountingTypes = new MountingTypesDal();
            let response = await dalMountingTypes.getMountingType(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.mountingtype = response.data;                
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

export default withRouter(MountingTypePage);