



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../../constants";

import PageHelper from "../../helpers/PageHelper";
import UserConfirmationsDal from '../../dal/UserConfirmationsDal';

import UsersDal from '../../dal/UsersDal';


class UserConfirmationsPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            userconfirmations: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}userconfirmations`,
            urlNewEntity: `${rooPath}userconfirmation/new`,
            urlEditEntity: `${rooPath}userconfirmation/edit/`,
        };
        this._initColumns();
       
        this._getUsers = this._getUsers.bind(this);
        this._getUserConfirmations = this._getUserConfirmations.bind(this);
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
			obj._getUserConfirmations().then( () => {} );
			});
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
                <h3>UserConfirmations</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ UserConfirmation</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'UserID', headerName: 'UserID', width: 250 },
                { field: 'ConfirmationCode', headerName: 'ConfirmationCode', width: 250 },
                { field: 'Comfirmed', headerName: 'Comfirmed', width: 250 },
                { field: 'ExpiresDate', headerName: 'ExpiresDate', width: 250 },
                { field: 'ConfirmationDate', headerName: 'ConfirmationDate', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.userconfirmations);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                UserID: cs[c].UserID ? this.state.users[ cs[c].UserID ].FirstName + " " + this.state.users[ cs[c].UserID ].LastName : "",
                ConfirmationCode: cs[c].ConfirmationCode,
                Comfirmed: cs[c].Comfirmed,
                ExpiresDate: cs[c].ExpiresDate,
                ConfirmationDate: cs[c].ConfirmationDate,

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
    

    async _getUserConfirmations() {
        let updatedState = this.state;
        updatedState.userconfirmations = {};
        let dalUserConfirmations = new UserConfirmationsDal();
        let response = await dalUserConfirmations.getUserConfirmations();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.userconfirmations[response.data[s].ID] = response.data[s];             
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

export default withRouter(UserConfirmationsPage);