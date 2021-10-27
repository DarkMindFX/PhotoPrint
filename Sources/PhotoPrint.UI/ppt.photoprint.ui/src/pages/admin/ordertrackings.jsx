



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../constants";

const PageHelper = require("../helpers/PageHelper");
const OrderTrackingsDal = require('../dal/OrderTrackingsDal');

const OrdersDal = require('../dal/OrdersDal');

const OrderStatusesDal = require('../dal/OrderStatusesDal');

const UsersDal = require('../dal/UsersDal');


class OrderTrackingsPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = ''; // set the page hierarchy here

        this.state = { 
            ordertrackings: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}/ordertrackings`,
            urlNewEntity: `${rooPath}/ordertracking/new`,
            urlEditEntity: `${rooPath}/ordertracking/edit/`,
        };
        this._initColumns();
       
        this._getOrders = this._getOrders.bind(this);
        this._getOrderStatuses = this._getOrderStatuses.bind(this);
        this._getUsers = this._getUsers.bind(this);
        this._getOrderTrackings = this._getOrderTrackings.bind(this);
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
            			obj._getOrders().then( () => {
			obj._getOrderStatuses().then( () => {
			obj._getUsers().then( () => {
			obj._getOrderTrackings().then( () => {} );
			});});});
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
                <h3>OrderTrackings</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ OrderTracking</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'OrderID', headerName: 'OrderID', width: 250 },
                { field: 'OrderStatusID', headerName: 'OrderStatusID', width: 250 },
                { field: 'SetDate', headerName: 'SetDate', width: 250 },
                { field: 'SetByID', headerName: 'SetByID', width: 250 },
                { field: 'Comment', headerName: 'Comment', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.ordertrackings);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                OrderID: cs[c].OrderID ? this.state.orders[ cs[c].OrderID ].Name : "",
                OrderStatusID: cs[c].OrderStatusID ? this.state.orderstatuses[ cs[c].OrderStatusID ].Name : "",
                SetDate: cs[c].SetDate,
                SetByID: cs[c].SetByID ? this.state.users[ cs[c].SetByID ].Name : "",
                Comment: cs[c].Comment,

            };

            records.push(r);
        }

        return records;
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
    

    async _getOrderTrackings() {
        let updatedState = this.state;
        updatedState.ordertrackings = {};
        let dalOrderTrackings = new OrderTrackingsDal();
        let response = await dalOrderTrackings.getOrderTrackings();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.ordertrackings[response.data[s].ID] = response.data[s];             
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

export default withRouter(OrderTrackingsPage);