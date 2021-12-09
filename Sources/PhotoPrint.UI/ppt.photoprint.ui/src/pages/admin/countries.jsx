



const React = require("react";
const { Link, withRouter } = require('react-router-dom'
const { DataGrid } = require('@material-ui/data-grid';
const Alert = require('@material-ui/lab/Alert';
const { Button } = require('@material-ui/core';
const constants = require("../../constants";

const PageHelper = require("../../helpers/PageHelper");
const CountriesDal = require('../../dal/CountriesDal');


class CountriesPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            countries: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}countries`,
            urlNewEntity: `${rooPath}country/new`,
            urlEditEntity: `${rooPath}country/edit/`,
        };
        this._initColumns();
       
        this._getCountries = this._getCountries.bind(this);
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
            			obj._getCountries().then( () => {} );
			
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
                <h3>Countries</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ Country</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'CountryName', headerName: 'CountryName', width: 250 },
                { field: 'ISO', headerName: 'ISO', width: 250 },
                { field: 'IsDeleted', headerName: 'IsDeleted', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.countries);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                CountryName: cs[c].CountryName,
                ISO: cs[c].ISO,
                IsDeleted: cs[c].IsDeleted,

            };

            records.push(r);
        }

        return records;
    }

    

    async _getCountries() {
        let updatedState = this.state;
        updatedState.countries = {};
        let dalCountries = new CountriesDal();
        let response = await dalCountries.getCountries();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.countries[response.data[s].ID] = response.data[s];             
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

export default withRouter(CountriesPage);