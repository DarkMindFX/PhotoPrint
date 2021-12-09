


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
const OrderStatusFlowsDal = require('../../dal/OrderStatusFlowsDal');

const OrderStatusesDal = require('../../dal/OrderStatusesDal');
const { OrderStatusFlowDto } = require('ppt.photoprint.dto')


class OrderStatusFlowPage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let paramOperation = this.props.match.params.operation;
        let paramFromStatusId = this.props.match.params.fromstatusid;
        let paramToStatusId = this.props.match.params.tostatusid;
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            operation:  paramOperation,
            fromstatusid:         paramFromStatusId ? parseInt(paramFromStatusId) : null,
            tostatusid:         paramToStatusId ? parseInt(paramToStatusId) : null,
            canEdit:    paramOperation ? ( paramOperation.toLowerCase() == 'new' || 
                                        paramOperation.toLowerCase() == 'edit' ? true : false) : false,
            orderstatusflow: this._createEmptyOrderStatusFlowObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}orderstatusflows`,
            urlThis: `${rooPath}orderstatusflow/${paramOperation}` + (paramFromStatusId ? `/${paramFromStatusId}/${paramToStatusId}` : ``)
        };

        this.onFromStatusIDChanged = this.onFromStatusIDChanged.bind(this);
        this.onToStatusIDChanged = this.onToStatusIDChanged.bind(this);
        this._getOrderStatusFlow = this._getOrderStatusFlow.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onFromStatusIDChanged = this.onFromStatusIDChanged.bind(this);
        this.onToStatusIDChanged = this.onToStatusIDChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getOrderStatuses().then( () => {
			obj._getOrderStatusFlow().then( () => {} );
			});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onFromStatusIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderstatusflow.FromStatusID = newVal;

        this.setState(updatedState);
    }

    onToStatusIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.orderstatusflow.ToStatusID = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving OrderStatusFlow: ", this.state.orderstatusflow);
        
        if(this._validateForm()) {
            const reqOrderStatusFlow = new OrderStatusFlowDto();
            reqOrderStatusFlow.FromStatusID = this.state.orderstatusflow.FromStatusID;
            reqOrderStatusFlow.ToStatusID = this.state.orderstatusflow.ToStatusID;

            console.log("Saving OrderStatusFlow: ", reqOrderStatusFlow); 
        
            let dalOrderStatusFlows = new OrderStatusFlowsDal();

            let obj = this;

            function upsertOrderStatusFlowThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.fromstatusid = response.data.FromStatusID;
                        updatedState.tostatusid = response.data.ToStatusID;
                        updatedState.success = `OrderStatusFlow was created.`;
                    }
                    else {
                        updatedState.success = `OrderStatusFlow was updated`;                
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

            if(this.state.fromstatusid != null && this.state.tostatusid != null) {
                dalOrderStatusFlows.updateOrderStatusFlow(reqOrderStatusFlow)
                                        .then( (res) => { upsertOrderStatusFlowThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalOrderStatusFlows.insertOrderStatusFlow(reqOrderStatusFlow)
                                        .then( (res) => { upsertOrderStatusFlowThen(res); } )
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
        
        let dalOrderStatusFlows = new OrderStatusFlowsDal();
        let obj = this;

        dalOrderStatusFlows.deleteOrderStatusFlow(this.state.fromstatusid, this.state.tostatusid).then( (response) => {
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
            display: this.state.fromstatusid && this.state.tostatusid ? "block" : "none"
        }

        const lstFromStatusIDsFields = ["OrderStatusName"];
        const lstFromStatusIDs = this._prepareOptionsList( this.state.orderstatuses 
                                                                    ? Object.values(this.state.orderstatuses) : null, 
                                                                    lstFromStatusIDsFields,
                                                                    false );
        const lstToStatusIDsFields = ["OrderStatusName"];
        const lstToStatusIDs = this._prepareOptionsList( this.state.orderstatuses 
                                                                    ? Object.values(this.state.orderstatuses) : null, 
                                                                    lstToStatusIDsFields,
                                                                    false );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>OrderStatusFlow: { (this.state.orderstatusflow.FromStatusID ? this.state.orderstatuses[ this.state.orderstatusflow.FromStatusID ].OrderStatusName : "")
                                    + " to " +
                                    (this.state.orderstatusflow.ToStatusID ? this.state.orderstatuses[ this.state.orderstatusflow.ToStatusID ].OrderStatusName : "")
                                     }
                                </h2>
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
                                <TextField  key="cbFromStatusID" 
                                            fullWidth
                                            select 
                                            label="FromStatusID" 
                                            value={ (this.state.orderstatusflow && this.state.orderstatusflow.FromStatusID) ? 
                                                        this.state.orderstatusflow.FromStatusID : '-1' }
                                                        onChange={ (event) => this.onFromStatusIDChanged(event) }>
                                        {
                                            lstFromStatusIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbToStatusID" 
                                            fullWidth
                                            select 
                                            label="ToStatusID" 
                                            value={ (this.state.orderstatusflow && this.state.orderstatusflow.ToStatusID) ? 
                                                        this.state.orderstatusflow.ToStatusID : '-1' }
                                                        onChange={ (event) => this.onToStatusIDChanged(event) }>
                                        {
                                            lstToStatusIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete OrderStatusFlow</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this OrderStatusFlow?
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

    _createEmptyOrderStatusFlowObj() {
        let orderstatusflow = new OrderStatusFlowDto();

        return orderstatusflow;
    }

    async _getOrderStatusFlow()
    {
        if(this.state.fromstatusid && this.state.tostatusid) {
            let updatedState = this.state;
                  
            let dalOrderStatusFlows = new OrderStatusFlowsDal();
            let response = await dalOrderStatusFlows.getOrderStatusFlow(this.state.fromstatusid, this.state.tostatusid);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.orderstatusflow = response.data;                
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

    async _getOrderStatuses() {
        let updatedState = this.state;
        updatedState.orderstatuses = {};
        let dalOrderStatuses = new OrderStatusesDal();
        let response = await dalOrderStatuses.getOrderStatuses();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.orderstatuses[response.data[s].ID] = response.data[s];             
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

export default withRouter(OrderStatusFlowPage);