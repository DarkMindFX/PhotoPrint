



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../../constants";

const PageHelper = require("../../helpers/PageHelper");
const OrderPaymentDetailsesDal = require('../../dal/OrderPaymentDetailsesDal');

const OrdersDal = require('../../dal/OrdersDal');

const PaymentMethodsDal = require('../../dal/PaymentMethodsDal');

const UsersDal = require('../../dal/UsersDal');


class OrderPaymentDetailsesPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            orderpaymentdetailses: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}orderpaymentdetailses`,
            urlNewEntity: `${rooPath}orderpaymentdetails/new`,
            urlEditEntity: `${rooPath}orderpaymentdetails/edit/`,
        };
        this._initColumns();
       
        this._getOrders = this._getOrders.bind(this);
        this._getPaymentMethods = this._getPaymentMethods.bind(this);
        this._getUsers = this._getUsers.bind(this);
        this._getOrderPaymentDetailses = this._getOrderPaymentDetailses.bind(this);
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
			obj._getPaymentMethods().then( () => {
			obj._getUsers().then( () => {
			obj._getOrderPaymentDetailses().then( () => {} );
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
                <h3>OrderPaymentDetailses</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ OrderPaymentDetails</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'OrderID', headerName: 'OrderID', width: 250 },
                { field: 'PaymentMethodID', headerName: 'PaymentMethodID', width: 250 },
                { field: 'PaymentTransUID', headerName: 'PaymentTransUID', width: 250 },
                { field: 'PaymentDateTime', headerName: 'PaymentDateTime', width: 250 },
                { field: 'IsDeleted', headerName: 'IsDeleted', width: 250 },
                { field: 'CreatedDate', headerName: 'CreatedDate', width: 250 },
                { field: 'CreatedByID', headerName: 'CreatedByID', width: 250 },
                { field: 'ModifiedDate', headerName: 'ModifiedDate', width: 250 },
                { field: 'ModifiedByID', headerName: 'ModifiedByID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.orderpaymentdetailses);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                OrderID: cs[c].OrderID ? this.state.orders[ cs[c].OrderID ].Name : "",
                PaymentMethodID: cs[c].PaymentMethodID ? this.state.paymentmethods[ cs[c].PaymentMethodID ].Name : "",
                PaymentTransUID: cs[c].PaymentTransUID,
                PaymentDateTime: cs[c].PaymentDateTime,
                IsDeleted: cs[c].IsDeleted,
                CreatedDate: cs[c].CreatedDate,
                CreatedByID: cs[c].CreatedByID ? this.state.users[ cs[c].CreatedByID ].Name : "",
                ModifiedDate: cs[c].ModifiedDate,
                ModifiedByID: cs[c].ModifiedByID ? this.state.users[ cs[c].ModifiedByID ].Name : "",

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
    

    async _getOrderPaymentDetailses() {
        let updatedState = this.state;
        updatedState.orderpaymentdetailses = {};
        let dalOrderPaymentDetailses = new OrderPaymentDetailsesDal();
        let response = await dalOrderPaymentDetailses.getOrderPaymentDetailses();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.orderpaymentdetailses[response.data[s].ID] = response.data[s];             
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

export default withRouter(OrderPaymentDetailsesPage);