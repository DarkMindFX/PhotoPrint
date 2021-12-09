



const React = require("react");
const { Link, withRouter } = require('react-router-dom');
const { DataGrid } = require('@material-ui/data-grid');
const Alert = require('@material-ui/lab/Alert');
const { Button } = require('@material-ui/core');
const constants = require("../../constants");

const PageHelper = require("../../helpers/PageHelper");
const CategoriesDal = require('../../dal/CategoriesDal');

const UsersDal = require('../../dal/UsersDal');


class CategoriesPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            categories: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}categories`,
            urlNewEntity: `${rooPath}category/new`,
            urlEditEntity: `${rooPath}category/edit/`,
        };
        this._initColumns();
       
        this._getCategories = this._getCategories.bind(this);
        this._getUsers = this._getUsers.bind(this);
        this._getCategories = this._getCategories.bind(this);
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
			obj._getCategories().then( () => {} );
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
                <h3>Categories</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ Category</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'CategoryName', headerName: 'CategoryName', width: 250 },
                { field: 'Description', headerName: 'Description', width: 250 },
                { field: 'ParentID', headerName: 'ParentID', width: 250 },
                { field: 'IsDeleted', headerName: 'IsDeleted', width: 250 },
                { field: 'CreatedDate', headerName: 'CreatedDate', width: 250 },
                { field: 'CreatedByID', headerName: 'CreatedByID', width: 250 },
                { field: 'ModifiedDate', headerName: 'ModifiedDate', width: 250 },
                { field: 'ModifiedByID', headerName: 'ModifiedByID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.categories);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                CategoryName: cs[c].CategoryName,
                Description: cs[c].Description,
                ParentID: cs[c].ParentID ? this.state.categories[ cs[c].ParentID ].CategoryName : "",
                IsDeleted: cs[c].IsDeleted,
                CreatedDate: cs[c].CreatedDate,
                CreatedByID: cs[c].CreatedByID ? this.state.users[ cs[c].CreatedByID ].FirstName + " " + this.state.users[ cs[c].CreatedByID ].LastName : "",
                ModifiedDate: cs[c].ModifiedDate,
                ModifiedByID: cs[c].ModifiedByID ? this.state.users[ cs[c].ModifiedByID ].FirstName + " " + this.state.users[ cs[c].ModifiedByID ].LastName : "",

            };

            records.push(r);
        }

        return records;
    }

    async _getCategories() {
        let updatedState = this.state;
        updatedState.categories = {};
        let dalCategories = new CategoriesDal();
        let response = await dalCategories.getCategories();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.categories[response.data[s].ID] = response.data[s];             
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
    

    async _getCategories() {
        let updatedState = this.state;
        updatedState.categories = {};
        let dalCategories = new CategoriesDal();
        let response = await dalCategories.getCategories();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.categories[response.data[s].ID] = response.data[s];             
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

export default withRouter(CategoriesPage);