


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
const Image = require("material-ui-image";

const constants = require('../../constants');
const { v4: uuidv4 } = require('uuid');
const PageHelper = require("../../helpers/PageHelper");
const ImageThumbnailsDal = require('../../dal/ImageThumbnailsDal');

const ImagesDal = require('../../dal/ImagesDal');
const { ImageThumbnailDto } = require('ppt.photoprint.dto')


class ImageThumbnailPage extends React.Component {

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
            imagethumbnail: this._createEmptyImageThumbnailObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}imagethumbnails`,
            urlThis: `${rooPath}imagethumbnail/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onUrlChanged = this.onUrlChanged.bind(this);
        this.onOrderChanged = this.onOrderChanged.bind(this);
        this.onImageIDChanged = this.onImageIDChanged.bind(this);
        this._getImageThumbnail = this._getImageThumbnail.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this.onUrlChanged = this.onUrlChanged.bind(this);
        this.onOrderChanged = this.onOrderChanged.bind(this);
        this.onImageIDChanged = this.onImageIDChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getImages().then( () => {
			obj._getImageThumbnail().then( () => {} );
			});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onUrlChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.imagethumbnail.Url = newVal;

        this.setState(updatedState);
    }

    onOrderChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.imagethumbnail.Order = newVal;

        this.setState(updatedState);
    }

    onImageIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.imagethumbnail.ImageID = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving ImageThumbnail: ", this.state.imagethumbnail);
        
        if(this._validateForm()) {
            const reqImageThumbnail = new ImageThumbnailDto();
            reqImageThumbnail.ID = this.state.id;
            reqImageThumbnail.Url = this.state.imagethumbnail.Url;
            reqImageThumbnail.Order = this.state.imagethumbnail.Order;
            reqImageThumbnail.ImageID = this.state.imagethumbnail.ImageID;

            console.log("Saving ImageThumbnail: ", reqImageThumbnail); 
        
            let dalImageThumbnails = new ImageThumbnailsDal();

            let obj = this;

            function upsertImageThumbnailThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `ImageThumbnail was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `ImageThumbnail was updated`;                
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
                dalImageThumbnails.updateImageThumbnail(reqImageThumbnail)
                                        .then( (res) => { upsertImageThumbnailThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalImageThumbnails.insertImageThumbnail(reqImageThumbnail)
                                        .then( (res) => { upsertImageThumbnailThen(res); } )
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
        
        let dalImageThumbnails = new ImageThumbnailsDal();
        let obj = this;

        dalImageThumbnails.deleteImageThumbnail(this.state.id).then( (response) => {
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

        const lstImageIDsFields = ["Title"];
        const lstImageIDs = this._prepareOptionsList( this.state.images 
                                                                    ? Object.values(this.state.images) : null, 
                                                                    lstImageIDsFields,
                                                                    false );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>ImageThumbnail: { this.state.imagethumbnail.ImageID ? this.state.images[ this.state.imagethumbnail.ImageID ].Title : "" }</h2>
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
                                <TextField  id="Url" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Url" 
                                            value={this.state.imagethumbnail.Url}
                                            onChange={ (event) => { this.onUrlChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
                        <tr>
                            <td>
                                <Image src={this.state.imagethumbnail.Url} />
                            </td>
                        </tr>   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Order" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Order" 
                                            value={this.state.imagethumbnail.Order}
                                            onChange={ (event) => { this.onOrderChanged(event) } }
                                            />

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbImageID" 
                                            fullWidth
                                            select 
                                            label="ImageID" 
                                            value={ (this.state.imagethumbnail && this.state.imagethumbnail.ImageID) ? 
                                                        this.state.imagethumbnail.ImageID : '-1' }
                                                        onChange={ (event) => this.onImageIDChanged(event) }>
                                        {
                                            lstImageIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete ImageThumbnail</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this ImageThumbnail?
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

    _createEmptyImageThumbnailObj() {
        let imagethumbnail = new ImageThumbnailDto();

        return imagethumbnail;
    }

    async _getImageThumbnail()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalImageThumbnails = new ImageThumbnailsDal();
            let response = await dalImageThumbnails.getImageThumbnail(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.imagethumbnail = response.data;                
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

export default withRouter(ImageThumbnailPage);