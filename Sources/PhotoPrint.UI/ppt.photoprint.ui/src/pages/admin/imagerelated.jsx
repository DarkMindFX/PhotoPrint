


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

const PageHelper = require("../helpers/PageHelper");
const ImageRelatedsDal = require('../dal/ImageRelatedsDal');
const { ImageRelatedDto } = require('ppt.photoprint.dto')

const constants = require('../constants');
const { v4: uuidv4 } = require('uuid');

class ImageRelatedPage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let paramOperation = this.props.match.params.operation;
        let paramId = this.props.match.params.id;
        let rooPath = ''; // set the page hierarchy here

        this.state = { 
            operation:  paramOperation,
            id:         paramId ? parseInt(paramId) : null,
            canEdit:    paramOperation ? ( paramOperation.toLowerCase() == 'new' || 
                                        paramOperation.toLowerCase() == 'edit' ? true : false) : false,
            imagerelated: this._createEmptyImageRelatedObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}/imagerelateds`,
            urlThis: `${rooPath}/imagerelated/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onImageIDChanged = this.onImageIDChanged.bind(this);
        this.onRelatedImageIDChanged = this.onRelatedImageIDChanged.bind(this);
        this._getImageRelated = this._getImageRelated.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onImageIDChanged = this.onImageIDChanged.bind(this);
        this.onRelatedImageIDChanged = this.onRelatedImageIDChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getImageRelated().then( () => {} );
			
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onImageIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.imagerelated.ImageID = newVal;

        this.setState(updatedState);
    }

    onRelatedImageIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.imagerelated.RelatedImageID = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving ImageRelated: ", this.state.imagerelated);
        
        if(this._validateForm()) {
            const reqImageRelated = new ImageRelatedDto();
            reqImageRelated.ID = this.state.id;
            reqImageRelated.ImageID = this.state.imagerelated.ImageID;
            reqImageRelated.RelatedImageID = this.state.imagerelated.RelatedImageID;

            console.log("Saving ImageRelated: ", reqImageRelated); 
        
            let dalImageRelateds = new ImageRelatedsDal();

            let obj = this;

            function upsertImageRelatedThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `ImageRelated was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `ImageRelated was updated`;                
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
                dalImageRelateds.updateImageRelated(reqImageRelated)
                                        .then( (res) => { upsertImageRelatedThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalImageRelateds.insertImageRelated(reqImageRelated)
                                        .then( (res) => { upsertImageRelatedThen(res); } )
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
        
        let dalImageRelateds = new ImageRelatedsDal();
        let obj = this;

        dalImageRelateds.deleteImageRelated(this.state.id).then( (response) => {
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
                                <h2>ImageRelated: { this.state.imagerelated.toString() }</h2>
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
                                <TextField  id="ImageID" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="ImageID" 
                                            value={this.state.imagerelated.ImageID}
                                            onChange={ (event) => { this.onImageIDChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="RelatedImageID" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="RelatedImageID" 
                                            value={this.state.imagerelated.RelatedImageID}
                                            onChange={ (event) => { this.onRelatedImageIDChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete ImageRelated</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this ImageRelated?
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

    _createEmptyImageRelatedObj() {
        let imagerelated = new ImageRelatedDto();

        return imagerelated;
    }

    async _getImageRelated()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalImageRelateds = new ImageRelatedsDal();
            let response = await dalImageRelateds.getImageRelated(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.imagerelated = response.data;                
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

export default withRouter(ImageRelatedPage);