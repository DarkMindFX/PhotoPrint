


const React = require('react';
const { Link, withRouter  } = require('react-router-dom'
const { TextField } = require('@material-ui/core';
const { Button } = require('@material-ui/core';
const Alert = require('@material-ui/lab/Alert';
const Dialog = require('@material-ui/core/Dialog';
const DialogActions = require('@material-ui/core/DialogActions';
const DialogContent = require('@material-ui/core/DialogContent';
const DialogContentText = require('@material-ui/core/DialogContentText';
const DialogTitle = require('@material-ui/core/DialogTitle';
const FormControlLabel = require('@material-ui/core/FormControlLabel';
const FormControl = require('@material-ui/core/FormControl';
const Checkbox = require('@material-ui/core/Checkbox';

const constants = require('../../constants');
const { v4: uuidv4 } = require('uuid');
const PageHelper = require("../../helpers/PageHelper");
const DeliveryServiceCitiesDal = require('../../dal/DeliveryServiceCitiesDal');

const DeliveryServicesDal = require('../../dal/DeliveryServicesDal');

const CitiesDal = require('../../dal/CitiesDal');
const { DeliveryServiceCityDto } = require('ppt.photoprint.dto')


class DeliveryServiceCityPage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let paramOperation = this.props.match.params.operation;
        let paramDeliveryServiceId = this.props.match.params.deliveryserviceid;
        let paramCityId = this.props.match.params.cityid;
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            operation:  paramOperation,
            deliveryserviceid:         paramDeliveryServiceId ? parseInt(paramDeliveryServiceId) : null,
            cityid:         paramCityId ? parseInt(paramCityId) : null,
            canEdit:    paramOperation ? ( paramOperation.toLowerCase() == 'new' || 
                                        paramOperation.toLowerCase() == 'edit' ? true : false) : false,
            deliveryservicecity: this._createEmptyDeliveryServiceCityObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}deliveryservicecities`,
            urlThis: `${rooPath}deliveryservicecity/${paramOperation}` + (paramDeliveryServiceId ? `/${paramDeliveryServiceId}/${paramCityId}` : ``)
        };

        this.onDeliveryServiceIDChanged = this.onDeliveryServiceIDChanged.bind(this);
        this.onCityIDChanged = this.onCityIDChanged.bind(this);
        this._getDeliveryServiceCity = this._getDeliveryServiceCity.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onDeliveryServiceIDChanged = this.onDeliveryServiceIDChanged.bind(this);
        this.onCityIDChanged = this.onCityIDChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getDeliveryServices().then( () => {
			obj._getCities().then( () => {
			obj._getDeliveryServiceCity().then( () => {} );
			});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onDeliveryServiceIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.deliveryservicecity.DeliveryServiceID = newVal;

        this.setState(updatedState);
    }

    onCityIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.deliveryservicecity.CityID = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving DeliveryServiceCity: ", this.state.deliveryservicecity);
        
        if(this._validateForm()) {
            const reqDeliveryServiceCity = new DeliveryServiceCityDto();
            reqDeliveryServiceCity.DeliveryServiceID = this.state.deliveryservicecity.DeliveryServiceID;
            reqDeliveryServiceCity.CityID = this.state.deliveryservicecity.CityID;

            console.log("Saving DeliveryServiceCity: ", reqDeliveryServiceCity); 
        
            let dalDeliveryServiceCities = new DeliveryServiceCitiesDal();

            let obj = this;

            function upsertDeliveryServiceCityThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.deliveryserviceid = response.data.DelvieryServiceID;
                        updatedState.cityid = response.data.CityID;
                        updatedState.success = `DeliveryServiceCity was created.`;
                    }
                    else {
                        updatedState.success = `DeliveryServiceCity was updated`;                
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

            if(this.state.deliveryserviceid != null && this.state.cityid) {
                dalDeliveryServiceCities.updateDeliveryServiceCity(reqDeliveryServiceCity)
                                        .then( (res) => { upsertDeliveryServiceCityThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalDeliveryServiceCities.insertDeliveryServiceCity(reqDeliveryServiceCity)
                                        .then( (res) => { upsertDeliveryServiceCityThen(res); } )
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
        
        let dalDeliveryServiceCities = new DeliveryServiceCitiesDal();
        let obj = this;

        dalDeliveryServiceCities.deleteDeliveryServiceCity(this.state.deliveryserviceid, this.state.cityid).then( (response) => {
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
            display: this.state.deliveryserviceid && this.state.cityid ? "block" : "none"
        }

        const lstDeliveryServiceIDsFields = ["DeliveryServiceName"];
        const lstDeliveryServiceIDs = this._prepareOptionsList( this.state.deliveryservices 
                                                                    ? Object.values(this.state.deliveryservices) : null, 
                                                                    lstDeliveryServiceIDsFields,
                                                                    false );
        const lstCityIDsFields = ["CityName"];
        const lstCityIDs = this._prepareOptionsList( this.state.cities 
                                                                    ? Object.values(this.state.cities) : null, 
                                                                    lstCityIDsFields,
                                                                    false );

        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>DeliveryServiceCity: { 
                                    (this.state.deliveryservicecity.DeliveryServiceID ? this.state.deliveryservices[ this.state.deliveryservicecity.DeliveryServiceID ].DeliveryServiceName : "")
                                    + " to " +
                                    (this.state.deliveryservicecity.CityID ? this.state.cities[ this.state.deliveryservicecity.CityID ].CityName : "")
                                    }</h2>
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
                                <TextField  key="cbDeliveryServiceID" 
                                            fullWidth
                                            select 
                                            label="DeliveryServiceID" 
                                            value={ (this.state.deliveryservicecity && this.state.deliveryservicecity.DeliveryServiceID) ? 
                                                        this.state.deliveryservicecity.DeliveryServiceID : '-1' }
                                                        onChange={ (event) => this.onDeliveryServiceIDChanged(event) }>
                                        {
                                            lstDeliveryServiceIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbCityID" 
                                            fullWidth
                                            select 
                                            label="CityID" 
                                            value={ (this.state.deliveryservicecity && this.state.deliveryservicecity.CityID) ? 
                                                        this.state.deliveryservicecity.CityID : '-1' }
                                                        onChange={ (event) => this.onCityIDChanged(event) }>
                                        {
                                            lstCityIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete DeliveryServiceCity</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this DeliveryServiceCity?
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

    _createEmptyDeliveryServiceCityObj() {
        let deliveryservicecity = new DeliveryServiceCityDto();

        return deliveryservicecity;
    }

    async _getDeliveryServiceCity()
    {
        if(this.state.deliveryserviceid && this.state.cityid) {
            let updatedState = this.state;
                  
            let dalDeliveryServiceCities = new DeliveryServiceCitiesDal();
            let response = await dalDeliveryServiceCities.getDeliveryServiceCity(this.state.deliveryserviceid, this.state.cityid);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.deliveryservicecity = response.data;                
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

export default withRouter(DeliveryServiceCityPage);