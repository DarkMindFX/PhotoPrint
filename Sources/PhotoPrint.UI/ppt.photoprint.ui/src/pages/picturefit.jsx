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

import PictureFit from "../components/PcitureFit"

import PageHelper from "../helpers/PageHelper";
import constants from '../constants';

import ImagesDal from '../dal/ImagesDal';
import OrdersDal from '../dal/OrdersDal';

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

            urlThis: `/picturefit`
        };

        this._getImages = this._getImages.bind(this);
        this._recalcPicSize = this._recalcPicSize.bind(this);

        this.onImageIDChanged = this.onImageIDChanged.bind(this);
        this.onRoomHeightChanged = this.onRoomHeightChanged.bind(this);
        this.onPictureHeightChanged = this.onPictureHeightChanged.bind(this);
        this.onPictureWidthChanged = this.onPictureWidthChanged.bind(this);
    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            obj._getImages().then( () => {

            } );
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
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
            left: "400px",
            top: "150px"
        };

        const lstImageIDsFields = ["Title"];
        const lstImageIDs = this._prepareOptionsList( this.state.images 
                                                       ? Object.values(this.state.images) : null, 
                                                       lstImageIDsFields,
                                                       false );

        const roomPicUrl = "/img/room_default.jpg";
        const picUrl = this.state.images && this.state.ImageID ? this.state.images[this.state.ImageID].OriginUrl : null; 

        console.log("Picture: ", picUrl);

        return (
            <div>
                <table>
                    <tr>
                        <td colSpan={3}>
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
                        <td colSpan={3}>
                            <div style={styleFitArea}>
                                <img src={roomPicUrl} style={styleRoomPhoto}/>
                                <div style={stylePicture}>
                                    <PictureFit picUrl={picUrl}
                                                picWidth={this.state.picWidth}
                                                picHeight={this.state.picHeight}
                                                 />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        );
    }

    onImageIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.ImageID = newVal;

        this.setState(updatedState);

        this._recalcPicSize();
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

}

export default withRouter(PictureFitPage);