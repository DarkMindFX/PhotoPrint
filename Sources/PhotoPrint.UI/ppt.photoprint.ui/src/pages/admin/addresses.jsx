



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../../constants";

const PageHelper = require("../../helpers/PageHelper");
const AddressesDal = require('../../dal/AddressesDal');

const AddressTypesDal = require('../../dal/AddressTypesDal');

const CitiesDal = require('../../dal/CitiesDal');

const UsersDal = require('../../dal/UsersDal');


class AddressesPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            addresses: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}addresses`,
            urlNewEntity: `${rooPath}address/new`,
            urlEditEntity: `${rooPath}address/edit/`,
        };
        this._initColumns();
       
        this._getAddressTypes = this._getAddressTypes.bind(this);
        this._getCities = this._getCities.bind(this);
        this._getUsers = this._getUsers.bind(this);
        this._getAddresses = this._getAddresses.bind(this);
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
            			obj._getAddressTypes().then( () => {
			obj._getCities().then( () => {
			obj._getUsers().then( () => {
			obj._getAddresses().then( () => {} );
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
                <h3>Addresses</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ Address</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'AddressTypeID', headerName: 'AddressTypeID', width: 250 },
                { field: 'Title', headerName: 'Title', width: 250 },
                { field: 'CityID', headerName: 'CityID', width: 250 },
                { field: 'Street', headerName: 'Street', width: 250 },
                { field: 'BuildingNo', headerName: 'BuildingNo', width: 250 },
                { field: 'ApartmentNo', headerName: 'ApartmentNo', width: 250 },
                { field: 'Comment', headerName: 'Comment', width: 250 },
                { field: 'CreatedByID', headerName: 'CreatedByID', width: 250 },
                { field: 'CreatedDate', headerName: 'CreatedDate', width: 250 },
                { field: 'ModifiedByID', headerName: 'ModifiedByID', width: 250 },
                { field: 'ModifiedDate', headerName: 'ModifiedDate', width: 250 },
                { field: 'IsDeleted', headerName: 'IsDeleted', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.addresses);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                AddressTypeID: cs[c].AddressTypeID ? this.state.addresstypes[ cs[c].AddressTypeID ].AddressTypeName : "",
                Title: cs[c].Title,
                CityID: cs[c].CityID ? this.state.cities[ cs[c].CityID ].CityName : "",
                Street: cs[c].Street,
                BuildingNo: cs[c].BuildingNo,
                ApartmentNo: cs[c].ApartmentNo,
                Comment: cs[c].Comment,
                CreatedByID: cs[c].CreatedByID ? this.state.users[ cs[c].CreatedByID ].FirstName + " " + this.state.users[ cs[c].CreatedByID ].LastName : "",
                CreatedDate: cs[c].CreatedDate,
                ModifiedByID: cs[c].ModifiedByID ? this.state.users[ cs[c].ModifiedByID ].FirstName + " " + this.state.users[ cs[c].ModifiedByID ].LastName : "",
                ModifiedDate: cs[c].ModifiedDate,
                IsDeleted: cs[c].IsDeleted,

            };

            records.push(r);
        }

        return records;
    }

    async _getAddressTypes() {
        let updatedState = this.state;
        updatedState.addresstypes = {};
        let dalAddressTypes = new AddressTypesDal();
        let response = await dalAddressTypes.getAddressTypes();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.addresstypes[response.data[s].ID] = response.data[s];             
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
    async _getCities() {
        let updatedState = this.state;
        updatedState.cities = {};
        let dalCities = new CitiesDal();
        let response = await dalCities.getCities();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.cities[response.data[s].ID] = response.data[s];             
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

export default withRouter(AddressesPage);