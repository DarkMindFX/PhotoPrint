


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
const ContactsDal = require('../../dal/ContactsDal');

const ContactTypesDal = require('../../dal/ContactTypesDal');

const UsersDal = require('../../dal/UsersDal');
const { ContactDto } = require('ppt.photoprint.dto')


class ContactPage extends React.Component {

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
            contact: this._createEmptyContactObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}contacts`,
            urlThis: `${rooPath}contact/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onContactTypeIDChanged = this.onContactTypeIDChanged.bind(this);
        this.onTitleChanged = this.onTitleChanged.bind(this);
        this.onCommentChanged = this.onCommentChanged.bind(this);
        this.onValueChanged = this.onValueChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this.onCreatedByIDChanged = this.onCreatedByIDChanged.bind(this);
        this.onCreatedDateChanged = this.onCreatedDateChanged.bind(this);
        this.onModifiedByIDChanged = this.onModifiedByIDChanged.bind(this);
        this.onModifiedDateChanged = this.onModifiedDateChanged.bind(this);
        this._getContact = this._getContact.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onContactTypeIDChanged = this.onContactTypeIDChanged.bind(this);
        this.onTitleChanged = this.onTitleChanged.bind(this);
        this.onCommentChanged = this.onCommentChanged.bind(this);
        this.onValueChanged = this.onValueChanged.bind(this);
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
            			obj._getContactTypes().then( () => {
			obj._getUsers().then( () => {
			obj._getContact().then( () => {} );
			});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onContactTypeIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.contact.ContactTypeID = newVal;

        this.setState(updatedState);
    }

    onTitleChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.contact.Title = newVal;

        this.setState(updatedState);
    }

    onCommentChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.contact.Comment = newVal;

        this.setState(updatedState);
    }

    onValueChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.contact.Value = newVal;

        this.setState(updatedState);
    }

    onIsDeletedChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.contact.IsDeleted = newVal;

        this.setState(updatedState);
    }

    onCreatedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.contact.CreatedByID = newVal;

        this.setState(updatedState);
    }

    onCreatedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.contact.CreatedDate = newVal;

        this.setState(updatedState);
    }

    onModifiedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.contact.ModifiedByID = newVal;

        this.setState(updatedState);
    }

    onModifiedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.contact.ModifiedDate = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving Contact: ", this.state.contact);
        
        if(this._validateForm()) {
            const reqContact = new ContactDto();
            reqContact.ID = this.state.id;
            reqContact.ContactTypeID = this.state.contact.ContactTypeID;
            reqContact.Title = this.state.contact.Title;
            reqContact.Comment = this.state.contact.Comment;
            reqContact.Value = this.state.contact.Value;
            reqContact.IsDeleted = this.state.contact.IsDeleted;
            reqContact.CreatedByID = this.state.contact.CreatedByID;
            reqContact.CreatedDate = this.state.contact.CreatedDate;
            reqContact.ModifiedByID = this.state.contact.ModifiedByID;
            reqContact.ModifiedDate = this.state.contact.ModifiedDate;

            console.log("Saving Contact: ", reqContact); 
        
            let dalContacts = new ContactsDal();

            let obj = this;

            function upsertContactThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `Contact was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `Contact was updated`;                
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
                dalContacts.updateContact(reqContact)
                                        .then( (res) => { upsertContactThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalContacts.insertContact(reqContact)
                                        .then( (res) => { upsertContactThen(res); } )
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
        
        let dalContacts = new ContactsDal();
        let obj = this;

        dalContacts.deleteContact(this.state.id).then( (response) => {
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

        const lstContactTypeIDsFields = ["Name"];
        const lstContactTypeIDs = this._prepareOptionsList( this.state.contacttypes 
                                                                    ? Object.values(this.state.contacttypes) : null, 
                                                                    lstContactTypeIDsFields,
                                                                    false );
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
                                <h2>Contact: { this.state.contact.toString() }</h2>
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
                                <TextField  key="cbContactTypeID" 
                                            fullWidth
                                            select 
                                            label="ContactTypeID" 
                                            value={ (this.state.contact && this.state.contact.ContactTypeID) ? 
                                                        this.state.contact.ContactTypeID : '-1' }
                                                        onChange={ (event) => this.onContactTypeIDChanged(event) }>
                                        {
                                            lstContactTypeIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Title" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Title" 
                                            value={this.state.contact.Title}
                                            onChange={ (event) => { this.onTitleChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Comment" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Comment" 
                                            value={this.state.contact.Comment}
                                            onChange={ (event) => { this.onCommentChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Value" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Value" 
                                            value={this.state.contact.Value}
                                            onChange={ (event) => { this.onValueChanged(event) } }
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
                                            value={this.state.contact.IsDeleted}
                                            onChange={ (event) => { this.onIsDeletedChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbCreatedByID" 
                                            fullWidth
                                            select 
                                            label="CreatedByID" 
                                            value={ (this.state.contact && this.state.contact.CreatedByID) ? 
                                                        this.state.contact.CreatedByID : '-1' }
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
                                            value={this.state.contact.CreatedDate}
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
                                            value={ (this.state.contact && this.state.contact.ModifiedByID) ? 
                                                        this.state.contact.ModifiedByID : '-1' }
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
                                            value={this.state.contact.ModifiedDate}
                                            onChange={ (event) => { this.onModifiedDateChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete Contact</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this Contact?
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

    _createEmptyContactObj() {
        let contact = new ContactDto();

        return contact;
    }

    async _getContact()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalContacts = new ContactsDal();
            let response = await dalContacts.getContact(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.contact = response.data;                
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

    async _getContactTypes() {
        let updatedState = this.state;
        updatedState.contacttypes = {};
        let dalContactTypes = new ContactTypesDal();
        let response = await dalContactTypes.getContactTypes();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.contacttypes[response.data[s].ID] = response.data[s];             
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

export default withRouter(ContactPage);