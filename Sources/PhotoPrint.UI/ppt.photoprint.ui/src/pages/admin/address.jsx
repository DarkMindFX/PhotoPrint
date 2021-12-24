

import constants from '../../constants';
import PageHelper from "../../helpers/PageHelper";
import UsersDal from '../../dal/UsersDal';
import CitiesDal from '../../dal/CitiesDal';
import AddressesDal from '../../dal/AddressesDal';
import  AddressTypesDal from '../../dal/AddressTypesDal';

import { AddressDto } from 'ppt.photoprint.dto';

import React from 'react';
import { Link, withRouter  } from 'react-router-dom';
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

class AddressPage extends React.Component {

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
            address: this._createEmptyAddressObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}addresses`,
            urlThis: `${rooPath}address/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onAddressTypeIDChanged = this.onAddressTypeIDChanged.bind(this);
        this.onTitleChanged = this.onTitleChanged.bind(this);
        this.onCityIDChanged = this.onCityIDChanged.bind(this);
        this.onStreetChanged = this.onStreetChanged.bind(this);
        this.onBuildingNoChanged = this.onBuildingNoChanged.bind(this);
        this.onApartmentNoChanged = this.onApartmentNoChanged.bind(this);
        this.onCommentChanged = this.onCommentChanged.bind(this);
        this.onCreatedByIDChanged = this.onCreatedByIDChanged.bind(this);
        this.onCreatedDateChanged = this.onCreatedDateChanged.bind(this);
        this.onModifiedByIDChanged = this.onModifiedByIDChanged.bind(this);
        this.onModifiedDateChanged = this.onModifiedDateChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this._getAddress = this._getAddress.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onAddressTypeIDChanged = this.onAddressTypeIDChanged.bind(this);
        this.onTitleChanged = this.onTitleChanged.bind(this);
        this.onCityIDChanged = this.onCityIDChanged.bind(this);
        this.onStreetChanged = this.onStreetChanged.bind(this);
        this.onBuildingNoChanged = this.onBuildingNoChanged.bind(this);
        this.onApartmentNoChanged = this.onApartmentNoChanged.bind(this);
        this.onCommentChanged = this.onCommentChanged.bind(this);
        this.onCreatedByIDChanged = this.onCreatedByIDChanged.bind(this);
        this.onCreatedDateChanged = this.onCreatedDateChanged.bind(this);
        this.onModifiedByIDChanged = this.onModifiedByIDChanged.bind(this);
        this.onModifiedDateChanged = this.onModifiedDateChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getAddressTypes().then( () => {
			obj._getCities().then( () => {
			obj._getUsers().then( () => {
			obj._getAddress().then( () => {} );
			});});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onAddressTypeIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.address.AddressTypeID = newVal;

        this.setState(updatedState);
    }

    onTitleChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.address.Title = newVal;

        this.setState(updatedState);
    }

    onCityIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.address.CityID = newVal;

        this.setState(updatedState);
    }

    onStreetChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.address.Street = newVal;

        this.setState(updatedState);
    }

    onBuildingNoChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.address.BuildingNo = newVal;

        this.setState(updatedState);
    }

    onApartmentNoChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.address.ApartmentNo = newVal;

        this.setState(updatedState);
    }

    onCommentChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.address.Comment = newVal;

        this.setState(updatedState);
    }

    onCreatedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.address.CreatedByID = newVal;

        this.setState(updatedState);
    }

    onCreatedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.address.CreatedDate = newVal;

        this.setState(updatedState);
    }

    onModifiedByIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.address.ModifiedByID = newVal;

        this.setState(updatedState);
    }

    onModifiedDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.address.ModifiedDate = newVal;

        this.setState(updatedState);
    }

    onIsDeletedChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.checked;
        updatedState.address.IsDeleted = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving Address: ", this.state.address);
        
        if(this._validateForm()) {
            const reqAddress = new AddressDto();
            reqAddress.ID = this.state.id;
            reqAddress.AddressTypeID = this.state.address.AddressTypeID;
            reqAddress.Title = this.state.address.Title;
            reqAddress.CityID = this.state.address.CityID;
            reqAddress.Street = this.state.address.Street;
            reqAddress.BuildingNo = this.state.address.BuildingNo;
            reqAddress.ApartmentNo = this.state.address.ApartmentNo;
            reqAddress.Comment = this.state.address.Comment;
            reqAddress.CreatedByID = this.state.address.CreatedByID;
            reqAddress.CreatedDate = this.state.address.CreatedDate;
            reqAddress.ModifiedByID = this.state.address.ModifiedByID;
            reqAddress.ModifiedDate = this.state.address.ModifiedDate;
            reqAddress.IsDeleted = this.state.address.IsDeleted;

            console.log("Saving Address: ", reqAddress); 
        
            let dalAddresses = new AddressesDal();

            let obj = this;

            function upsertAddressThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `Address was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `Address was updated`;                
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
                dalAddresses.updateAddress(reqAddress)
                                        .then( (res) => { upsertAddressThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalAddresses.insertAddress(reqAddress)
                                        .then( (res) => { upsertAddressThen(res); } )
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
        
        let dalAddresses = new AddressesDal();
        let obj = this;

        dalAddresses.deleteAddress(this.state.id).then( (response) => {
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

        const lstAddressTypeIDsFields = ["AddressTypeName"];
        const lstAddressTypeIDs = this._prepareOptionsList( this.state.addresstypes 
                                                                    ? Object.values(this.state.addresstypes) : null, 
                                                                    lstAddressTypeIDsFields,
                                                                    false );
        const lstCityIDsFields = ["CityName"];
        const lstCityIDs = this._prepareOptionsList( this.state.cities 
                                                                    ? Object.values(this.state.cities) : null, 
                                                                    lstCityIDsFields,
                                                                    false );
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
                                <h2>Address: { this.state.address.Title }</h2>
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
                                <TextField  key="cbAddressTypeID" 
                                            fullWidth
                                            select 
                                            label="AddressTypeID" 
                                            value={ (this.state.address && this.state.address.AddressTypeID) ? 
                                                        this.state.address.AddressTypeID : '-1' }
                                                        onChange={ (event) => this.onAddressTypeIDChanged(event) }>
                                        {
                                            lstAddressTypeIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Title" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Title" 
                                            value={this.state.address.Title}
                                            onChange={ (event) => { this.onTitleChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbCityID" 
                                            fullWidth
                                            select 
                                            label="CityID" 
                                            value={ (this.state.address && this.state.address.CityID) ? 
                                                        this.state.address.CityID : '-1' }
                                                        onChange={ (event) => this.onCityIDChanged(event) }>
                                        {
                                            lstCityIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Street" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Street" 
                                            value={this.state.address.Street}
                                            onChange={ (event) => { this.onStreetChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="BuildingNo" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="BuildingNo" 
                                            value={this.state.address.BuildingNo}
                                            onChange={ (event) => { this.onBuildingNoChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="ApartmentNo" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="ApartmentNo" 
                                            value={this.state.address.ApartmentNo}
                                            onChange={ (event) => { this.onApartmentNoChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Comment" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Comment" 
                                            value={this.state.address.Comment}
                                            onChange={ (event) => { this.onCommentChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbCreatedByID" 
                                            fullWidth
                                            select 
                                            label="CreatedByID" 
                                            value={ (this.state.address && this.state.address.CreatedByID) ? 
                                                        this.state.address.CreatedByID : '-1' }
                                                        onChange={ (event) => this.onCreatedByIDChanged(event) }>
                                        {
                                            lstCreatedByIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="CreatedDate" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="CreatedDate" 
                                            value={this.state.address.CreatedDate}
                                            onChange={ (event) => { this.onCreatedDateChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbModifiedByID" 
                                            fullWidth
                                            select 
                                            label="ModifiedByID" 
                                            value={ (this.state.address && this.state.address.ModifiedByID) ? 
                                                        this.state.address.ModifiedByID : '-1' }
                                                        onChange={ (event) => this.onModifiedByIDChanged(event) }>
                                        {
                                            lstModifiedByIDs 
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
                                            value={this.state.address.ModifiedDate}
                                            onChange={ (event) => { this.onModifiedDateChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <FormControlLabel
                                    key="lblIsDeleted"                        
                                    control = {
                                        <Checkbox   checked={ this.state.address.IsDeleted ? true : false } 
                                                    onChange={(event) => this.onIsDeletedChanged(event)} 
                                                    name="IsDeleted" />
                                        }
                                    label="IsDeleted"
                                />
                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete Address</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this Address?
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

    _createEmptyAddressObj() {
        let address = new AddressDto();

        return address;
    }

    async _getAddress()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalAddresses = new AddressesDal();
            let response = await dalAddresses.getAddress(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.address = response.data;                
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

    async _getAddressTypes() {
        let updatedState = this.state;
        updatedState.addresstypes = {};
        let dalAddressTypes = new AddressTypesDal();
        let response = await dalAddressTypes.getAddressTypes();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.addresstypes[response.data[s].ID] = response.data[s];             
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

export default withRouter(AddressPage);