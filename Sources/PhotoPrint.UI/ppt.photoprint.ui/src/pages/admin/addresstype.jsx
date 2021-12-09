


const React = require('react');
const { Link, withRouter  } = require('react-router-dom');
const { TextField } = require('@material-ui/core');
const { Button } = require('@material-ui/core');
const Alert = require('@material-ui/lab/Alert');
const Dialog = require('@material-ui/core/Dialog');
const DialogActions = require('@material-ui/core/DialogActions');
const DialogContent = require('@material-ui/core/DialogContent');
const DialogContentText = require('@material-ui/core/DialogContentText');
const DialogTitle = require('@material-ui/core/DialogTitle');
const FormControlLabel = require('@material-ui/core/FormControlLabel');
const FormControl = require('@material-ui/core/FormControl');
const Checkbox = require('@material-ui/core/Checkbox');

const constants = require('../../constants');
const { v4: uuidv4 } = require('uuid');
const PageHelper = require("../../helpers/PageHelper");
const AddressTypesDal = require('../../dal/AddressTypesDal');
const { AddressTypeDto } = require('ppt.photoprint.dto')


class AddressTypePage extends React.Component {

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
            addresstype: this._createEmptyAddressTypeObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}addresstypes`,
            urlThis: `${rooPath}addresstype/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onAddressTypeNameChanged = this.onAddressTypeNameChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);
        this._getAddressType = this._getAddressType.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onAddressTypeNameChanged = this.onAddressTypeNameChanged.bind(this);
        this.onIsDeletedChanged = this.onIsDeletedChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getAddressType().then( () => {} );
			
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onAddressTypeNameChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.addresstype.AddressTypeName = newVal;

        this.setState(updatedState);
    }

    onIsDeletedChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.checked;
        updatedState.addresstype.IsDeleted = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving AddressType: ", this.state.addresstype);
        
        if(this._validateForm()) {
            const reqAddressType = new AddressTypeDto();
            reqAddressType.ID = this.state.id;
            reqAddressType.AddressTypeName = this.state.addresstype.AddressTypeName;
            reqAddressType.IsDeleted = this.state.addresstype.IsDeleted;

            console.log("Saving AddressType: ", reqAddressType); 
        
            let dalAddressTypes = new AddressTypesDal();

            let obj = this;

            function upsertAddressTypeThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `AddressType was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `AddressType was updated`;                
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
                dalAddressTypes.updateAddressType(reqAddressType)
                                        .then( (res) => { upsertAddressTypeThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalAddressTypes.insertAddressType(reqAddressType)
                                        .then( (res) => { upsertAddressTypeThen(res); } )
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
        
        let dalAddressTypes = new AddressTypesDal();
        let obj = this;

        dalAddressTypes.deleteAddressType(this.state.id).then( (response) => {
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

        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>AddressType: { this.state.addresstype.AddressTypeName }</h2>
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
                                <TextField  id="AddressTypeName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="AddressTypeName" 
                                            value={this.state.addresstype.AddressTypeName}
                                            onChange={ (event) => { this.onAddressTypeNameChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <FormControlLabel
                                    key="lblIsDeleted"                        
                                    control = {
                                        <Checkbox   checked={ this.state.addresstype.IsDeleted ? true : false } 
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
                    <DialogTitle id="form-dialog-title">Delete AddressType</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this AddressType?
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

    _createEmptyAddressTypeObj() {
        let addresstype = new AddressTypeDto();

        return addresstype;
    }

    async _getAddressType()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalAddressTypes = new AddressTypesDal();
            let response = await dalAddressTypes.getAddressType(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.addresstype = response.data;  
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

export default withRouter(AddressTypePage);