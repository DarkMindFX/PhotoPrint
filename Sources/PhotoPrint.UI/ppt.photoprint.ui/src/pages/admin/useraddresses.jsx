



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../constants";

const PageHelper = require("../helpers/PageHelper");
const UserAddressesDal = require('../dal/UserAddressesDal');

const UsersDal = require('../dal/UsersDal');

const AddressesDal = require('../dal/AddressesDal');


class UserAddressesPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = ''; // set the page hierarchy here

        this.state = { 
            useraddresses: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}/useraddresses`,
            urlNewEntity: `${rooPath}/useraddress/new`,
            urlEditEntity: `${rooPath}/useraddress/edit/`,
        };
        this._initColumns();
       
        this._getUsers = this._getUsers.bind(this);
        this._getAddresses = this._getAddresses.bind(this);
        this._getUserAddresses = this._getUserAddresses.bind(this);
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
			obj._getAddresses().then( () => {
			obj._getUserAddresses().then( () => {} );
			});});
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
                <h3>UserAddresses</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ UserAddress</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'UserID', headerName: 'UserID', width: 250 },
                { field: 'AddressID', headerName: 'AddressID', width: 250 },
                { field: 'IsPrimary', headerName: 'IsPrimary', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.useraddresses);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                UserID: cs[c].UserID ? this.state.users[ cs[c].UserID ].Name : "",
                AddressID: cs[c].AddressID ? this.state.addresses[ cs[c].AddressID ].Name : "",
                IsPrimary: cs[c].IsPrimary,

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
    

    async _getUserAddresses() {
        let updatedState = this.state;
        updatedState.useraddresses = {};
        let dalUserAddresses = new UserAddressesDal();
        let response = await dalUserAddresses.getUserAddresses();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.useraddresses[response.data[s].ID] = response.data[s];             
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

export default withRouter(UserAddressesPage);