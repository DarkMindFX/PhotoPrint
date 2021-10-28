


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

const constants = require('../../constants');
const { v4: uuidv4 } = require('uuid');
const PageHelper = require("../../helpers/PageHelper");
const UserContactsDal = require('../../dal/UserContactsDal');

const UsersDal = require('../../dal/UsersDal');

const ContactsDal = require('../../dal/ContactsDal');
const { UserContactDto } = require('ppt.photoprint.dto')


class UserContactPage extends React.Component {

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
            usercontact: this._createEmptyUserContactObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}usercontacts`,
            urlThis: `${rooPath}usercontact/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onUserIDChanged = this.onUserIDChanged.bind(this);
        this.onContactIDChanged = this.onContactIDChanged.bind(this);
        this.onIsPrimaryChanged = this.onIsPrimaryChanged.bind(this);
        this._getUserContact = this._getUserContact.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onUserIDChanged = this.onUserIDChanged.bind(this);
        this.onContactIDChanged = this.onContactIDChanged.bind(this);
        this.onIsPrimaryChanged = this.onIsPrimaryChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getUsers().then( () => {
			obj._getContacts().then( () => {
			obj._getUserContact().then( () => {} );
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
        updatedState.usercontact.UserID = newVal;

        this.setState(updatedState);
    }

    onContactIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.usercontact.ContactID = newVal;

        this.setState(updatedState);
    }

    onIsPrimaryChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.usercontact.IsPrimary = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving UserContact: ", this.state.usercontact);
        
        if(this._validateForm()) {
            const reqUserContact = new UserContactDto();
            reqUserContact.ID = this.state.id;
            reqUserContact.UserID = this.state.usercontact.UserID;
            reqUserContact.ContactID = this.state.usercontact.ContactID;
            reqUserContact.IsPrimary = this.state.usercontact.IsPrimary;

            console.log("Saving UserContact: ", reqUserContact); 
        
            let dalUserContacts = new UserContactsDal();

            let obj = this;

            function upsertUserContactThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `UserContact was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `UserContact was updated`;                
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
                dalUserContacts.updateUserContact(reqUserContact)
                                        .then( (res) => { upsertUserContactThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalUserContacts.insertUserContact(reqUserContact)
                                        .then( (res) => { upsertUserContactThen(res); } )
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
        
        let dalUserContacts = new UserContactsDal();
        let obj = this;

        dalUserContacts.deleteUserContact(this.state.id).then( (response) => {
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
        const lstContactIDsFields = ["Name"];
        const lstContactIDs = this._prepareOptionsList( this.state.contacts 
                                                                    ? Object.values(this.state.contacts) : null, 
                                                                    lstContactIDsFields,
                                                                    false );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>UserContact: { this.state.usercontact.toString() }</h2>
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
                                            value={ (this.state.usercontact && this.state.usercontact.UserID) ? 
                                                        this.state.usercontact.UserID : '-1' }
                                                        onChange={ (event) => this.onUserIDChanged(event) }>
                                        {
                                            lstUserIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbContactID" 
                                            fullWidth
                                            select 
                                            label="ContactID" 
                                            value={ (this.state.usercontact && this.state.usercontact.ContactID) ? 
                                                        this.state.usercontact.ContactID : '-1' }
                                                        onChange={ (event) => this.onContactIDChanged(event) }>
                                        {
                                            lstContactIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="IsPrimary" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="IsPrimary" 
                                            value={this.state.usercontact.IsPrimary}
                                            onChange={ (event) => { this.onIsPrimaryChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete UserContact</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this UserContact?
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

    _createEmptyUserContactObj() {
        let usercontact = new UserContactDto();

        return usercontact;
    }

    async _getUserContact()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalUserContacts = new UserContactsDal();
            let response = await dalUserContacts.getUserContact(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.usercontact = response.data;                
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

    async _getContacts() {
        let updatedState = this.state;
        updatedState.contacts = {};
        let dalContacts = new ContactsDal();
        let response = await dalContacts.getContacts();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.contacts[response.data[s].ID] = response.data[s];             
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

export default withRouter(UserContactPage);