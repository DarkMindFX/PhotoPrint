


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

const constants = require('../../constants');
const { v4: uuidv4 } = require('uuid');
const PageHelper = require("../../helpers/PageHelper");
const CitiesDal = require('../../dal/CitiesDal');

const RegionsDal = require('../../dal/RegionsDal');
const { CityDto } = require('ppt.photoprint.dto')


class CityPage extends React.Component {

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
            city: this._createEmptyCityObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}cities`,
            urlThis: `${rooPath}city/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onCityNameChanged = this.onCityNameChanged.bind(this);
        this.onRegionIDChanged = this.onRegionIDChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this._getCity = this._getCity.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onCityNameChanged = this.onCityNameChanged.bind(this);
        this.onRegionIDChanged = this.onRegionIDChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getRegions().then( () => {
			obj._getCity().then( () => {} );
			});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onCityNameChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.city.CityName = newVal;

        this.setState(updatedState);
    }

    onRegionIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.city.RegionID = newVal;

        this.setState(updatedState);
    }

    onIsDeletedChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.city.IsDeleted = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving City: ", this.state.city);
        
        if(this._validateForm()) {
            const reqCity = new CityDto();
            reqCity.ID = this.state.id;
            reqCity.CityName = this.state.city.CityName;
            reqCity.RegionID = this.state.city.RegionID;
            reqCity.IsDeleted = this.state.city.IsDeleted;

            console.log("Saving City: ", reqCity); 
        
            let dalCities = new CitiesDal();

            let obj = this;

            function upsertCityThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `City was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `City was updated`;                
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
                dalCities.updateCity(reqCity)
                                        .then( (res) => { upsertCityThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalCities.insertCity(reqCity)
                                        .then( (res) => { upsertCityThen(res); } )
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
        
        let dalCities = new CitiesDal();
        let obj = this;

        dalCities.deleteCity(this.state.id).then( (response) => {
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

        const lstRegionIDsFields = ["Name"];
        const lstRegionIDs = this._prepareOptionsList( this.state.regions 
                                                                    ? Object.values(this.state.regions) : null, 
                                                                    lstRegionIDsFields,
                                                                    false );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>City: { this.state.city.toString() }</h2>
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
                                <TextField  id="CityName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="CityName" 
                                            value={this.state.city.CityName}
                                            onChange={ (event) => { this.onCityNameChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbRegionID" 
                                            fullWidth
                                            select 
                                            label="RegionID" 
                                            value={ (this.state.city && this.state.city.RegionID) ? 
                                                        this.state.city.RegionID : '-1' }
                                                        onChange={ (event) => this.onRegionIDChanged(event) }>
                                        {
                                            lstRegionIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="IsDeleted" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="IsDeleted" 
                                            value={this.state.city.IsDeleted}
                                            onChange={ (event) => { this.onIsDeletedChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete City</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this City?
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

    _createEmptyCityObj() {
        let city = new CityDto();

        return city;
    }

    async _getCity()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalCities = new CitiesDal();
            let response = await dalCities.getCity(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.city = response.data;                
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

    async _getRegions() {
        let updatedState = this.state;
        updatedState.regions = {};
        let dalRegions = new RegionsDal();
        let response = await dalRegions.getRegions();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.regions[response.data[s].ID] = response.data[s];             
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

export default withRouter(CityPage);