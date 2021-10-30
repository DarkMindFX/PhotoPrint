



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../../constants";

const PageHelper = require("../../helpers/PageHelper");
const OrdersDal = require('../../dal/OrdersDal');

const UsersDal = require('../../dal/UsersDal');

const ContactsDal = require('../../dal/ContactsDal');

const AddressesDal = require('../../dal/AddressesDal');

const DeliveryServicesDal = require('../../dal/DeliveryServicesDal');


class OrdersPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            orders: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}orders`,
            urlNewEntity: `${rooPath}order/new`,
            urlEditEntity: `${rooPath}order/edit/`,
        };
        this._initColumns();
       
        this._getUsers = this._getUsers.bind(this);
        this._getContacts = this._getContacts.bind(this);
        this._getAddresses = this._getAddresses.bind(this);
        this._getDeliveryServices = this._getDeliveryServices.bind(this);
        this._getOrders = this._getOrders.bind(this);
        this._redirectToLogin = this._redirectToLogin.bind(this);

        this.onRowClick = this.onRowClick.bind(this);
    }

    onRowClick(event) {
        const row = event.row;
        if(row) {
            this.props.history.push(this.state.urlEditEntity + row.id);
        }

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
			obj._getOrders().then( () => {} );
			});});});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    render() {
        let records = this._getRecords();

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        return (
            <div style={{ height: 500, width: '100%' }}>
                <h3>Orders</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ Order</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'ManagerID', headerName: 'ManagerID', width: 250 },
                { field: 'UserID', headerName: 'UserID', width: 250 },
                { field: 'ContactID', headerName: 'ContactID', width: 250 },
                { field: 'DeliveryAddressID', headerName: 'DeliveryAddressID', width: 250 },
                { field: 'DeliveryServiceID', headerName: 'DeliveryServiceID', width: 250 },
                { field: 'Comments', headerName: 'Comments', width: 250 },
                { field: 'IsDeleted', headerName: 'IsDeleted', width: 250 },
                { field: 'CreatedDate', headerName: 'CreatedDate', width: 250 },
                { field: 'CreatedByID', headerName: 'CreatedByID', width: 250 },
                { field: 'ModifiedDate', headerName: 'ModifiedDate', width: 250 },
                { field: 'ModifiedByID', headerName: 'ModifiedByID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.orders);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                ManagerID: cs[c].ManagerID ? this.state.users[ cs[c].ManagerID ].FirstName + " " + this.state.users[ cs[c].ManagerID ].LastName : "",
                UserID: cs[c].UserID ? this.state.users[ cs[c].UserID ].FirstName + " " + this.state.users[ cs[c].UserID ].LastName : "",
                ContactID: cs[c].ContactID ? this.state.contacts[ cs[c].ContactID ].Title : "",
                DeliveryAddressID: cs[c].DeliveryAddressID ? this.state.addresses[ cs[c].DeliveryAddressID ].Title : "",
                DeliveryServiceID: cs[c].DeliveryServiceID ? this.state.deliveryservices[ cs[c].DeliveryServiceID ].DeliveryServiceName : "",
                Comments: cs[c].Comments,
                IsDeleted: cs[c].IsDeleted,
                CreatedDate: cs[c].CreatedDate,
                CreatedByID: cs[c].CreatedByID ? this.state.users[ cs[c].CreatedByID ].FirstName + " " + this.state.users[ cs[c].CreatedByID ].LastName : "",
                ModifiedDate: cs[c].ModifiedDate,
                ModifiedByID: cs[c].ModifiedByID ? this.state.users[ cs[c].ModifiedByID ].FirstName + " " + this.state.users[ cs[c].ModifiedByID ].LastName : "",

            };

            records.push(r);
        }

        return records;
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

    _showError(updatedState, response) {
        var error = JSON.parse(response.data.response);
        updatedState.showError = true;
        updatedState.error = error.Message;
    }

    _redirectToLogin()
    {        
        this._pageHelper.redirectToLogin(this.state.urlThis);  
    }
}

export default withRouter(OrdersPage);