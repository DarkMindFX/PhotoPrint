


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
const PrintingHouseAddressesDal = require('../../dal/PrintingHouseAddressesDal');

const PrintingHousesDal = require('../../dal/PrintingHousesDal');

const AddressesDal = require('../../dal/AddressesDal');
const { PrintingHouseAddressDto } = require('ppt.photoprint.dto')


class PrintingHouseAddressPage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let paramOperation = this.props.match.params.operation;
        let paramPrintingHouseId = this.props.match.params.printinghouseid;
        let paramAddressId = this.props.match.params.addressid;
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            operation:  paramOperation,
            printinghouseid:         paramPrintingHouseId ? parseInt(paramPrintingHouseId) : null,
            addressid:         paramPrintingHouseId ? parseInt(paramAddressId) : null,
            canEdit:    paramOperation ? ( paramOperation.toLowerCase() == 'new' || 
                                        paramOperation.toLowerCase() == 'edit' ? true : false) : false,
            printinghouseaddress: this._createEmptyPrintingHouseAddressObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}printinghouseaddresses`,
            urlThis: `${rooPath}printinghouseaddress/${paramOperation}` + (paramPrintingHouseId ? `/${paramPrintingHouseId}/${paramAddressId}` : ``)
        };

        this.onPrintingHouseIDChanged = this.onPrintingHouseIDChanged.bind(this);
        this.onAddressIDChanged = this.onAddressIDChanged.bind(this);
        this.onIsPrimaryChanged = this.onIsPrimaryChanged.bind(this);
        this._getPrintingHouseAddress = this._getPrintingHouseAddress.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onPrintingHouseIDChanged = this.onPrintingHouseIDChanged.bind(this);
        this.onAddressIDChanged = this.onAddressIDChanged.bind(this);
        this.onIsPrimaryChanged = this.onIsPrimaryChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getPrintingHouses().then( () => {
			obj._getAddresses().then( () => {
			obj._getPrintingHouseAddress().then( () => {} );
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
        updatedState.printinghouseaddress.PrintingHouseID = newVal;

        this.setState(updatedState);
    }

    onAddressIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.printinghouseaddress.AddressID = newVal;

        this.setState(updatedState);
    }

    onIsPrimaryChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.checked;
        updatedState.printinghouseaddress.IsPrimary = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving PrintingHouseAddress: ", this.state.printinghouseaddress);
        
        if(this._validateForm()) {
            const reqPrintingHouseAddress = new PrintingHouseAddressDto();
            reqPrintingHouseAddress.PrintingHouseID = this.state.printinghouseaddress.PrintingHouseID;
            reqPrintingHouseAddress.AddressID = this.state.printinghouseaddress.AddressID;
            reqPrintingHouseAddress.IsPrimary = this.state.printinghouseaddress.IsPrimary;

            console.log("Saving PrintingHouseAddress: ", reqPrintingHouseAddress); 
        
            let dalPrintingHouseAddresses = new PrintingHouseAddressesDal();

            let obj = this;

            function upsertPrintingHouseAddressThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.printinghouseid = response.data.PrintingHouseID;
                        updatedState.addressid = response.data.AddressID;
                        updatedState.success = `PrintingHouseAddress was created.`;
                    }
                    else {
                        updatedState.success = `PrintingHouseAddress was updated`;                
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

            if(this.state.printinghouseid, this.state.addressid != null) {
                dalPrintingHouseAddresses.updatePrintingHouseAddress(reqPrintingHouseAddress)
                                        .then( (res) => { upsertPrintingHouseAddressThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalPrintingHouseAddresses.insertPrintingHouseAddress(reqPrintingHouseAddress)
                                        .then( (res) => { upsertPrintingHouseAddressThen(res); } )
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
        
        let dalPrintingHouseAddresses = new PrintingHouseAddressesDal();
        let obj = this;

        dalPrintingHouseAddresses.deletePrintingHouseAddress(this.state.printinghouseid, this.state.addressid).then( (response) => {
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
            display: this.state.printinghouseid && this.state.addressid ? "block" : "none"
        }

        const lstPrintingHouseIDsFields = ["Name"];
        const lstPrintingHouseIDs = this._prepareOptionsList( this.state.printinghouses 
                                                                    ? Object.values(this.state.printinghouses) : null, 
                                                                    lstPrintingHouseIDsFields,
                                                                    false );
        const lstAddressIDsFields = ["Title"];
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
                                <h2>PrintingHouseAddress: { this.state.printinghouseaddress.PrintingHouseID ? this.state.printinghouses[this.state.printinghouseaddress.PrintingHouseID].Name : "" }</h2>
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
                                            value={ (this.state.printinghouseaddress && this.state.printinghouseaddress.PrintingHouseID) ? 
                                                        this.state.printinghouseaddress.PrintingHouseID : '-1' }
                                                        onChange={ (event) => this.onPrintingHouseIDChanged(event) }>
                                        {
                                            lstPrintingHouseIDs 
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
                                            value={ (this.state.printinghouseaddress && this.state.printinghouseaddress.AddressID) ? 
                                                        this.state.printinghouseaddress.AddressID : '-1' }
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
                                        <Checkbox   checked={ this.state.printinghouseaddress.IsPrimary ? true : false } 
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
                    <DialogTitle id="form-dialog-title">Delete PrintingHouseAddress</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this PrintingHouseAddress?
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

    _createEmptyPrintingHouseAddressObj() {
        let printinghouseaddress = new PrintingHouseAddressDto();

        return printinghouseaddress;
    }

    async _getPrintingHouseAddress()
    {
        if(this.state.printinghouseid && this.state.addressid) {
            let updatedState = this.state;
                  
            let dalPrintingHouseAddresses = new PrintingHouseAddressesDal();
            let response = await dalPrintingHouseAddresses.getPrintingHouseAddress(this.state.printinghouseid, this.state.addressid);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.printinghouseaddress = response.data;                
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

export default withRouter(PrintingHouseAddressPage);