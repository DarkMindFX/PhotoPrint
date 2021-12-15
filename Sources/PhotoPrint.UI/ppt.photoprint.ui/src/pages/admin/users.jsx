



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../../constants";

import PageHelper from "../../helpers/PageHelper";
import UsersDal from '../../dal/UsersDal';

import UserStatusesDal from '../../dal/UserStatusesDal';

import UserTypesDal from '../../dal/UserTypesDal';


class UsersPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            users: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}users`,
            urlNewEntity: `${rooPath}user/new`,
            urlEditEntity: `${rooPath}user/edit/`,
        };
        this._initColumns();
       
        this._getUserStatuses = this._getUserStatuses.bind(this);
        this._getUserTypes = this._getUserTypes.bind(this);
        this._getUsers = this._getUsers.bind(this);
        this._getUsers = this._getUsers.bind(this);
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
            			obj._getUserStatuses().then( () => {
			obj._getUserTypes().then( () => {
			obj._getUsers().then( () => {} );
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
                <h3>Users</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ User</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'Login', headerName: 'Login', width: 250 },
                { field: 'FirstName', headerName: 'FirstName', width: 250 },
                { field: 'MiddleName', headerName: 'MiddleName', width: 250 },
                { field: 'LastName', headerName: 'LastName', width: 250 },
                { field: 'FriendlyName', headerName: 'FriendlyName', width: 250 },
                { field: 'UserStatusID', headerName: 'UserStatusID', width: 250 },
                { field: 'UserTypeID', headerName: 'UserTypeID', width: 250 },
                { field: 'CreatedDate', headerName: 'CreatedDate', width: 250 },
                { field: 'ModifiedDate', headerName: 'ModifiedDate', width: 250 },
                { field: 'ModifiedByID', headerName: 'ModifiedByID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.users);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                Login: cs[c].Login,
                FirstName: cs[c].FirstName,
                MiddleName: cs[c].MiddleName,
                LastName: cs[c].LastName,
                FriendlyName: cs[c].FriendlyName,
                UserStatusID: cs[c].UserStatusID ? this.state.userstatuses[ cs[c].UserStatusID ].StatusName : "",
                UserTypeID: cs[c].UserTypeID ? this.state.usertypes[ cs[c].UserTypeID ].UserTypeName : "",
                CreatedDate: cs[c].CreatedDate,
                ModifiedDate: cs[c].ModifiedDate,
                ModifiedByID: cs[c].ModifiedByID ? this.state.users[ cs[c].ModifiedByID ].FirstName  + " " + this.state.users[ cs[c].ModifiedByID ].LastName: "",

            };

            records.push(r);
        }

        return records;
    }

    async _getUserStatuses() {
        let updatedState = this.state;
        updatedState.userstatuses = {};
        let dalUserStatuses = new UserStatusesDal();
        let response = await dalUserStatuses.getUserStatuses();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.userstatuses[response.data[s].ID] = response.data[s];             
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
    async _getUserTypes() {
        let updatedState = this.state;
        updatedState.usertypes = {};
        let dalUserTypes = new UserTypesDal();
        let response = await dalUserTypes.getUserTypes();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.usertypes[response.data[s].ID] = response.data[s];             
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

export default withRouter(UsersPage);