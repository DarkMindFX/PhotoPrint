


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
const RegionsDal = require('../../dal/RegionsDal');

const CountriesDal = require('../../dal/CountriesDal');
const { RegionDto } = require('ppt.photoprint.dto')


class RegionPage extends React.Component {

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
            region: this._createEmptyRegionObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}regions`,
            urlThis: `${rooPath}region/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onRegionNameChanged = this.onRegionNameChanged.bind(this);
        this.onCountryIDChanged = this.onCountryIDChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this._getRegion = this._getRegion.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onRegionNameChanged = this.onRegionNameChanged.bind(this);
        this.onCountryIDChanged = this.onCountryIDChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getCountries().then( () => {
			obj._getRegion().then( () => {} );
			});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onRegionNameChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.region.RegionName = newVal;

        this.setState(updatedState);
    }

    onCountryIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.region.CountryID = newVal;

        this.setState(updatedState);
    }

    onIsDeletedChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.checked;
        updatedState.region.IsDeleted = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving Region: ", this.state.region);
        
        if(this._validateForm()) {
            const reqRegion = new RegionDto();
            reqRegion.ID = this.state.id;
            reqRegion.RegionName = this.state.region.RegionName;
            reqRegion.CountryID = this.state.region.CountryID;
            reqRegion.IsDeleted = this.state.region.IsDeleted;

            console.log("Saving Region: ", reqRegion); 
        
            let dalRegions = new RegionsDal();

            let obj = this;

            function upsertRegionThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `Region was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `Region was updated`;                
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
                dalRegions.updateRegion(reqRegion)
                                        .then( (res) => { upsertRegionThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalRegions.insertRegion(reqRegion)
                                        .then( (res) => { upsertRegionThen(res); } )
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
        
        let dalRegions = new RegionsDal();
        let obj = this;

        dalRegions.deleteRegion(this.state.id).then( (response) => {
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

        const lstCountryIDsFields = ["CountryName"];
        const lstCountryIDs = this._prepareOptionsList( this.state.countries 
                                                                    ? Object.values(this.state.countries) : null, 
                                                                    lstCountryIDsFields,
                                                                    false );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>Region: { this.state.region.RegionName }</h2>
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
                                <TextField  id="RegionName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="RegionName" 
                                            value={this.state.region.RegionName}
                                            onChange={ (event) => { this.onRegionNameChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbCountryID" 
                                            fullWidth
                                            select 
                                            label="CountryID" 
                                            value={ (this.state.region && this.state.region.CountryID) ? 
                                                        this.state.region.CountryID : '-1' }
                                                        onChange={ (event) => this.onCountryIDChanged(event) }>
                                        {
                                            lstCountryIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <FormControlLabel
                                    key="lblIsDeleted"                        
                                    control = {
                                        <Checkbox   checked={ this.state.region.IsDeleted ? true : false } 
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
                    <DialogTitle id="form-dialog-title">Delete Region</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this Region?
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

    _createEmptyRegionObj() {
        let region = new RegionDto();

        return region;
    }

    async _getRegion()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalRegions = new RegionsDal();
            let response = await dalRegions.getRegion(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.region = response.data;                
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

export default withRouter(RegionPage);