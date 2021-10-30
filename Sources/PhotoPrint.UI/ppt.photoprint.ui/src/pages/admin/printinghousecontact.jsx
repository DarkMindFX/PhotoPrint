


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
const PrintingHouseContactsDal = require('../../dal/PrintingHouseContactsDal');

const PrintingHousesDal = require('../../dal/PrintingHousesDal');

const ContactsDal = require('../../dal/ContactsDal');
const { PrintingHouseContactDto } = require('ppt.photoprint.dto')


class PrintingHouseContactPage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let paramOperation = this.props.match.params.operation;
        let paramPrintingHouseId = this.props.match.params.printinghouseid;
        let paramContactId = this.props.match.params.contactid;
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            operation:  paramOperation,
            printinghouseid:         paramPrintingHouseId ? parseInt(paramPrintingHouseId) : null,
            contactid: paramContactId  ? parseInt(paramContactId) : null,
            canEdit:    paramOperation ? ( paramOperation.toLowerCase() == 'new' || 
                                        paramOperation.toLowerCase() == 'edit' ? true : false) : false,
            printinghousecontact: this._createEmptyPrintingHouseContactObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}printinghousecontacts`,
            urlThis: `${rooPath}printinghousecontact/${paramOperation}` + (paramPrintingHouseId ? `/${paramPrintingHouseId}` : ``)
        };

        this.onPrintingHouseIDChanged = this.onPrintingHouseIDChanged.bind(this);
        this.onContactIDChanged = this.onContactIDChanged.bind(this);
        this.onIsPrimaryChanged = this.onIsPrimaryChanged.bind(this);
        this._getPrintingHouseContact = this._getPrintingHouseContact.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onPrintingHouseIDChanged = this.onPrintingHouseIDChanged.bind(this);
        this.onContactIDChanged = this.onContactIDChanged.bind(this);
        this.onIsPrimaryChanged = this.onIsPrimaryChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getPrintingHouses().then( () => {
			obj._getContacts().then( () => {
			obj._getPrintingHouseContact().then( () => {} );
			});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onPrintingHouseIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.printinghousecontact.PrintingHouseID = newVal;

        this.setState(updatedState);
    }

    onContactIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.printinghousecontact.ContactID = newVal;

        this.setState(updatedState);
    }

    onIsPrimaryChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.checked;
        updatedState.printinghousecontact.IsPrimary = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving PrintingHouseContact: ", this.state.printinghousecontact);
        
        if(this._validateForm()) {
            const reqPrintingHouseContact = new PrintingHouseContactDto();
            reqPrintingHouseContact.PrintingHouseID = this.state.printinghousecontact.PrintingHouseID;
            reqPrintingHouseContact.ContactID = this.state.printinghousecontact.ContactID;
            reqPrintingHouseContact.IsPrimary = this.state.printinghousecontact.IsPrimary;

            console.log("Saving PrintingHouseContact: ", reqPrintingHouseContact); 
        
            let dalPrintingHouseContacts = new PrintingHouseContactsDal();

            let obj = this;

            function upsertPrintingHouseContactThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.printinghouseid = response.data.PrintingHouseID;
                        updatedState.contactid = response.data.ContactID;
                        updatedState.success = `PrintingHouseContact was created.`;
                    }
                    else {
                        updatedState.success = `PrintingHouseContact was updated`;                
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

            if(this.state.printinghouseid != null && this.state.contactid != null) {
                dalPrintingHouseContacts.updatePrintingHouseContact(reqPrintingHouseContact)
                                        .then( (res) => { upsertPrintingHouseContactThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalPrintingHouseContacts.insertPrintingHouseContact(reqPrintingHouseContact)
                                        .then( (res) => { upsertPrintingHouseContactThen(res); } )
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
        
        let dalPrintingHouseContacts = new PrintingHouseContactsDal();
        let obj = this;

        dalPrintingHouseContacts.deletePrintingHouseContact(this.state.printinghouseid, this.state.contactid).then( (response) => {
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
            display: this.state.printinghouseid && this.state.contactid ? "block" : "none"
        }

        const lstPrintingHouseIDsFields = ["Name"];
        const lstPrintingHouseIDs = this._prepareOptionsList( this.state.printinghouses 
                                                                    ? Object.values(this.state.printinghouses) : null, 
                                                                    lstPrintingHouseIDsFields,
                                                                    false );
        const lstContactIDsFields = ["Title"];
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
                                <h2>PrintingHouseContact: { this.state.printinghousecontact.PrintingHouseID ?  this.state.printinghouses[ this.state.printinghousecontact.PrintingHouseID ].Name : "" }</h2>
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
                                <TextField  key="cbPrintingHouseID" 
                                            fullWidth
                                            select 
                                            label="PrintingHouseID" 
                                            value={ (this.state.printinghousecontact && this.state.printinghousecontact.PrintingHouseID) ? 
                                                        this.state.printinghousecontact.PrintingHouseID : '-1' }
                                                        onChange={ (event) => this.onPrintingHouseIDChanged(event) }>
                                        {
                                            lstPrintingHouseIDs 
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
                                            value={ (this.state.printinghousecontact && this.state.printinghousecontact.ContactID) ? 
                                                        this.state.printinghousecontact.ContactID : '-1' }
                                                        onChange={ (event) => this.onContactIDChanged(event) }>
                                        {
                                            lstContactIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <FormControlLabel
                                    key="lblIsPrimary"                        
                                    control = {
                                        <Checkbox   checked={ this.state.printinghousecontact.IsPrimary ? true : false } 
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
                    <DialogTitle id="form-dialog-title">Delete PrintingHouseContact</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this PrintingHouseContact?
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

    _createEmptyPrintingHouseContactObj() {
        let printinghousecontact = new PrintingHouseContactDto();

        return printinghousecontact;
    }

    async _getPrintingHouseContact()
    {
        if(this.state.printinghouseid && this.state.contactid) {
            let updatedState = this.state;
                  
            let dalPrintingHouseContacts = new PrintingHouseContactsDal();
            let response = await dalPrintingHouseContacts.getPrintingHouseContact(this.state.printinghouseid, this.state.contactid);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.printinghousecontact = response.data;                
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

    async _getPrintingHouses() {
        let updatedState = this.state;
        updatedState.printinghouses = {};
        let dalPrintingHouses = new PrintingHousesDal();
        let response = await dalPrintingHouses.getPrintingHouses();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.printinghouses[response.data[s].ID] = response.data[s];             
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

export default withRouter(PrintingHouseContactPage);