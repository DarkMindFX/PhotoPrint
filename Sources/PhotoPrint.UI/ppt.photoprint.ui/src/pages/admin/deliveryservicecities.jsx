



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../constants";

const PageHelper = require("../helpers/PageHelper");
const DeliveryServiceCitiesDal = require('../dal/DeliveryServiceCitiesDal');

const DeliveryServicesDal = require('../dal/DeliveryServicesDal');

const CitiesDal = require('../dal/CitiesDal');


class DeliveryServiceCitiesPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = ''; // set the page hierarchy here

        this.state = { 
            deliveryservicecities: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}/deliveryservicecities`,
            urlNewEntity: `${rooPath}/deliveryservicecity/new`,
            urlEditEntity: `${rooPath}/deliveryservicecity/edit/`,
        };
        this._initColumns();
       
        this._getDeliveryServices = this._getDeliveryServices.bind(this);
        this._getCities = this._getCities.bind(this);
        this._getDeliveryServiceCities = this._getDeliveryServiceCities.bind(this);
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
            			obj._getDeliveryServices().then( () => {
			obj._getCities().then( () => {
			obj._getDeliveryServiceCities().then( () => {} );
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
                <h3>DeliveryServiceCities</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ DeliveryServiceCity</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'DeliveryServiceID', headerName: 'DeliveryServiceID', width: 250 },
                { field: 'CityID', headerName: 'CityID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.deliveryservicecities);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                DeliveryServiceID: cs[c].DeliveryServiceID ? this.state.deliveryservices[ cs[c].DeliveryServiceID ].Name : "",
                CityID: cs[c].CityID ? this.state.cities[ cs[c].CityID ].Name : "",

            };

            records.push(r);
        }

        return records;
    }

    async _getDeliveryServices() {
        let updatedState = this.state;
        updatedState.deliveryservices = {};
        let dalDeliveryServices = new DeliveryServicesDal();
        let response = await dalDeliveryServices.getDeliveryServices();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.deliveryservices[response.data[s].ID] = response.data[s];             
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
    

    async _getDeliveryServiceCities() {
        let updatedState = this.state;
        updatedState.deliveryservicecities = {};
        let dalDeliveryServiceCities = new DeliveryServiceCitiesDal();
        let response = await dalDeliveryServiceCities.getDeliveryServiceCities();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.deliveryservicecities[response.data[s].ID] = response.data[s];             
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

export default withRouter(DeliveryServiceCitiesPage);