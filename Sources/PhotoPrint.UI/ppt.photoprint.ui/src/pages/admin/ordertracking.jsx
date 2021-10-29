


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
const OrderTrackingsDal = require('../../dal/OrderTrackingsDal');

const OrdersDal = require('../../dal/OrdersDal');

const OrderStatusesDal = require('../../dal/OrderStatusesDal');

const UsersDal = require('../../dal/UsersDal');
const { OrderTrackingDto } = require('ppt.photoprint.dto')


class OrderTrackingPage extends React.Component {

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
            ordertracking: this._createEmptyOrderTrackingObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}ordertrackings`,
            urlThis: `${rooPath}ordertracking/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onOrderIDChanged = this.onOrderIDChanged.bind(this);
        this.onOrderStatusIDChanged = this.onOrderStatusIDChanged.bind(this);
        this.onSetDateChanged = this.onSetDateChanged.bind(this);
        this.onSetByIDChanged = this.onSetByIDChanged.bind(this);
        this.onCommentChanged = this.onCommentChanged.bind(this);
        this._getOrderTracking = this._getOrderTracking.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onOrderIDChanged = this.onOrderIDChanged.bind(this);
        this.onOrderStatusIDChanged = this.onOrderStatusIDChanged.bind(this);
        this.onSetDateChanged = this.onSetDateChanged.bind(this);
        this.onSetByIDChanged = this.onSetByIDChanged.bind(this);
        this.onCommentChanged = this.onCommentChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getOrders().then( () => {
			obj._getOrderStatuses().then( () => {
			obj._getUsers().then( () => {
			obj._getOrderTracking().then( () => {} );
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
        updatedState.ordertracking.OrderID = newVal;

        this.setState(updatedState);
    }

    onOrderStatusIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.ordertracking.OrderStatusID = newVal;

        this.setState(updatedState);
    }

    onSetDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.ordertracking.SetDate = newVal;

        this.setState(updatedState);
    }

    onSetByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.ordertracking.SetByID = newVal;

        this.setState(updatedState);
    }

    onCommentChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.ordertracking.Comment = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving OrderTracking: ", this.state.ordertracking);
        
        if(this._validateForm()) {
            const reqOrderTracking = new OrderTrackingDto();
            reqOrderTracking.ID = this.state.id;
            reqOrderTracking.OrderID = this.state.ordertracking.OrderID;
            reqOrderTracking.OrderStatusID = this.state.ordertracking.OrderStatusID;
            reqOrderTracking.SetDate = this.state.ordertracking.SetDate;
            reqOrderTracking.SetByID = this.state.ordertracking.SetByID;
            reqOrderTracking.Comment = this.state.ordertracking.Comment;

            console.log("Saving OrderTracking: ", reqOrderTracking); 
        
            let dalOrderTrackings = new OrderTrackingsDal();

            let obj = this;

            function upsertOrderTrackingThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `OrderTracking was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `OrderTracking was updated`;                
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
                dalOrderTrackings.updateOrderTracking(reqOrderTracking)
                                        .then( (res) => { upsertOrderTrackingThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalOrderTrackings.insertOrderTracking(reqOrderTracking)
                                        .then( (res) => { upsertOrderTrackingThen(res); } )
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
        
        let dalOrderTrackings = new OrderTrackingsDal();
        let obj = this;

        dalOrderTrackings.deleteOrderTracking(this.state.id).then( (response) => {
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
        const lstOrderStatusIDsFields = ["Name"];
        const lstOrderStatusIDs = this._prepareOptionsList( this.state.orderstatuses 
                                                                    ? Object.values(this.state.orderstatuses) : null, 
                                                                    lstOrderStatusIDsFields,
                                                                    false );
        const lstSetByIDsFields = ["Name"];
        const lstSetByIDs = this._prepareOptionsList( this.state.users 
                                                                    ? Object.values(this.state.users) : null, 
                                                                    lstSetByIDsFields,
                                                                    false );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>OrderTracking: { this.state.ordertracking.toString() }</h2>
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
                                            value={ (this.state.ordertracking && this.state.ordertracking.OrderID) ? 
                                                        this.state.ordertracking.OrderID : '-1' }
                                                        onChange={ (event) => this.onOrderIDChanged(event) }>
                                        {
                                            lstOrderIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbOrderStatusID" 
                                            fullWidth
                                            select 
                                            label="OrderStatusID" 
                                            value={ (this.state.ordertracking && this.state.ordertracking.OrderStatusID) ? 
                                                        this.state.ordertracking.OrderStatusID : '-1' }
                                                        onChange={ (event) => this.onOrderStatusIDChanged(event) }>
                                        {
                                            lstOrderStatusIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="SetDate" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="SetDate" 
                                            value={this.state.ordertracking.SetDate}
                                            onChange={ (event) => { this.onSetDateChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbSetByID" 
                                            fullWidth
                                            select 
                                            label="SetByID" 
                                            value={ (this.state.ordertracking && this.state.ordertracking.SetByID) ? 
                                                        this.state.ordertracking.SetByID : '-1' }
                                                        onChange={ (event) => this.onSetByIDChanged(event) }>
                                        {
                                            lstSetByIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Comment" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Comment" 
                                            value={this.state.ordertracking.Comment}
                                            onChange={ (event) => { this.onCommentChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete OrderTracking</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this OrderTracking?
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

    _createEmptyOrderTrackingObj() {
        let ordertracking = new OrderTrackingDto();

        return ordertracking;
    }

    async _getOrderTracking()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalOrderTrackings = new OrderTrackingsDal();
            let response = await dalOrderTrackings.getOrderTracking(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.ordertracking = response.data;                
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

    async _getOrderStatuses() {
        let updatedState = this.state;
        updatedState.orderstatuses = {};
        let dalOrderStatuses = new OrderStatusesDal();
        let response = await dalOrderStatuses.getOrderStatuses();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.orderstatuses[response.data[s].ID] = response.data[s];             
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

export default withRouter(OrderTrackingPage);