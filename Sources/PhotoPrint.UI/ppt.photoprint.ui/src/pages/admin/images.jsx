



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../../constants";

import PageHelper from "../../helpers/PageHelper";
import ImagesDal from '../../dal/ImagesDal';


import CurrenciesDal from '../../dal/CurrenciesDal';

import UsersDal from '../../dal/UsersDal';


class ImagesPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            images: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}images`,
            urlNewEntity: `${rooPath}image/new`,
            urlEditEntity: `${rooPath}image/edit/`,
        };
        this._initColumns();
       
        this._getCurrencies = this._getCurrencies.bind(this);
        this._getUsers = this._getUsers.bind(this);
        this._getImages = this._getImages.bind(this);
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
            			obj._getCurrencies().then( () => {
			obj._getUsers().then( () => {
			obj._getImages().then( () => {} );
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
                <h3>Images</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ Image</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'Title', headerName: 'Title', width: 250 },
                { field: 'Description', headerName: 'Description', width: 250 },
                { field: 'OriginUrl', headerName: 'OriginUrl', width: 250 },
                { field: 'MaxWidth', headerName: 'MaxWidth', width: 250 },
                { field: 'MaxHeight', headerName: 'MaxHeight', width: 250 },
                { field: 'PriceAmount', headerName: 'PriceAmount', width: 250 },
                { field: 'PriceCurrencyID', headerName: 'PriceCurrencyID', width: 250 },
                { field: 'IsDeleted', headerName: 'IsDeleted', width: 250 },
                { field: 'CreatedByID', headerName: 'CreatedByID', width: 250 },
                { field: 'CreatedDate', headerName: 'CreatedDate', width: 250 },
                { field: 'ModifiedByID', headerName: 'ModifiedByID', width: 250 },
                { field: 'ModifiedDate', headerName: 'ModifiedDate', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.images);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                Title: cs[c].Title,
                Description: cs[c].Description,
                OriginUrl: cs[c].OriginUrl,
                MaxWidth: cs[c].MaxWidth,
                MaxHeight: cs[c].MaxHeight,
                PriceAmount: cs[c].PriceAmount,
                PriceCurrencyID: cs[c].PriceCurrencyID ? this.state.currencies[ cs[c].PriceCurrencyID ].ISO : "",
                IsDeleted: cs[c].IsDeleted,
                CreatedByID: cs[c].CreatedByID ? this.state.users[ cs[c].CreatedByID ].FirstName + " " + this.state.users[ cs[c].CreatedByID ].LastName : "",
                CreatedDate: cs[c].CreatedDate,
                ModifiedByID: cs[c].ModifiedByID ? this.state.users[ cs[c].ModifiedByID ].FirstName + " " + this.state.users[ cs[c].ModifiedByID ].LastName : "",
                ModifiedDate: cs[c].ModifiedDate,

            };

            records.push(r);
        }

        return records;
    }

    async _getCurrencies() {
        let updatedState = this.state;
        updatedState.currencies = {};
        let dalCurrencies = new CurrenciesDal();
        let response = await dalCurrencies.getCurrencies();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.currencies[response.data[s].ID] = response.data[s];             
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
    

    async _getImages() {
        let updatedState = this.state;
        updatedState.images = {};
        let dalImages = new ImagesDal();
        let response = await dalImages.getImages();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.images[response.data[s].ID] = response.data[s];             
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

export default withRouter(ImagesPage);