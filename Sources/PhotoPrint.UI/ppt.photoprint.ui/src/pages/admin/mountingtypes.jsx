



const React = require("react";
const { Link, withRouter } = require('react-router-dom'
const { DataGrid } = require('@material-ui/data-grid';
const Alert = require('@material-ui/lab/Alert';
const { Button } = require('@material-ui/core';
const constants = require("../../constants";

const PageHelper = require("../../helpers/PageHelper");
const MountingTypesDal = require('../../dal/MountingTypesDal');


class MountingTypesPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            mountingtypes: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}mountingtypes`,
            urlNewEntity: `${rooPath}mountingtype/new`,
            urlEditEntity: `${rooPath}mountingtype/edit/`,
        };
        this._initColumns();
       
        this._getMountingTypes = this._getMountingTypes.bind(this);
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
            			obj._getMountingTypes().then( () => {} );
			
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
                <h3>MountingTypes</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ MountingType</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'MountingTypeName', headerName: 'MountingTypeName', width: 250 },
                { field: 'Description', headerName: 'Description', width: 250 },
                { field: 'ThumbnailUrl', headerName: 'ThumbnailUrl', width: 250 },
                { field: 'IsDeleted', headerName: 'IsDeleted', width: 250 },
                { field: 'CreatedDate', headerName: 'CreatedDate', width: 250 },
                { field: 'CreatedByID', headerName: 'CreatedByID', width: 250 },
                { field: 'ModifiedDate', headerName: 'ModifiedDate', width: 250 },
                { field: 'ModifiedByID', headerName: 'ModifiedByID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.mountingtypes);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                MountingTypeName: cs[c].MountingTypeName,
                Description: cs[c].Description,
                ThumbnailUrl: cs[c].ThumbnailUrl,
                IsDeleted: cs[c].IsDeleted,
                CreatedDate: cs[c].CreatedDate,
                CreatedByID: cs[c].CreatedByID,
                ModifiedDate: cs[c].ModifiedDate,
                ModifiedByID: cs[c].ModifiedByID,

            };

            records.push(r);
        }

        return records;
    }

    

    async _getMountingTypes() {
        let updatedState = this.state;
        updatedState.mountingtypes = {};
        let dalMountingTypes = new MountingTypesDal();
        let response = await dalMountingTypes.getMountingTypes();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.mountingtypes[response.data[s].ID] = response.data[s];             
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

export default withRouter(MountingTypesPage);