


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
const ImageRelatedsDal = require('../../dal/ImageRelatedsDal');

const ImagesDal = require('../../dal/ImagesDal');
const { ImageRelatedDto } = require('ppt.photoprint.dto')


class ImageRelatedPage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let paramOperation = this.props.match.params.operation;
        let paramImageId = this.props.match.params.imageid;
        let paramRelatedImageId = this.props.match.params.relatedimageid;
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            operation:  paramOperation,
            imageid:    paramImageId ? parseInt(paramImageId) : null,
            relatedimageid:    paramRelatedImageId ? parseInt(paramRelatedImageId) : null,
            canEdit:    paramOperation ? ( paramOperation.toLowerCase() == 'new' || 
                                        paramOperation.toLowerCase() == 'edit' ? true : false) : false,
            imagerelated: this._createEmptyImageRelatedObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}imagerelateds`,
            urlThis: `${rooPath}imagerelated/${paramOperation}` + (paramImageId ? `/${paramImageId}/${paramRelatedImageId}` : ``)
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
            			obj._getImages().then( () => {
			obj._getImageRelated().then( () => {} );
			});
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
                        updatedState.imageid = response.data.ImageID;
                        updatedState.relatedimageid = response.data.RelatedImageID;
                        updatedState.success = `ImageRelated was created.`;
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

            if(this.state.imageid != null && this.state.relatedimageid != null) {
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

        dalImageRelateds.deleteImageRelated(this.state.imageid, this.state.relatedimageid).then( (response) => {
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
            display: this.state.imageid && this.state.relatedimageid ? "block" : "none"
        }

        const lstImageIDsFields = ["Title"];
        const lstImageIDs = this._prepareOptionsList( this.state.images 
                                                                    ? Object.values(this.state.images) : null, 
                                                                    lstImageIDsFields,
                                                                    false );
        const lstRelatedImageIDsFields = ["Title"];
        const lstRelatedImageIDs = this._prepareOptionsList( this.state.images 
                                                                    ? Object.values(this.state.images) : null, 
                                                                    lstRelatedImageIDsFields,
                                                                    false );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>ImageRelated: { 
                                    (this.state.imagerelated.ImageID ? this.state.images[ this.state.imagerelated.ImageID ].Title : "")
                                    + " - " +
                                    (this.state.imagerelated.RelatedImageID ? this.state.images[ this.state.imagerelated.RelatedImageID ].Title : "")
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
                                <TextField  key="cbImageID" 
                                            fullWidth
                                            select 
                                            label="ImageID" 
                                            value={ (this.state.imagerelated && this.state.imagerelated.ImageID) ? 
                                                        this.state.imagerelated.ImageID : '-1' }
                                                        onChange={ (event) => this.onImageIDChanged(event) }>
                                        {
                                            lstImageIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbRelatedImageID" 
                                            fullWidth
                                            select 
                                            label="RelatedImageID" 
                                            value={ (this.state.imagerelated && this.state.imagerelated.RelatedImageID) ? 
                                                        this.state.imagerelated.RelatedImageID : '-1' }
                                                        onChange={ (event) => this.onRelatedImageIDChanged(event) }>
                                        {
                                            lstRelatedImageIDs 
                                        }
                                </TextField>

                                
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
        if(this.state.imageid && this.state.relatedimageid) {
            let updatedState = this.state;
                  
            let dalImageRelateds = new ImageRelatedsDal();
            let response = await dalImageRelateds.getImageRelated(this.state.imageid, this.state.relatedimageid);

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