



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../../constants";

const PageHelper = require("../../helpers/PageHelper");
const UserContactsDal = require('../../dal/UserContactsDal');

const UsersDal = require('../../dal/UsersDal');

const ContactsDal = require('../../dal/ContactsDal');


class UserContactsPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            usercontacts: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}usercontacts`,
            urlNewEntity: `${rooPath}usercontact/new`,
            urlEditEntity: `${rooPath}usercontact/edit/`,
        };
        this._initColumns();
       
        this._getUsers = this._getUsers.bind(this);
        this._getContacts = this._getContacts.bind(this);
        this._getUserContacts = this._getUserContacts.bind(this);
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
			obj._getUserContacts().then( () => {} );
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
                <h3>UserContacts</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ UserContact</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'UserID', headerName: 'UserID', width: 250 },
                { field: 'ContactID', headerName: 'ContactID', width: 250 },
                { field: 'IsPrimary', headerName: 'IsPrimary', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.usercontacts);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                UserID: cs[c].UserID ? this.state.users[ cs[c].UserID ].Name : "",
                ContactID: cs[c].ContactID ? this.state.contacts[ cs[c].ContactID ].Name : "",
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
    

    async _getUserContacts() {
        let updatedState = this.state;
        updatedState.usercontacts = {};
        let dalUserContacts = new UserContactsDal();
        let response = await dalUserContacts.getUserContacts();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.usercontacts[response.data[s].ID] = response.data[s];             
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

export default withRouter(UserContactsPage);