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
import Box from '@material-ui/core/Box';

import Draggable from 'react-draggable';

import PictureFit from "../components/PcitureFit"

import PageHelper from "../helpers/PageHelper";
import constants from '../constants';

import ImagesDal from '../dal/ImagesDal';
import FrameTypesDal from '../dal/FrameTypesDal';
import MaterialTypesDal from '../dal/MaterialTypesDal';
import MountingTypesDal from '../dal/MountingTypesDal';
import CurrenciesDal from '../dal/CurrenciesDal';
import OrdersDal from '../dal/OrdersDal';
import OrderItemsDal from '../dal/OrderItemsDal';


class PictureFitPage extends React.Component {

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);

        this.state = {

            showError: false,
            showSuccess: false,
            error: null,
            success: null,

            ImageID: null,
            RoomHeight: 250,
            PictureWidth: 120,
            PictureHeight: 60,
            picWidth: 200,
            picHeight: 100,
            roomPicHeight: 750,

            activeDrags: 0,
            deltaPosition: {
                x: 0, y: 0
            },
            controlledPosition: {
                x: -400, y: 200
            },

            urlThis: `/picturefit`
        };

        this._getImages = this._getImages.bind(this);
        this._recalcPicSize = this._recalcPicSize.bind(this);

        this.onImageIDChanged = this.onImageIDChanged.bind(this);
        this.onFrameTypeChanged = this.onFrameTypeChanged.bind(this);
        this.onMaterialTypeChanged = this.onMaterialTypeChanged.bind(this);
        this.onMountingTypeChanged = this.onMountingTypeChanged.bind(this);
        this.onRoomHeightChanged = this.onRoomHeightChanged.bind(this);
        this.onPictureHeightChanged = this.onPictureHeightChanged.bind(this);
        this.onPictureWidthChanged = this.onPictureWidthChanged.bind(this);
    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            obj._getCurrencies().then( () => {
                obj._getImages().then( () => {
                    obj._getFrameTypes().then( () => { 
                        obj._getMaterialTypes().then( () => { 
                            obj._getMountingTypes().then( () => { 
                                obj._recalcPicSize();
                            } );          
                        } );                
                    } );
                } );
            } );
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
        updatedState.ImageID = newVal;

        this.setState(updatedState);

        this._recalcPicSize();
    }

    onFrameTypeChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.FrameTypeID = newVal;

        this.setState(updatedState);

        this._recalcPicSize();
    }

    onMaterialTypeChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.MaterialTypeID = newVal;

        this.setState(updatedState);
    }

    onMountingTypeChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.MountingTypeID = newVal;

        this.setState(updatedState);
    }

    onRoomHeightChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.RoomHeight = newVal;

        this.setState(updatedState);

        this._recalcPicSize();
    }

    onPictureHeightChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.PictureHeight = newVal;

        this.setState(updatedState);

        this._recalcPicSize();
    }

    onPictureWidthChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.PictureWidth = newVal;

        this.setState(updatedState);

        this._recalcPicSize();
    }

    _recalcPicSize() {

        let updatedState = this.state;
        let pxsPerCm = this.state.roomPicHeight / this.state.RoomHeight;
        updatedState.picHeight = pxsPerCm * this.state.PictureHeight;
        updatedState.picWidth = pxsPerCm * this.state.PictureWidth;

        this.setState(updatedState);
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

            updatedState.ImageID = response.data[0].ID;
        }
        else if(response.status == constants.HTTP_Unauthorized) {
            this._redirectToLogin();            
        }
        else {
            this._showError(updatedState, response);                        
        }

        this.setState(updatedState);
    }

    async _getCurrencies() {
        let updatedState = this.state;
        updatedState.currencies = {};
        let dalCurrenies= new CurrenciesDal();
        let response = await dalCurrenies.getCurrencies();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.currencies[response.data[s].ID] = response.data[s];             
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

    async _getFrameTypes() {
        let updatedState = this.state;
        updatedState.frameTypes = {};
        let dalFrameTypes = new FrameTypesDal();
        let response = await dalFrameTypes.getFrameTypes();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.frameTypes[response.data[s].ID] = response.data[s];             
            }

            updatedState.FrameTypeID = response.data[0].ID;
        }
        else if(response.status == constants.HTTP_Unauthorized) {
            this._redirectToLogin();            
        }
        else {
            this._showError(updatedState, response);                        
        }

        this.setState(updatedState);
    }

    async _getMaterialTypes() {
        let updatedState = this.state;
        updatedState.materialTypes = {};
        let dalMaterialTypes = new MaterialTypesDal();
        let response = await dalMaterialTypes.getMaterialTypes();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.materialTypes[response.data[s].ID] = response.data[s];             
            }

            updatedState.MaterialTypeID = response.data[0].ID;
        }
        else if(response.status == constants.HTTP_Unauthorized) {
            this._redirectToLogin();            
        }
        else {
            this._showError(updatedState, response);                        
        }

        this.setState(updatedState);
    }

    async _getMountingTypes() {
        let updatedState = this.state;
        updatedState.mountingTypes = {};
        let dalMountingTypes = new MountingTypesDal();
        let response = await dalMountingTypes.getMountingTypes();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.mountingTypes[response.data[s].ID] = response.data[s];             
            }

            updatedState.MountingTypeID = response.data[0].ID;
        }
        else if(response.status == constants.HTTP_Unauthorized) {
            this._redirectToLogin();            
        }
        else {
            this._showError(updatedState, response);                        
        }

        this.setState(updatedState);
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

    render() {

        const styleFitArea = {
            position: "relative",
            left: "0",
            top: "0"
        };

        const styleRoomPhoto = {
            position: "relative",
            left: "0",
            top: "0",
            height: this.state.roomPicHeight + "px"
        };

        const stylePicture = {
            position: "absolute",
            transform: `translate(${(1000 - this.state.picWidth)/2}px, ${(this.roomPicHeight - this.state.picHeight)/2}px);`
        };

        const lstImageIDsFields = ["Title"];
        const lstImageIDs = this._prepareOptionsList( this.state.images 
                                                       ? Object.values(this.state.images) : null, 
                                                       lstImageIDsFields,
                                                       false );

        const lstFrameTypesFields = ["FrameTypeName"];
        const lstFrameTypes = this._prepareOptionsList( this.state.frameTypes 
                                                        ? Object.values(this.state.frameTypes) : null, 
                                                        lstFrameTypesFields,
                                                        false );

        const lstMaterialTypesFields = ["MaterialTypeName"];
        const lstMaterialTypes = this._prepareOptionsList( this.state.materialTypes 
                                                        ? Object.values(this.state.materialTypes) : null, 
                                                        lstMaterialTypesFields,
                                                        false );

        const lstMountingTypesFields = ["MountingTypeName"];
        const lstMountingTypes = this._prepareOptionsList( this.state.mountingTypes 
                                                        ? Object.values(this.state.mountingTypes) : null, 
                                                        lstMountingTypesFields,
                                                        false );

        const roomPicUrl = "/img/room_default.jpg";
        const picUrl = this.state.images && this.state.ImageID ? this.state.images[this.state.ImageID].OriginUrl : null; 
        const picFrame = this.state.frameTypes && this.state.FrameTypeID ? this.state.frameTypes[this.state.FrameTypeID].ThumbnailUrl : null;
        

        console.log("Picture: ", picUrl);

        return (
            <div>
                <table>
                    <tr>
                        <td>
                                <TextField  key="cbImageID" 
                                            select 
                                            label="Images: " 
                                            value={ (this.state.ImageID) ? 
                                                        this.state.ImageID : '-1' }
                                            onChange={ (event) => this.onImageIDChanged(event) }>
                                        {
                                            lstImageIDs 
                                        }
                                </TextField>
                        </td>
                        <td>
                                <TextField  key="cbFrameTypeID" 
                                            select 
                                            label="Frames: " 
                                            value={ (this.state.FrameTypeID) ? 
                                                        this.state.FrameTypeID : '-1' }
                                            onChange={ (event) => this.onFrameTypeChanged(event) }>
                                        {
                                            lstFrameTypes 
                                        }
                                </TextField>
                        </td>
                        <td>
                                <TextField  key="cbMaterialTypeID" 
                                            select 
                                            label="Materials: " 
                                            value={ (this.state.MaterialTypeID) ? 
                                                        this.state.MaterialTypeID : '-1' }
                                            onChange={ (event) => this.onMaterialTypeChanged(event) }>
                                        {
                                            lstMaterialTypes 
                                        }
                                </TextField>
                        </td>
                        <td>
                                <TextField  key="cbMountingTypeID" 
                                            select 
                                            label="Mountings: " 
                                            value={ (this.state.MountingTypeID) ? 
                                                        this.state.MountingTypeID : '-1' }
                                            onChange={ (event) => this.onMountingTypeChanged(event) }>
                                        {
                                            lstMountingTypes 
                                        }
                                </TextField>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <TextField  key="txtRoomHeight"
                                        label="Room Height (cm):"
                                        value={this.state.RoomHeight}
                                        onChange={ (e) => this.onRoomHeightChanged(e) }>
                            </TextField>
                        </td>
                        <td>
                            <TextField  key="txtPcitureWidth"
                                        label="Picture Width (cm):"
                                        value={this.state.PictureWidth}
                                        onChange={ (e) => this.onPictureWidthChanged(e) }>
                            </TextField>
                        </td>
                        <td>
                            <TextField  key="txtPcitureHeight"
                                        label="Picture Height (cm):"
                                        value={this.state.PictureHeight}
                                        onChange={ (e) => this.onPictureHeightChanged(e) }>
                            </TextField>
                        </td>
                    </tr> 
                    <tr>
                        <td>
                            Price Per Item: { this.state.images && this.state.ImageID ? (this.state.images[this.state.ImageID].PriceAmount ? this.state.images[this.state.ImageID].PriceAmount : " ") + " " +
                                                                                        (this.state.images[this.state.ImageID].PriceCurrencyID ? this.state.currencies[this.state.images[this.state.ImageID].PriceCurrencyID].ISO : "") : 0}
                        </td>
                    </tr>
                    <tr>
                        <td colSpan={4}>
                            <div>
                                <img src={roomPicUrl} style={styleRoomPhoto}/>
                                <Draggable bounds="parent"
                                            style={stylePicture}>
                                <Box    width={this.state.picWidth}
                                        sx={{ top: -500, left: -500 }}
                                        boxShadow={3}>
                                        <PictureFit 
                                            picUrl={picUrl}
                                            picFrame={picFrame}
                                            picWidth={this.state.picWidth}
                                            picHeight={this.state.picHeight} 
                                        />
                                </Box>
                                    
                                </Draggable>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        );
    }

}

export default withRouter(PictureFitPage);