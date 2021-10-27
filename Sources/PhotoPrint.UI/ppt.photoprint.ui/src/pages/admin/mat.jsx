


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
const MatsDal = require('../dal/MatsDal');

const UsersDal = require('../dal/UsersDal');
const { MatDto } = require('ppt.photoprint.dto')

const constants = require('../constants');
const { v4: uuidv4 } = require('uuid');

class MatPage extends React.Component {

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
            mat: this._createEmptyMatObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}/mats`,
            urlThis: `${rooPath}/mat/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onMatNameChanged = this.onMatNameChanged.bind(this);
        this.onDescriptionChanged = this.onDescriptionChanged.bind(this);
        this.onThumbnailUrlChanged = this.onThumbnailUrlChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this.onCreatedDateChanged = this.onCreatedDateChanged.bind(this);
        this.onCreatedByIDChanged = this.onCreatedByIDChanged.bind(this);
        this.onModifiedDateChanged = this.onModifiedDateChanged.bind(this);
        this.onModifiedByIDChanged = this.onModifiedByIDChanged.bind(this);
        this._getMat = this._getMat.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onMatNameChanged = this.onMatNameChanged.bind(this);
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
            			obj._getUsers().then( () => {
			obj._getMat().then( () => {} );
			});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onMatNameChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.mat.MatName = newVal;

        this.setState(updatedState);
    }

    onDescriptionChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.mat.Description = newVal;

        this.setState(updatedState);
    }

    onThumbnailUrlChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.mat.ThumbnailUrl = newVal;

        this.setState(updatedState);
    }

    onIsDeletedChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.mat.IsDeleted = newVal;

        this.setState(updatedState);
    }

    onCreatedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.mat.CreatedDate = newVal;

        this.setState(updatedState);
    }

    onCreatedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.mat.CreatedByID = newVal;

        this.setState(updatedState);
    }

    onModifiedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.mat.ModifiedDate = newVal;

        this.setState(updatedState);
    }

    onModifiedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.mat.ModifiedByID = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving Mat: ", this.state.mat);
        
        if(this._validateForm()) {
            const reqMat = new MatDto();
            reqMat.ID = this.state.id;
            reqMat.MatName = this.state.mat.MatName;
            reqMat.Description = this.state.mat.Description;
            reqMat.ThumbnailUrl = this.state.mat.ThumbnailUrl;
            reqMat.IsDeleted = this.state.mat.IsDeleted;
            reqMat.CreatedDate = this.state.mat.CreatedDate;
            reqMat.CreatedByID = this.state.mat.CreatedByID;
            reqMat.ModifiedDate = this.state.mat.ModifiedDate;
            reqMat.ModifiedByID = this.state.mat.ModifiedByID;

            console.log("Saving Mat: ", reqMat); 
        
            let dalMats = new MatsDal();

            let obj = this;

            function upsertMatThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `Mat was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `Mat was updated`;                
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
                dalMats.updateMat(reqMat)
                                        .then( (res) => { upsertMatThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalMats.insertMat(reqMat)
                                        .then( (res) => { upsertMatThen(res); } )
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
        
        let dalMats = new MatsDal();
        let obj = this;

        dalMats.deleteMat(this.state.id).then( (response) => {
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
                                <h2>Mat: { this.state.mat.toString() }</h2>
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
                                <TextField  id="MatName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="MatName" 
                                            value={this.state.mat.MatName}
                                            onChange={ (event) => { this.onMatNameChanged(event) } }
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
                                            value={this.state.mat.Description}
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
                                            value={this.state.mat.ThumbnailUrl}
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
                                            value={this.state.mat.IsDeleted}
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
                                            value={this.state.mat.CreatedDate}
                                            onChange={ (event) => { this.onCreatedDateChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbCreatedByID" 
                                            fullWidth
                                            select 
                                            label="CreatedByID" 
                                            value={ (this.state.mat && this.state.mat.CreatedByID) ? 
                                                        this.state.mat.CreatedByID : '-1' }
                                                        onChange={ (event) => this.onCreatedByIDChanged(event) }>
                                        {
                                            lstCreatedByIDs 
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
                                            value={this.state.mat.ModifiedDate}
                                            onChange={ (event) => { this.onModifiedDateChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbModifiedByID" 
                                            fullWidth
                                            select 
                                            label="ModifiedByID" 
                                            value={ (this.state.mat && this.state.mat.ModifiedByID) ? 
                                                        this.state.mat.ModifiedByID : '-1' }
                                                        onChange={ (event) => this.onModifiedByIDChanged(event) }>
                                        {
                                            lstModifiedByIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete Mat</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this Mat?
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

    _createEmptyMatObj() {
        let mat = new MatDto();

        return mat;
    }

    async _getMat()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalMats = new MatsDal();
            let response = await dalMats.getMat(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.mat = response.data;                
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

export default withRouter(MatPage);