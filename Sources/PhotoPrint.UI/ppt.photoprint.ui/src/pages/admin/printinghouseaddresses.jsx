



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../../constants";

import PageHelper from "../../helpers/PageHelper";
import PrintingHouseAddressesDal from '../../dal/PrintingHouseAddressesDal';

import PrintingHousesDal from '../../dal/PrintingHousesDal';

import AddressesDal from '../../dal/AddressesDal';


class PrintingHouseAddressesPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            printinghouseaddresses: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}printinghouseaddresses`,
            urlNewEntity: `${rooPath}printinghouseaddress/new`,
            urlEditEntity: `${rooPath}printinghouseaddress/edit/`,
        };
        this._initColumns();
       
        this._getPrintingHouses = this._getPrintingHouses.bind(this);
        this._getAddresses = this._getAddresses.bind(this);
        this._getPrintingHouseAddresses = this._getPrintingHouseAddresses.bind(this);
        this._redirectToLogin = this._redirectToLogin.bind(this);

        this.onRowClick = this.onRowClick.bind(this);
    }

    onRowClick(event) {
        const row = event.row;
        if(row) {
            this.props.history.push(this.state.urlEditEntity + row.PrintingHouseID + "/" + row.AddressID);
        }

    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getPrintingHouses().then( () => {
			obj._getAddresses().then( () => {
			obj._getPrintingHouseAddresses().then( () => {} );
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
                <h3>PrintingHouseAddresses</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ PrintingHouseAddress</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'PrintingHouse', headerName: 'PrintingHouseID', width: 250 },
                { field: 'Address', headerName: 'AddressID', width: 250 },
                { field: 'IsPrimary', headerName: 'IsPrimary', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.printinghouseaddresses);

        for(let c in cs) {

            let r = {
                id: c,
                PrintingHouseID: cs[c].PrintingHouseID,
                AddressID: cs[c].AddressID,
                PrintingHouse: cs[c].PrintingHouseID ? this.state.printinghouses[ cs[c].PrintingHouseID ].Name : "",
                Address: cs[c].AddressID ? this.state.addresses[ cs[c].AddressID ].Title : "",
                IsPrimary: cs[c].IsPrimary,

            };

            records.push(r);
        }

        return records;
    }

    async _getPrintingHouses() {
        let updatedState = this.state;
        updatedState.printinghouses = {};
        let dalPrintingHouses = new PrintingHousesDal();
        let response = await dalPrintingHouses.getPrintingHouses();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.printinghouses[response.data[s].ID] = response.data[s];             
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
    

    async _getPrintingHouseAddresses() {
        let updatedState = this.state;
        updatedState.printinghouseaddresses = {};
        let dalPrintingHouseAddresses = new PrintingHouseAddressesDal();
        let response = await dalPrintingHouseAddresses.getPrintingHouseAddresses();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.printinghouseaddresses[[response.data[s].PrintingHouseID,
                                                        response.data[s].AddressID]] = response.data[s];             
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

export default withRouter(PrintingHouseAddressesPage);