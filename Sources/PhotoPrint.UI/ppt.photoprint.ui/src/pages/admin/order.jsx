


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
const OrdersDal = require('../../dal/OrdersDal');

const UsersDal = require('../../dal/UsersDal');

const ContactsDal = require('../../dal/ContactsDal');

const AddressesDal = require('../../dal/AddressesDal');

const DeliveryServicesDal = require('../../dal/DeliveryServicesDal');
const { OrderDto } = require('ppt.photoprint.dto')


class OrderPage extends React.Component {

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
            order: this._createEmptyOrderObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}orders`,
            urlThis: `${rooPath}order/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onManagerIDChanged = this.onManagerIDChanged.bind(this);
        this.onUserIDChanged = this.onUserIDChanged.bind(this);
        this.onContactIDChanged = this.onContactIDChanged.bind(this);
        this.onDeliveryAddressIDChanged = this.onDeliveryAddressIDChanged.bind(this);
        this.onDeliveryServiceIDChanged = this.onDeliveryServiceIDChanged.bind(this);
        this.onCommentsChanged = this.onCommentsChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this.onCreatedDateChanged = this.onCreatedDateChanged.bind(this);
        this.onCreatedByIDChanged = this.onCreatedByIDChanged.bind(this);
        this.onModifiedDateChanged = this.onModifiedDateChanged.bind(this);
        this.onModifiedByIDChanged = this.onModifiedByIDChanged.bind(this);
        this._getOrder = this._getOrder.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onManagerIDChanged = this.onManagerIDChanged.bind(this);
        this.onUserIDChanged = this.onUserIDChanged.bind(this);
        this.onContactIDChanged = this.onContactIDChanged.bind(this);
        this.onDeliveryAddressIDChanged = this.onDeliveryAddressIDChanged.bind(this);
        this.onDeliveryServiceIDChanged = this.onDeliveryServiceIDChanged.bind(this);
        this.onCommentsChanged = this.onCommentsChanged.bind(this);
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
			obj._getContacts().then( () => {
			obj._getAddresses().then( () => {
			obj._getDeliveryServices().then( () => {
			obj._getOrder().then( () => {} );
			});});});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onManagerIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.order.ManagerID = newVal;

        this.setState(updatedState);
    }

    onUserIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.order.UserID = newVal;

        this.setState(updatedState);
    }

    onContactIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.order.ContactID = newVal;

        this.setState(updatedState);
    }

    onDeliveryAddressIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.order.DeliveryAddressID = newVal;

        this.setState(updatedState);
    }

    onDeliveryServiceIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.order.DeliveryServiceID = newVal;

        this.setState(updatedState);
    }

    onCommentsChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.order.Comments = newVal;

        this.setState(updatedState);
    }

    onIsDeletedChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.checked;
        updatedState.order.IsDeleted = newVal;

        this.setState(updatedState);
    }

    onCreatedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.order.CreatedDate = newVal;

        this.setState(updatedState);
    }

    onCreatedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.order.CreatedByID = newVal;

        this.setState(updatedState);
    }

    onModifiedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.order.ModifiedDate = newVal;

        this.setState(updatedState);
    }

    onModifiedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.order.ModifiedByID = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving Order: ", this.state.order);
        
        if(this._validateForm()) {
            const reqOrder = new OrderDto();
            reqOrder.ID = this.state.id;
            reqOrder.ManagerID = this.state.order.ManagerID;
            reqOrder.UserID = this.state.order.UserID;
            reqOrder.ContactID = this.state.order.ContactID;
            reqOrder.DeliveryAddressID = this.state.order.DeliveryAddressID;
            reqOrder.DeliveryServiceID = this.state.order.DeliveryServiceID;
            reqOrder.Comments = this.state.order.Comments;
            reqOrder.IsDeleted = this.state.order.IsDeleted;
            reqOrder.CreatedDate = this.state.order.CreatedDate;
            reqOrder.CreatedByID = this.state.order.CreatedByID;
            reqOrder.ModifiedDate = this.state.order.ModifiedDate;
            reqOrder.ModifiedByID = this.state.order.ModifiedByID;

            console.log("Saving Order: ", reqOrder); 
        
            let dalOrders = new OrdersDal();

            let obj = this;

            function upsertOrderThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `Order was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `Order was updated`;                
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
                dalOrders.updateOrder(reqOrder)
                                        .then( (res) => { upsertOrderThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalOrders.insertOrder(reqOrder)
                                        .then( (res) => { upsertOrderThen(res); } )
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
        
        let dalOrders = new OrdersDal();
        let obj = this;

        dalOrders.deleteOrder(this.state.id).then( (response) => {
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

        const lstManagerIDsFields = ["Name"];
        const lstManagerIDs = this._prepareOptionsList( this.state.users 
                                                                    ? Object.values(this.state.users) : null, 
                                                                    lstManagerIDsFields,
                                                                    true );
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
        const lstDeliveryAddressIDsFields = ["Name"];
        const lstDeliveryAddressIDs = this._prepareOptionsList( this.state.addresses 
                                                                    ? Object.values(this.state.addresses) : null, 
                                                                    lstDeliveryAddressIDsFields,
                                                                    false );
        const lstDeliveryServiceIDsFields = ["Name"];
        const lstDeliveryServiceIDs = this._prepareOptionsList( this.state.deliveryservices 
                                                                    ? Object.values(this.state.deliveryservices) : null, 
                                                                    lstDeliveryServiceIDsFields,
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
                                <h2>Order: { this.state.order.toString() }</h2>
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
                                <TextField  key="cbManagerID" 
                                            fullWidth
                                            select 
                                            label="ManagerID" 
                                            value={ (this.state.order && this.state.order.ManagerID) ? 
                                                        this.state.order.ManagerID : '-1' }
                                                        onChange={ (event) => this.onManagerIDChanged(event) }>
                                        {
                                            lstManagerIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbUserID" 
                                            fullWidth
                                            select 
                                            label="UserID" 
                                            value={ (this.state.order && this.state.order.UserID) ? 
                                                        this.state.order.UserID : '-1' }
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
                                            value={ (this.state.order && this.state.order.ContactID) ? 
                                                        this.state.order.ContactID : '-1' }
                                                        onChange={ (event) => this.onContactIDChanged(event) }>
                                        {
                                            lstContactIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbDeliveryAddressID" 
                                            fullWidth
                                            select 
                                            label="DeliveryAddressID" 
                                            value={ (this.state.order && this.state.order.DeliveryAddressID) ? 
                                                        this.state.order.DeliveryAddressID : '-1' }
                                                        onChange={ (event) => this.onDeliveryAddressIDChanged(event) }>
                                        {
                                            lstDeliveryAddressIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbDeliveryServiceID" 
                                            fullWidth
                                            select 
                                            label="DeliveryServiceID" 
                                            value={ (this.state.order && this.state.order.DeliveryServiceID) ? 
                                                        this.state.order.DeliveryServiceID : '-1' }
                                                        onChange={ (event) => this.onDeliveryServiceIDChanged(event) }>
                                        {
                                            lstDeliveryServiceIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Comments" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Comments" 
                                            value={this.state.order.Comments}
                                            onChange={ (event) => { this.onCommentsChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <FormControlLabel
                                    key="lblIsDeleted"                        
                                    control = {
                                        <Checkbox   checked={ this.state.order.IsDeleted } 
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
                                            value={this.state.order.CreatedDate}
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
                                            value={ (this.state.order && this.state.order.CreatedByID) ? 
                                                        this.state.order.CreatedByID : '-1' }
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
                                            value={this.state.order.ModifiedDate}
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
                                            value={ (this.state.order && this.state.order.ModifiedByID) ? 
                                                        this.state.order.ModifiedByID : '-1' }
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
                    <DialogTitle id="form-dialog-title">Delete Order</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this Order?
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

    _createEmptyOrderObj() {
        let order = new OrderDto();

        return order;
    }

    async _getOrder()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalOrders = new OrdersDal();
            let response = await dalOrders.getOrder(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.order = response.data;                
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

    async _getDeliveryServices() {
        let updatedState = this.state;
        updatedState.deliveryservices = {};
        let dalDeliveryServices = new DeliveryServicesDal();
        let response = await dalDeliveryServices.getDeliveryServices();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.deliveryservices[response.data[s].ID] = response.data[s];             
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

export default withRouter(OrderPage);