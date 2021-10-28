



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../../constants";

const PageHelper = require("../../helpers/PageHelper");
const OrderItemsDal = require('../../dal/OrderItemsDal');

const OrdersDal = require('../../dal/OrdersDal');

const ImagesDal = require('../../dal/ImagesDal');

const SizesDal = require('../../dal/SizesDal');

const FrameTypesDal = require('../../dal/FrameTypesDal');

const MatsDal = require('../../dal/MatsDal');

const MaterialTypesDal = require('../../dal/MaterialTypesDal');

const MountingTypesDal = require('../../dal/MountingTypesDal');

const CurrenciesDal = require('../../dal/CurrenciesDal');

const PrintingHousesDal = require('../../dal/PrintingHousesDal');

const UsersDal = require('../../dal/UsersDal');


class OrderItemsPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            orderitems: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}orderitems`,
            urlNewEntity: `${rooPath}orderitem/new`,
            urlEditEntity: `${rooPath}orderitem/edit/`,
        };
        this._initColumns();
       
        this._getOrders = this._getOrders.bind(this);
        this._getImages = this._getImages.bind(this);
        this._getSizes = this._getSizes.bind(this);
        this._getFrameTypes = this._getFrameTypes.bind(this);
        this._getMats = this._getMats.bind(this);
        this._getMaterialTypes = this._getMaterialTypes.bind(this);
        this._getMountingTypes = this._getMountingTypes.bind(this);
        this._getCurrencies = this._getCurrencies.bind(this);
        this._getPrintingHouses = this._getPrintingHouses.bind(this);
        this._getUsers = this._getUsers.bind(this);
        this._getOrderItems = this._getOrderItems.bind(this);
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
            			obj._getOrders().then( () => {
			obj._getImages().then( () => {
			obj._getSizes().then( () => {
			obj._getFrameTypes().then( () => {
			obj._getMats().then( () => {
			obj._getMaterialTypes().then( () => {
			obj._getMountingTypes().then( () => {
			obj._getCurrencies().then( () => {
			obj._getPrintingHouses().then( () => {
			obj._getUsers().then( () => {
			obj._getOrderItems().then( () => {} );
			});});});});});});});});});});
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
                <h3>OrderItems</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ OrderItem</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'OrderID', headerName: 'OrderID', width: 250 },
                { field: 'ImageID', headerName: 'ImageID', width: 250 },
                { field: 'Width', headerName: 'Width', width: 250 },
                { field: 'Height', headerName: 'Height', width: 250 },
                { field: 'SizeID', headerName: 'SizeID', width: 250 },
                { field: 'FrameTypeID', headerName: 'FrameTypeID', width: 250 },
                { field: 'FrameSizeID', headerName: 'FrameSizeID', width: 250 },
                { field: 'MatID', headerName: 'MatID', width: 250 },
                { field: 'MaterialTypeID', headerName: 'MaterialTypeID', width: 250 },
                { field: 'MountingTypeID', headerName: 'MountingTypeID', width: 250 },
                { field: 'ItemCount', headerName: 'ItemCount', width: 250 },
                { field: 'PriceAmountPerItem', headerName: 'PriceAmountPerItem', width: 250 },
                { field: 'PriceCurrencyID', headerName: 'PriceCurrencyID', width: 250 },
                { field: 'Comments', headerName: 'Comments', width: 250 },
                { field: 'PrintingHouseID', headerName: 'PrintingHouseID', width: 250 },
                { field: 'IsDeleted', headerName: 'IsDeleted', width: 250 },
                { field: 'CreatedDate', headerName: 'CreatedDate', width: 250 },
                { field: 'CreatedByID', headerName: 'CreatedByID', width: 250 },
                { field: 'ModifiedDate', headerName: 'ModifiedDate', width: 250 },
                { field: 'ModifiedByID', headerName: 'ModifiedByID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.orderitems);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                OrderID: cs[c].OrderID ? this.state.orders[ cs[c].OrderID ].Name : "",
                ImageID: cs[c].ImageID ? this.state.images[ cs[c].ImageID ].Name : "",
                Width: cs[c].Width,
                Height: cs[c].Height,
                SizeID: cs[c].SizeID ? this.state.sizes[ cs[c].SizeID ].Name : "",
                FrameTypeID: cs[c].FrameTypeID ? this.state.frametypes[ cs[c].FrameTypeID ].Name : "",
                FrameSizeID: cs[c].FrameSizeID ? this.state.sizes[ cs[c].FrameSizeID ].Name : "",
                MatID: cs[c].MatID ? this.state.mats[ cs[c].MatID ].Name : "",
                MaterialTypeID: cs[c].MaterialTypeID ? this.state.materialtypes[ cs[c].MaterialTypeID ].Name : "",
                MountingTypeID: cs[c].MountingTypeID ? this.state.mountingtypes[ cs[c].MountingTypeID ].Name : "",
                ItemCount: cs[c].ItemCount,
                PriceAmountPerItem: cs[c].PriceAmountPerItem,
                PriceCurrencyID: cs[c].PriceCurrencyID ? this.state.currencies[ cs[c].PriceCurrencyID ].Name : "",
                Comments: cs[c].Comments,
                PrintingHouseID: cs[c].PrintingHouseID ? this.state.printinghouses[ cs[c].PrintingHouseID ].Name : "",
                IsDeleted: cs[c].IsDeleted,
                CreatedDate: cs[c].CreatedDate,
                CreatedByID: cs[c].CreatedByID ? this.state.users[ cs[c].CreatedByID ].Name : "",
                ModifiedDate: cs[c].ModifiedDate,
                ModifiedByID: cs[c].ModifiedByID ? this.state.users[ cs[c].ModifiedByID ].Name : "",

            };

            records.push(r);
        }

        return records;
    }

    async _getOrders() {
        let updatedState = this.state;
        updatedState.orders = {};
        let dalOrders = new OrdersDal();
        let response = await dalOrders.getOrders();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.orders[response.data[s].ID] = response.data[s];             
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
    async _getSizes() {
        let updatedState = this.state;
        updatedState.sizes = {};
        let dalSizes = new SizesDal();
        let response = await dalSizes.getSizes();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.sizes[response.data[s].ID] = response.data[s];             
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
    async _getFrameTypes() {
        let updatedState = this.state;
        updatedState.frametypes = {};
        let dalFrameTypes = new FrameTypesDal();
        let response = await dalFrameTypes.getFrameTypes();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.frametypes[response.data[s].ID] = response.data[s];             
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
    async _getMats() {
        let updatedState = this.state;
        updatedState.mats = {};
        let dalMats = new MatsDal();
        let response = await dalMats.getMats();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.mats[response.data[s].ID] = response.data[s];             
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
    async _getMaterialTypes() {
        let updatedState = this.state;
        updatedState.materialtypes = {};
        let dalMaterialTypes = new MaterialTypesDal();
        let response = await dalMaterialTypes.getMaterialTypes();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.materialtypes[response.data[s].ID] = response.data[s];             
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
    

    async _getOrderItems() {
        let updatedState = this.state;
        updatedState.orderitems = {};
        let dalOrderItems = new OrderItemsDal();
        let response = await dalOrderItems.getOrderItems();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.orderitems[response.data[s].ID] = response.data[s];             
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

export default withRouter(OrderItemsPage);