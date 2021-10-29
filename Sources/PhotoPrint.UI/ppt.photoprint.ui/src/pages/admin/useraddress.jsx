


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
const UserAddressesDal = require('../../dal/UserAddressesDal');

const UsersDal = require('../../dal/UsersDal');

const AddressesDal = require('../../dal/AddressesDal');
const { UserAddressDto } = require('ppt.photoprint.dto')


class UserAddressPage extends React.Component {

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
            useraddress: this._createEmptyUserAddressObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}useraddresses`,
            urlThis: `${rooPath}useraddress/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onUserIDChanged = this.onUserIDChanged.bind(this);
        this.onAddressIDChanged = this.onAddressIDChanged.bind(this);
        this.onIsPrimaryChanged = this.onIsPrimaryChanged.bind(this);
        this._getUserAddress = this._getUserAddress.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onUserIDChanged = this.onUserIDChanged.bind(this);
        this.onAddressIDChanged = this.onAddressIDChanged.bind(this);
        this.onIsPrimaryChanged = this.onIsPrimaryChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getUsers().then( () => {
			obj._getAddresses().then( () => {
			obj._getUserAddress().then( () => {} );
			});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onUserIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.useraddress.UserID = newVal;

        this.setState(updatedState);
    }

    onAddressIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.useraddress.AddressID = newVal;

        this.setState(updatedState);
    }

    onIsPrimaryChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.checked;
        updatedState.useraddress.IsPrimary = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving UserAddress: ", this.state.useraddress);
        
        if(this._validateForm()) {
            const reqUserAddress = new UserAddressDto();
            reqUserAddress.ID = this.state.id;
            reqUserAddress.UserID = this.state.useraddress.UserID;
            reqUserAddress.AddressID = this.state.useraddress.AddressID;
            reqUserAddress.IsPrimary = this.state.useraddress.IsPrimary;

            console.log("Saving UserAddress: ", reqUserAddress); 
        
            let dalUserAddresses = new UserAddressesDal();

            let obj = this;

            function upsertUserAddressThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `UserAddress was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `UserAddress was updated`;                
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
                dalUserAddresses.updateUserAddress(reqUserAddress)
                                        .then( (res) => { upsertUserAddressThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalUserAddresses.insertUserAddress(reqUserAddress)
                                        .then( (res) => { upsertUserAddressThen(res); } )
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
        
        let dalUserAddresses = new UserAddressesDal();
        let obj = this;

        dalUserAddresses.deleteUserAddress(this.state.id).then( (response) => {
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

        const lstUserIDsFields = ["Name"];
        const lstUserIDs = this._prepareOptionsList( this.state.users 
                                                                    ? Object.values(this.state.users) : null, 
                                                                    lstUserIDsFields,
                                                                    false );
        const lstAddressIDsFields = ["Name"];
        const lstAddressIDs = this._prepareOptionsList( this.state.addresses 
                                                                    ? Object.values(this.state.addresses) : null, 
                                                                    lstAddressIDsFields,
                                                                    false );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>UserAddress: { this.state.useraddress.toString() }</h2>
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
                                <TextField  key="cbUserID" 
                                            fullWidth
                                            select 
                                            label="UserID" 
                                            value={ (this.state.useraddress && this.state.useraddress.UserID) ? 
                                                        this.state.useraddress.UserID : '-1' }
                                                        onChange={ (event) => this.onUserIDChanged(event) }>
                                        {
                                            lstUserIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbAddressID" 
                                            fullWidth
                                            select 
                                            label="AddressID" 
                                            value={ (this.state.useraddress && this.state.useraddress.AddressID) ? 
                                                        this.state.useraddress.AddressID : '-1' }
                                                        onChange={ (event) => this.onAddressIDChanged(event) }>
                                        {
                                            lstAddressIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <FormControlLabel
                                    key="lblIsPrimary"                        
                                    control = {
                                        <Checkbox   checked={ this.state.useraddress.IsPrimary ? true : false } 
                                                    onChange={(event) => this.onIsPrimaryChanged(event)} 
                                                    name="IsPrimary" />
                                        }
                                    label="IsPrimary"
                                />
                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete UserAddress</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this UserAddress?
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

    _createEmptyUserAddressObj() {
        let useraddress = new UserAddressDto();

        return useraddress;
    }

    async _getUserAddress()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalUserAddresses = new UserAddressesDal();
            let response = await dalUserAddresses.getUserAddress(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.useraddress = response.data;                
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

    async _getAddresses() {
        let updatedState = this.state;
        updatedState.addresses = {};
        let dalAddresses = new AddressesDal();
        let response = await dalAddresses.getAddresses();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.addresses[response.data[s].ID] = response.data[s];             
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

export default withRouter(UserAddressPage);