


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
const UserConfirmationsDal = require('../../dal/UserConfirmationsDal');

const UsersDal = require('../../dal/UsersDal');
const { UserConfirmationDto } = require('ppt.photoprint.dto')


class UserConfirmationPage extends React.Component {

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
            userconfirmation: this._createEmptyUserConfirmationObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}userconfirmations`,
            urlThis: `${rooPath}userconfirmation/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onUserIDChanged = this.onUserIDChanged.bind(this);
        this.onConfirmationCodeChanged = this.onConfirmationCodeChanged.bind(this);
        this.onComfirmedChanged = this.onComfirmedChanged.bind(this);
        this.onExpiresDateChanged = this.onExpiresDateChanged.bind(this);
        this.onConfirmationDateChanged = this.onConfirmationDateChanged.bind(this);
        this._getUserConfirmation = this._getUserConfirmation.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onUserIDChanged = this.onUserIDChanged.bind(this);
        this.onConfirmationCodeChanged = this.onConfirmationCodeChanged.bind(this);
        this.onComfirmedChanged = this.onComfirmedChanged.bind(this);
        this.onExpiresDateChanged = this.onExpiresDateChanged.bind(this);
        this.onConfirmationDateChanged = this.onConfirmationDateChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getUsers().then( () => {
			obj._getUserConfirmation().then( () => {} );
			});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onUserIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.userconfirmation.UserID = newVal;

        this.setState(updatedState);
    }

    onConfirmationCodeChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.userconfirmation.ConfirmationCode = newVal;

        this.setState(updatedState);
    }

    onComfirmedChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.checked;
        updatedState.userconfirmation.Comfirmed = newVal;

        this.setState(updatedState);
    }

    onExpiresDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.userconfirmation.ExpiresDate = newVal;

        this.setState(updatedState);
    }

    onConfirmationDateChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.userconfirmation.ConfirmationDate = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving UserConfirmation: ", this.state.userconfirmation);
        
        if(this._validateForm()) {
            const reqUserConfirmation = new UserConfirmationDto();
            reqUserConfirmation.ID = this.state.id;
            reqUserConfirmation.UserID = this.state.userconfirmation.UserID;
            reqUserConfirmation.ConfirmationCode = this.state.userconfirmation.ConfirmationCode;
            reqUserConfirmation.Comfirmed = this.state.userconfirmation.Comfirmed;
            reqUserConfirmation.ExpiresDate = this.state.userconfirmation.ExpiresDate;
            reqUserConfirmation.ConfirmationDate = this.state.userconfirmation.ConfirmationDate;

            console.log("Saving UserConfirmation: ", reqUserConfirmation); 
        
            let dalUserConfirmations = new UserConfirmationsDal();

            let obj = this;

            function upsertUserConfirmationThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `UserConfirmation was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `UserConfirmation was updated`;                
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
                dalUserConfirmations.updateUserConfirmation(reqUserConfirmation)
                                        .then( (res) => { upsertUserConfirmationThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalUserConfirmations.insertUserConfirmation(reqUserConfirmation)
                                        .then( (res) => { upsertUserConfirmationThen(res); } )
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
        
        let dalUserConfirmations = new UserConfirmationsDal();
        let obj = this;

        dalUserConfirmations.deleteUserConfirmation(this.state.id).then( (response) => {
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

        const lstUserIDsFields = ["FirstName", "LastName"];
        const lstUserIDs = this._prepareOptionsList( this.state.users 
                                                                    ? Object.values(this.state.users) : null, 
                                                                    lstUserIDsFields,
                                                                    false );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>UserConfirmation: { this.state.userconfirmation.UserID ? this.state.users[this.state.userconfirmation.UserID].FirstName + " " + this.state.users[this.state.userconfirmation.UserID].LastName : "" }</h2>
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
                                <TextField  key="cbUserID" 
                                            fullWidth
                                            select 
                                            label="UserID" 
                                            value={ (this.state.userconfirmation && this.state.userconfirmation.UserID) ? 
                                                        this.state.userconfirmation.UserID : '-1' }
                                                        onChange={ (event) => this.onUserIDChanged(event) }>
                                        {
                                            lstUserIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="ConfirmationCode" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="ConfirmationCode" 
                                            value={this.state.userconfirmation.ConfirmationCode}
                                            onChange={ (event) => { this.onConfirmationCodeChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <FormControlLabel
                                    key="lblComfirmed"                        
                                    control = {
                                        <Checkbox   checked={ this.state.userconfirmation.Comfirmed ? true : false } 
                                                    onChange={(event) => this.onComfirmedChanged(event)} 
                                                    name="Comfirmed" />
                                        }
                                    label="Comfirmed"
                                />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="ExpiresDate" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="ExpiresDate" 
                                            value={this.state.userconfirmation.ExpiresDate}
                                            onChange={ (event) => { this.onExpiresDateChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="ConfirmationDate" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="ConfirmationDate" 
                                            value={this.state.userconfirmation.ConfirmationDate}
                                            onChange={ (event) => { this.onConfirmationDateChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete UserConfirmation</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this UserConfirmation?
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

    _createEmptyUserConfirmationObj() {
        let userconfirmation = new UserConfirmationDto();

        return userconfirmation;
    }

    async _getUserConfirmation()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalUserConfirmations = new UserConfirmationsDal();
            let response = await dalUserConfirmations.getUserConfirmation(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.userconfirmation = response.data;                
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

export default withRouter(UserConfirmationPage);