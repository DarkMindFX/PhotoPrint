



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../constants";

const PageHelper = require("../helpers/PageHelper");
const UnitsDal = require('../dal/UnitsDal');


class UnitsPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = ''; // set the page hierarchy here

        this.state = { 
            units: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}/units`,
            urlNewEntity: `${rooPath}/unit/new`,
            urlEditEntity: `${rooPath}/unit/edit/`,
        };
        this._initColumns();
       
        this._getUnits = this._getUnits.bind(this);
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
            			obj._getUnits().then( () => {} );
			
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
                <h3>Units</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ Unit</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'UnitName', headerName: 'UnitName', width: 250 },
                { field: 'UnitAbbr', headerName: 'UnitAbbr', width: 250 },
                { field: 'Description', headerName: 'Description', width: 250 },
                { field: 'IsDeleted', headerName: 'IsDeleted', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.units);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                UnitName: cs[c].UnitName,
                UnitAbbr: cs[c].UnitAbbr,
                Description: cs[c].Description,
                IsDeleted: cs[c].IsDeleted,

            };

            records.push(r);
        }

        return records;
    }

    

    async _getUnits() {
        let updatedState = this.state;
        updatedState.units = {};
        let dalUnits = new UnitsDal();
        let response = await dalUnits.getUnits();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.units[response.data[s].ID] = response.data[s];             
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

export default withRouter(UnitsPage);