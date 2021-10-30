


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
const OrderPaymentDetailsesDal = require('../../dal/OrderPaymentDetailsesDal');

const OrdersDal = require('../../dal/OrdersDal');

const PaymentMethodsDal = require('../../dal/PaymentMethodsDal');

const UsersDal = require('../../dal/UsersDal');
const { OrderPaymentDetailsDto } = require('ppt.photoprint.dto')


class OrderPaymentDetailsPage extends React.Component {

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
            orderpaymentdetails: this._createEmptyOrderPaymentDetailsObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}orderpaymentdetailses`,
            urlThis: `${rooPath}orderpaymentdetails/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onOrderIDChanged = this.onOrderIDChanged.bind(this);
        this.onPaymentMethodIDChanged = this.onPaymentMethodIDChanged.bind(this);
        this.onPaymentTransUIDChanged = this.onPaymentTransUIDChanged.bind(this);
        this.onPaymentDateTimeChanged = this.onPaymentDateTimeChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this.onCreatedDateChanged = this.onCreatedDateChanged.bind(this);
        this.onCreatedByIDChanged = this.onCreatedByIDChanged.bind(this);
        this.onModifiedDateChanged = this.onModifiedDateChanged.bind(this);
        this.onModifiedByIDChanged = this.onModifiedByIDChanged.bind(this);
        this._getOrderPaymentDetails = this._getOrderPaymentDetails.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onOrderIDChanged = this.onOrderIDChanged.bind(this);
        this.onPaymentMethodIDChanged = this.onPaymentMethodIDChanged.bind(this);
        this.onPaymentTransUIDChanged = this.onPaymentTransUIDChanged.bind(this);
        this.onPaymentDateTimeChanged = this.onPaymentDateTimeChanged.bind(this);
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
            			obj._getOrders().then( () => {
			obj._getPaymentMethods().then( () => {
			obj._getUsers().then( () => {
			obj._getOrderPaymentDetails().then( () => {} );
			});});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onOrderIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderpaymentdetails.OrderID = newVal;

        this.setState(updatedState);
    }

    onPaymentMethodIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderpaymentdetails.PaymentMethodID = newVal;

        this.setState(updatedState);
    }

    onPaymentTransUIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.orderpaymentdetails.PaymentTransUID = newVal;

        this.setState(updatedState);
    }

    onPaymentDateTimeChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.orderpaymentdetails.PaymentDateTime = newVal;

        this.setState(updatedState);
    }

    onIsDeletedChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.checked;
        updatedState.orderpaymentdetails.IsDeleted = newVal;

        this.setState(updatedState);
    }

    onCreatedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.orderpaymentdetails.CreatedDate = newVal;

        this.setState(updatedState);
    }

    onCreatedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderpaymentdetails.CreatedByID = newVal;

        this.setState(updatedState);
    }

    onModifiedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.orderpaymentdetails.ModifiedDate = newVal;

        this.setState(updatedState);
    }

    onModifiedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderpaymentdetails.ModifiedByID = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving OrderPaymentDetails: ", this.state.orderpaymentdetails);
        
        if(this._validateForm()) {
            const reqOrderPaymentDetails = new OrderPaymentDetailsDto();
            reqOrderPaymentDetails.ID = this.state.id;
            reqOrderPaymentDetails.OrderID = this.state.orderpaymentdetails.OrderID;
            reqOrderPaymentDetails.PaymentMethodID = this.state.orderpaymentdetails.PaymentMethodID;
            reqOrderPaymentDetails.PaymentTransUID = this.state.orderpaymentdetails.PaymentTransUID;
            reqOrderPaymentDetails.PaymentDateTime = this.state.orderpaymentdetails.PaymentDateTime;
            reqOrderPaymentDetails.IsDeleted = this.state.orderpaymentdetails.IsDeleted;
            reqOrderPaymentDetails.CreatedDate = this.state.orderpaymentdetails.CreatedDate;
            reqOrderPaymentDetails.CreatedByID = this.state.orderpaymentdetails.CreatedByID;
            reqOrderPaymentDetails.ModifiedDate = this.state.orderpaymentdetails.ModifiedDate;
            reqOrderPaymentDetails.ModifiedByID = this.state.orderpaymentdetails.ModifiedByID;

            console.log("Saving OrderPaymentDetails: ", reqOrderPaymentDetails); 
        
            let dalOrderPaymentDetailses = new OrderPaymentDetailsesDal();

            let obj = this;

            function upsertOrderPaymentDetailsThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `OrderPaymentDetails was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `OrderPaymentDetails was updated`;                
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
                dalOrderPaymentDetailses.updateOrderPaymentDetails(reqOrderPaymentDetails)
                                        .then( (res) => { upsertOrderPaymentDetailsThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalOrderPaymentDetailses.insertOrderPaymentDetails(reqOrderPaymentDetails)
                                        .then( (res) => { upsertOrderPaymentDetailsThen(res); } )
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
        
        let dalOrderPaymentDetailses = new OrderPaymentDetailsesDal();
        let obj = this;

        dalOrderPaymentDetailses.deleteOrderPaymentDetails(this.state.id).then( (response) => {
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

        const lstOrderIDsFields = ["Name"];
        const lstOrderIDs = this._prepareOptionsList( this.state.orders 
                                                                    ? Object.values(this.state.orders) : null, 
                                                                    lstOrderIDsFields,
                                                                    false );
        const lstPaymentMethodIDsFields = ["Name"];
        const lstPaymentMethodIDs = this._prepareOptionsList( this.state.paymentmethods 
                                                                    ? Object.values(this.state.paymentmethods) : null, 
                                                                    lstPaymentMethodIDsFields,
                                                                    false );
        const lstCreatedByIDsFields = ["FirstName", "LastName"];
        const lstCreatedByIDs = this._prepareOptionsList( this.state.users 
                                                                    ? Object.values(this.state.users) : null, 
                                                                    lstCreatedByIDsFields,
                                                                    false );
        const lstModifiedByIDsFields = ["FirstName", "LastName"];
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
                                <h2>OrderPaymentDetails: { this.state.orderpaymentdetails.toString() }</h2>
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
                                <TextField  key="cbOrderID" 
                                            fullWidth
                                            select 
                                            label="OrderID" 
                                            value={ (this.state.orderpaymentdetails && this.state.orderpaymentdetails.OrderID) ? 
                                                        this.state.orderpaymentdetails.OrderID : '-1' }
                                                        onChange={ (event) => this.onOrderIDChanged(event) }>
                                        {
                                            lstOrderIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbPaymentMethodID" 
                                            fullWidth
                                            select 
                                            label="PaymentMethodID" 
                                            value={ (this.state.orderpaymentdetails && this.state.orderpaymentdetails.PaymentMethodID) ? 
                                                        this.state.orderpaymentdetails.PaymentMethodID : '-1' }
                                                        onChange={ (event) => this.onPaymentMethodIDChanged(event) }>
                                        {
                                            lstPaymentMethodIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="PaymentTransUID" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="PaymentTransUID" 
                                            value={this.state.orderpaymentdetails.PaymentTransUID}
                                            onChange={ (event) => { this.onPaymentTransUIDChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="PaymentDateTime" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="PaymentDateTime" 
                                            value={this.state.orderpaymentdetails.PaymentDateTime}
                                            onChange={ (event) => { this.onPaymentDateTimeChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <FormControlLabel
                                    key="lblIsDeleted"                        
                                    control = {
                                        <Checkbox   checked={ this.state.orderpaymentdetails.IsDeleted ? true : false } 
                                                    onChange={(event) => this.onIsDeletedChanged(event)} 
                                                    name="IsDeleted" />
                                        }
                                    label="IsDeleted"
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
                                            value={this.state.orderpaymentdetails.CreatedDate}
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
                                            value={ (this.state.orderpaymentdetails && this.state.orderpaymentdetails.CreatedByID) ? 
                                                        this.state.orderpaymentdetails.CreatedByID : '-1' }
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
                                            value={this.state.orderpaymentdetails.ModifiedDate}
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
                                            value={ (this.state.orderpaymentdetails && this.state.orderpaymentdetails.ModifiedByID) ? 
                                                        this.state.orderpaymentdetails.ModifiedByID : '-1' }
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
                    <DialogTitle id="form-dialog-title">Delete OrderPaymentDetails</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this OrderPaymentDetails?
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

    _createEmptyOrderPaymentDetailsObj() {
        let orderpaymentdetails = new OrderPaymentDetailsDto();

        return orderpaymentdetails;
    }

    async _getOrderPaymentDetails()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalOrderPaymentDetailses = new OrderPaymentDetailsesDal();
            let response = await dalOrderPaymentDetailses.getOrderPaymentDetails(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.orderpaymentdetails = response.data;                
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

    async _getOrders() {
        let updatedState = this.state;
        updatedState.orders = {};
        let dalOrders = new OrdersDal();
        let response = await dalOrders.getOrders();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.orders[response.data[s].ID] = response.data[s];             
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

    async _getPaymentMethods() {
        let updatedState = this.state;
        updatedState.paymentmethods = {};
        let dalPaymentMethods = new PaymentMethodsDal();
        let response = await dalPaymentMethods.getPaymentMethods();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.paymentmethods[response.data[s].ID] = response.data[s];             
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

export default withRouter(OrderPaymentDetailsPage);