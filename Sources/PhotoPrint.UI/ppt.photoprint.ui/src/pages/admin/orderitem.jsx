


import React from 'react';
import { Link, withRouter  } from 'react-router-dom'
import { TextField } from '@material-ui/core';
import { Button } from '@material-ui/core';
import Alert from '@material-ui/lab/Alert';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import FormControl from '@material-ui/core/FormControl';
import Checkbox from '@material-ui/core/Checkbox';

import constants from '../../constants';

import PageHelper from "../../helpers/PageHelper";
import OrderItemsDal from '../../dal/OrderItemsDal';


import OrdersDal from '../../dal/OrdersDal';

import ImagesDal from '../../dal/ImagesDal';


import SizesDal from '../../dal/SizesDal';

import FrameTypesDal from '../../dal/FrameTypesDal';


import MatsDal from '../../dal/MatsDal';


import MaterialTypesDal from '../../dal/MaterialTypesDal';


import MountingTypesDal from '../../dal/MountingTypesDal';

import CurrenciesDal from '../../dal/CurrenciesDal';

import PrintingHousesDal from '../../dal/PrintingHousesDal';

import UsersDal from '../../dal/UsersDal';
import { OrderItemDto } from 'ppt.photoprint.dto';


class OrderItemPage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let paramOperation = this.props.match.params.operation;
        let paramId = this.props.match.params.id;
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            operation:  paramOperation,
            id:         paramId ? parseInt(paramId) : null,
            canEdit:    paramOperation ? ( paramOperation.toLowerCase() == 'new' || 
                                        paramOperation.toLowerCase() == 'edit' ? true : false) : false,
            orderitem: this._createEmptyOrderItemObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}orderitems`,
            urlThis: `${rooPath}orderitem/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onOrderIDChanged = this.onOrderIDChanged.bind(this);
        this.onImageIDChanged = this.onImageIDChanged.bind(this);
        this.onWidthChanged = this.onWidthChanged.bind(this);
        this.onHeightChanged = this.onHeightChanged.bind(this);
        this.onSizeIDChanged = this.onSizeIDChanged.bind(this);
        this.onFrameTypeIDChanged = this.onFrameTypeIDChanged.bind(this);
        this.onFrameSizeIDChanged = this.onFrameSizeIDChanged.bind(this);
        this.onMatIDChanged = this.onMatIDChanged.bind(this);
        this.onMaterialTypeIDChanged = this.onMaterialTypeIDChanged.bind(this);
        this.onMountingTypeIDChanged = this.onMountingTypeIDChanged.bind(this);
        this.onItemCountChanged = this.onItemCountChanged.bind(this);
        this.onPriceAmountPerItemChanged = this.onPriceAmountPerItemChanged.bind(this);
        this.onPriceCurrencyIDChanged = this.onPriceCurrencyIDChanged.bind(this);
        this.onCommentsChanged = this.onCommentsChanged.bind(this);
        this.onPrintingHouseIDChanged = this.onPrintingHouseIDChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this.onCreatedDateChanged = this.onCreatedDateChanged.bind(this);
        this.onCreatedByIDChanged = this.onCreatedByIDChanged.bind(this);
        this.onModifiedDateChanged = this.onModifiedDateChanged.bind(this);
        this.onModifiedByIDChanged = this.onModifiedByIDChanged.bind(this);
        this._getOrderItem = this._getOrderItem.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onOrderIDChanged = this.onOrderIDChanged.bind(this);
        this.onImageIDChanged = this.onImageIDChanged.bind(this);
        this.onWidthChanged = this.onWidthChanged.bind(this);
        this.onHeightChanged = this.onHeightChanged.bind(this);
        this.onSizeIDChanged = this.onSizeIDChanged.bind(this);
        this.onFrameTypeIDChanged = this.onFrameTypeIDChanged.bind(this);
        this.onFrameSizeIDChanged = this.onFrameSizeIDChanged.bind(this);
        this.onMatIDChanged = this.onMatIDChanged.bind(this);
        this.onMaterialTypeIDChanged = this.onMaterialTypeIDChanged.bind(this);
        this.onMountingTypeIDChanged = this.onMountingTypeIDChanged.bind(this);
        this.onItemCountChanged = this.onItemCountChanged.bind(this);
        this.onPriceAmountPerItemChanged = this.onPriceAmountPerItemChanged.bind(this);
        this.onPriceCurrencyIDChanged = this.onPriceCurrencyIDChanged.bind(this);
        this.onCommentsChanged = this.onCommentsChanged.bind(this);
        this.onPrintingHouseIDChanged = this.onPrintingHouseIDChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this.onCreatedDateChanged = this.onCreatedDateChanged.bind(this);
        this.onCreatedByIDChanged = this.onCreatedByIDChanged.bind(this);
        this.onModifiedDateChanged = this.onModifiedDateChanged.bind(this);
        this.onModifiedByIDChanged = this.onModifiedByIDChanged.bind(this);


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
			obj._getOrderItem().then( () => {} );
			});});});});});});});});});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onOrderIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.OrderID = newVal;

        this.setState(updatedState);
    }

    onImageIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.ImageID = newVal;

        this.setState(updatedState);
    }

    onWidthChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.Width = newVal;

        this.setState(updatedState);
    }

    onHeightChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.Height = newVal;

        this.setState(updatedState);
    }

    onSizeIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.SizeID = newVal;

        this.setState(updatedState);
    }

    onFrameTypeIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.FrameTypeID = newVal;

        this.setState(updatedState);
    }

    onFrameSizeIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.FrameSizeID = newVal;

        this.setState(updatedState);
    }

    onMatIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.MatID = newVal;

        this.setState(updatedState);
    }

    onMaterialTypeIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.MaterialTypeID = newVal;

        this.setState(updatedState);
    }

    onMountingTypeIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.MountingTypeID = newVal;

        this.setState(updatedState);
    }

    onItemCountChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.ItemCount = newVal;

        this.setState(updatedState);
    }

    onPriceAmountPerItemChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseFloat(event.target.value);
        updatedState.orderitem.PriceAmountPerItem = newVal;

        this.setState(updatedState);
    }

    onPriceCurrencyIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.PriceCurrencyID = newVal;

        this.setState(updatedState);
    }

    onCommentsChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.orderitem.Comments = newVal;

        this.setState(updatedState);
    }

    onPrintingHouseIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.PrintingHouseID = newVal;

        this.setState(updatedState);
    }

    onIsDeletedChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.checked;
        updatedState.orderitem.IsDeleted = newVal;

        this.setState(updatedState);
    }

    onCreatedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.orderitem.CreatedDate = newVal;

        this.setState(updatedState);
    }

    onCreatedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.CreatedByID = newVal;

        this.setState(updatedState);
    }

    onModifiedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.orderitem.ModifiedDate = newVal;

        this.setState(updatedState);
    }

    onModifiedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderitem.ModifiedByID = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving OrderItem: ", this.state.orderitem);
        
        if(this._validateForm()) {
            const reqOrderItem = new OrderItemDto();
            reqOrderItem.ID = this.state.id;
            reqOrderItem.OrderID = this.state.orderitem.OrderID;
            reqOrderItem.ImageID = this.state.orderitem.ImageID;
            reqOrderItem.Width = this.state.orderitem.Width;
            reqOrderItem.Height = this.state.orderitem.Height;
            reqOrderItem.SizeID = this.state.orderitem.SizeID;
            reqOrderItem.FrameTypeID = this.state.orderitem.FrameTypeID;
            reqOrderItem.FrameSizeID = this.state.orderitem.FrameSizeID;
            reqOrderItem.MatID = this.state.orderitem.MatID;
            reqOrderItem.MaterialTypeID = this.state.orderitem.MaterialTypeID;
            reqOrderItem.MountingTypeID = this.state.orderitem.MountingTypeID;
            reqOrderItem.ItemCount = this.state.orderitem.ItemCount;
            reqOrderItem.PriceAmountPerItem = this.state.orderitem.PriceAmountPerItem;
            reqOrderItem.PriceCurrencyID = this.state.orderitem.PriceCurrencyID;
            reqOrderItem.Comments = this.state.orderitem.Comments;
            reqOrderItem.PrintingHouseID = this.state.orderitem.PrintingHouseID;
            reqOrderItem.IsDeleted = this.state.orderitem.IsDeleted;
            reqOrderItem.CreatedDate = this.state.orderitem.CreatedDate;
            reqOrderItem.CreatedByID = this.state.orderitem.CreatedByID;
            reqOrderItem.ModifiedDate = this.state.orderitem.ModifiedDate;
            reqOrderItem.ModifiedByID = this.state.orderitem.ModifiedByID;

            console.log("Saving OrderItem: ", reqOrderItem); 
        
            let dalOrderItems = new OrderItemsDal();

            let obj = this;

            function upsertOrderItemThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `OrderItem was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `OrderItem was updated`;                
                    }

                    obj.setState(updatedState);
                }
                else {
                    obj._showError(updatedState, response); 
                
                    obj.setState(updatedState);
                }
            }  

            function upsertCatch(err) {
                const updatedState = obj.state;
                const errMsg = `Error: ${err}`
                updatedState.showSuccess = false;
                updatedState.showError = true;
                updatedState.error = errMsg; 
                obj.setState(updatedState);
            }

            if(this.state.id != null) {
                dalOrderItems.updateOrderItem(reqOrderItem)
                                        .then( (res) => { upsertOrderItemThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalOrderItems.insertOrderItem(reqOrderItem)
                                        .then( (res) => { upsertOrderItemThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });        
            }

        }
        
    }

    onDeleteClicked() {
        const updatedState = this.state;
        updatedState.showDeleteConfirm = true;
        this.setState(updatedState);
    }

    onDeleteCancel() {
        const updatedState = this.state;
        updatedState.showDeleteConfirm = false;
        this.setState(updatedState);
    }

    onDeleteConfirm() {  
        
        let dalOrderItems = new OrderItemsDal();
        let obj = this;

        dalOrderItems.deleteOrderItem(this.state.id).then( (response) => {
            if(response.status == constants.HTTP_OK) {
                obj.props.history.push(this.state.urlEntities);                
            }
            else {
                const updatedState = obj.state;
                updatedState.showDeleteConfirm = false;
                obj._showError(updatedState, response);                
                obj.setState(updatedState);               
            }
        })
    }

    render() {

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        const styleSuccess = {
            display: this.state.showSuccess ? "block" : "none"
        }   
        
        const styleDeleteBtn = {
            display: this.state.id ? "block" : "none"
        }

        const lstOrderIDsFields = ["ID"];
        const lstOrderIDs = this._prepareOptionsList( this.state.orders 
                                                                    ? Object.values(this.state.orders) : null, 
                                                                    lstOrderIDsFields,
                                                                    false );
        const lstImageIDsFields = ["Title"];
        const lstImageIDs = this._prepareOptionsList( this.state.images 
                                                                    ? Object.values(this.state.images) : null, 
                                                                    lstImageIDsFields,
                                                                    false );
        const lstSizeIDsFields = ["SizeName"];
        const lstSizeIDs = this._prepareOptionsList( this.state.sizes 
                                                                    ? Object.values(this.state.sizes) : null, 
                                                                    lstSizeIDsFields,
                                                                    true );
        const lstFrameTypeIDsFields = ["FrameTypeName"];
        const lstFrameTypeIDs = this._prepareOptionsList( this.state.frametypes 
                                                                    ? Object.values(this.state.frametypes) : null, 
                                                                    lstFrameTypeIDsFields,
                                                                    false );
        const lstFrameSizeIDsFields = ["SizeName"];
        const lstFrameSizeIDs = this._prepareOptionsList( this.state.sizes 
                                                                    ? Object.values(this.state.sizes) : null, 
                                                                    lstFrameSizeIDsFields,
                                                                    true );
        const lstMatIDsFields = ["MatName"];
        const lstMatIDs = this._prepareOptionsList( this.state.mats 
                                                                    ? Object.values(this.state.mats) : null, 
                                                                    lstMatIDsFields,
                                                                    false );
        const lstMaterialTypeIDsFields = ["MaterialTypeName"];
        const lstMaterialTypeIDs = this._prepareOptionsList( this.state.materialtypes 
                                                                    ? Object.values(this.state.materialtypes) : null, 
                                                                    lstMaterialTypeIDsFields,
                                                                    false );
        const lstMountingTypeIDsFields = ["MountingTypeName"];
        const lstMountingTypeIDs = this._prepareOptionsList( this.state.mountingtypes 
                                                                    ? Object.values(this.state.mountingtypes) : null, 
                                                                    lstMountingTypeIDsFields,
                                                                    false );
        const lstPriceCurrencyIDsFields = ["ISO"];
        const lstPriceCurrencyIDs = this._prepareOptionsList( this.state.currencies 
                                                                    ? Object.values(this.state.currencies) : null, 
                                                                    lstPriceCurrencyIDsFields,
                                                                    false );
        const lstPrintingHouseIDsFields = ["Name"];
        const lstPrintingHouseIDs = this._prepareOptionsList( this.state.printinghouses 
                                                                    ? Object.values(this.state.printinghouses) : null, 
                                                                    lstPrintingHouseIDsFields,
                                                                    true );
        const lstCreatedByIDsFields = ["FirstName", "LastName"];
        const lstCreatedByIDs = this._prepareOptionsList( this.state.users 
                                                                    ? Object.values(this.state.users) : null, 
                                                                    lstCreatedByIDsFields,
                                                                    false );
        const lstModifiedByIDsFields = ["FirstName", "LastName"];
        const lstModifiedByIDs = this._prepareOptionsList( this.state.users 
                                                                    ? Object.values(this.state.users) : null, 
                                                                    lstModifiedByIDsFields,
                                                                    true );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>OrderItem: { this.state.orderitem.ID }</h2>
                            </td>
                            <td>
                                <Button variant="contained" color="primary"
                                        onClick={ () => this.onSaveClicked() }>Save</Button>

                                <Button variant="contained" color="secondary"
                                        style={styleDeleteBtn}
                                        onClick={ () => this.onDeleteClicked() }>Delete</Button>

                                <Button variant="contained" component={Link} to={this.state.urlEntities}>Cancel</Button>
                            </td>
                        </tr>
                        <tr>
                            <td colSpan={2}>
                                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                                <Alert severity="success" style={styleSuccess}>Success! {this.state.success}</Alert>                                
                            </td>
                        </tr> 
    
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbOrderID" 
                                            fullWidth
                                            select 
                                            label="OrderID" 
                                            value={ (this.state.orderitem && this.state.orderitem.OrderID) ? 
                                                        this.state.orderitem.OrderID : '-1' }
                                                        onChange={ (event) => this.onOrderIDChanged(event) }>
                                        {
                                            lstOrderIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbImageID" 
                                            fullWidth
                                            select 
                                            label="ImageID" 
                                            value={ (this.state.orderitem && this.state.orderitem.ImageID) ? 
                                                        this.state.orderitem.ImageID : '-1' }
                                                        onChange={ (event) => this.onImageIDChanged(event) }>
                                        {
                                            lstImageIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Width" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Width" 
                                            value={this.state.orderitem.Width}
                                            onChange={ (event) => { this.onWidthChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Height" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Height" 
                                            value={this.state.orderitem.Height}
                                            onChange={ (event) => { this.onHeightChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbSizeID" 
                                            fullWidth
                                            select 
                                            label="SizeID" 
                                            value={ (this.state.orderitem && this.state.orderitem.SizeID) ? 
                                                        this.state.orderitem.SizeID : '-1' }
                                                        onChange={ (event) => this.onSizeIDChanged(event) }>
                                        {
                                            lstSizeIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbFrameTypeID" 
                                            fullWidth
                                            select 
                                            label="FrameTypeID" 
                                            value={ (this.state.orderitem && this.state.orderitem.FrameTypeID) ? 
                                                        this.state.orderitem.FrameTypeID : '-1' }
                                                        onChange={ (event) => this.onFrameTypeIDChanged(event) }>
                                        {
                                            lstFrameTypeIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbFrameSizeID" 
                                            fullWidth
                                            select 
                                            label="FrameSizeID" 
                                            value={ (this.state.orderitem && this.state.orderitem.FrameSizeID) ? 
                                                        this.state.orderitem.FrameSizeID : '-1' }
                                                        onChange={ (event) => this.onFrameSizeIDChanged(event) }>
                                        {
                                            lstFrameSizeIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbMatID" 
                                            fullWidth
                                            select 
                                            label="MatID" 
                                            value={ (this.state.orderitem && this.state.orderitem.MatID) ? 
                                                        this.state.orderitem.MatID : '-1' }
                                                        onChange={ (event) => this.onMatIDChanged(event) }>
                                        {
                                            lstMatIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbMaterialTypeID" 
                                            fullWidth
                                            select 
                                            label="MaterialTypeID" 
                                            value={ (this.state.orderitem && this.state.orderitem.MaterialTypeID) ? 
                                                        this.state.orderitem.MaterialTypeID : '-1' }
                                                        onChange={ (event) => this.onMaterialTypeIDChanged(event) }>
                                        {
                                            lstMaterialTypeIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbMountingTypeID" 
                                            fullWidth
                                            select 
                                            label="MountingTypeID" 
                                            value={ (this.state.orderitem && this.state.orderitem.MountingTypeID) ? 
                                                        this.state.orderitem.MountingTypeID : '-1' }
                                                        onChange={ (event) => this.onMountingTypeIDChanged(event) }>
                                        {
                                            lstMountingTypeIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="ItemCount" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="ItemCount" 
                                            value={this.state.orderitem.ItemCount}
                                            onChange={ (event) => { this.onItemCountChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="PriceAmountPerItem" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="PriceAmountPerItem" 
                                            value={this.state.orderitem.PriceAmountPerItem}
                                            onChange={ (event) => { this.onPriceAmountPerItemChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbPriceCurrencyID" 
                                            fullWidth
                                            select 
                                            label="PriceCurrencyID" 
                                            value={ (this.state.orderitem && this.state.orderitem.PriceCurrencyID) ? 
                                                        this.state.orderitem.PriceCurrencyID : '-1' }
                                                        onChange={ (event) => this.onPriceCurrencyIDChanged(event) }>
                                        {
                                            lstPriceCurrencyIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Comments" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Comments" 
                                            value={this.state.orderitem.Comments}
                                            onChange={ (event) => { this.onCommentsChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbPrintingHouseID" 
                                            fullWidth
                                            select 
                                            label="PrintingHouseID" 
                                            value={ (this.state.orderitem && this.state.orderitem.PrintingHouseID) ? 
                                                        this.state.orderitem.PrintingHouseID : '-1' }
                                                        onChange={ (event) => this.onPrintingHouseIDChanged(event) }>
                                        {
                                            lstPrintingHouseIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <FormControlLabel
                                    key="lblIsDeleted"                        
                                    control = {
                                        <Checkbox   checked={ this.state.orderitem.IsDeleted ? true : false } 
                                                    onChange={(event) => this.onIsDeletedChanged(event)} 
                                                    name="IsDeleted" />
                                        }
                                    label="IsDeleted"
                                />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="CreatedDate" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="CreatedDate" 
                                            value={this.state.orderitem.CreatedDate}
                                            onChange={ (event) => { this.onCreatedDateChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbCreatedByID" 
                                            fullWidth
                                            select 
                                            label="CreatedByID" 
                                            value={ (this.state.orderitem && this.state.orderitem.CreatedByID) ? 
                                                        this.state.orderitem.CreatedByID : '-1' }
                                                        onChange={ (event) => this.onCreatedByIDChanged(event) }>
                                        {
                                            lstCreatedByIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="ModifiedDate" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="ModifiedDate" 
                                            value={this.state.orderitem.ModifiedDate}
                                            onChange={ (event) => { this.onModifiedDateChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbModifiedByID" 
                                            fullWidth
                                            select 
                                            label="ModifiedByID" 
                                            value={ (this.state.orderitem && this.state.orderitem.ModifiedByID) ? 
                                                        this.state.orderitem.ModifiedByID : '-1' }
                                                        onChange={ (event) => this.onModifiedByIDChanged(event) }>
                                        {
                                            lstModifiedByIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete OrderItem</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this OrderItem?
                    </DialogContentText>                    
                    </DialogContent>
                    <DialogActions>
                    <Button onClick={() => { this.onDeleteCancel() }} color="primary">
                        Cancel
                    </Button>
                    <Button onClick={() => { this.onDeleteConfirm() }} color="primary">
                        Delete
                    </Button>
                    </DialogActions>
                </Dialog>
            </div>

        );
    }

    _createEmptyOrderItemObj() {
        let orderitem = new OrderItemDto();

        return orderitem;
    }

    async _getOrderItem()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalOrderItems = new OrderItemsDal();
            let response = await dalOrderItems.getOrderItem(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.orderitem = response.data;                
            }
            else if(response.status == constants.HTTP_Unauthorized)
            {
                this._redirectToLogin();
            }
            else 
            {
                this._showError(updatedState, response);
            }
        
            this.setState(updatedState);    
        }
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

    

    _validateForm() {
        let updatedState = this.state;
        let isValid = true;
        
        // TODO: add validation here if needed

        if(isValid) {
            updatedState.showError = false;
        }
        
        this.setState(updatedState);
        
        return isValid;
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

    _prepareOptionsList(objs, fields, hasEmptyVal) 
    {
        var lst = [];
        
        if(hasEmptyVal) {
            lst.push( <option key='-1' value='-1'>[Empty]</option> );
        }

        if(objs) {
            
            lst.push(
                objs.map( (i) => {
                    let optionText = "";
                    for(let f in fields) {
                        optionText += i[fields[f]] + (f + 1 < fields.length ? " " : "");
                    }

                    return(
                        <option key={i.ID} value={i.ID}>
                            { optionText }
                        </option>
                    )
                })
            )
        }

        return lst;
    }
}

export default withRouter(OrderItemPage);