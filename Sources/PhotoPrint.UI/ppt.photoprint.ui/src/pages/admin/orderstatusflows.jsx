



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../../constants";

import PageHelper from "../../helpers/PageHelper";
import OrderStatusFlowsDal from '../../dal/OrderStatusFlowsDal';


import OrderStatusesDal from '../../dal/OrderStatusesDal';




class OrderStatusFlowsPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);
        let rooPath = '/admin/'; // set the page hierarchy here

        this.state = { 
            orderstatusflows: [],
            showError: false,
            error: null,
            urlThis: `${rooPath}orderstatusflows`,
            urlNewEntity: `${rooPath}orderstatusflow/new`,
            urlEditEntity: `${rooPath}orderstatusflow/edit/`,
        };
        this._initColumns();
       
        this._getOrderStatuses = this._getOrderStatuses.bind(this);
        this._getOrderStatusFlows = this._getOrderStatusFlows.bind(this);
        this._redirectToLogin = this._redirectToLogin.bind(this);

        this.onRowClick = this.onRowClick.bind(this);
    }

    onRowClick(event) {
        const row = event.row;
        if(row) {
            this.props.history.push(this.state.urlEditEntity + row.FromStatusID + "/" + row.ToStatusID);
        }

    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getOrderStatuses().then( () => {
			obj._getOrderStatusFlows().then( () => {} );
			});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    render() {
        let records = this._getRecords();

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        return (
            <div style={{ height: 500, width: '100%' }}>
                <h3>OrderStatusFlows</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to={this.state.urlNewEntity} >+ OrderStatusFlow</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'FromStatus', headerName: 'FromStatusID', width: 250 },
                { field: 'ToStatus', headerName: 'ToStatusID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.orderstatusflows);

        for(let c in cs) {

            let r = {
                id: c,
                FromStatusID: cs[c].FromStatusID,
                ToStatusID: cs[c].ToStatusID,
                FromStatus: cs[c].FromStatusID ? this.state.orderstatuses[ cs[c].FromStatusID ].OrderStatusName : "",
                ToStatus: cs[c].ToStatusID ? this.state.orderstatuses[ cs[c].ToStatusID ].OrderStatusName : "",

            };

            records.push(r);
        }

        return records;
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
    

    async _getOrderStatusFlows() {
        let updatedState = this.state;
        updatedState.orderstatusflows = {};
        let dalOrderStatusFlows = new OrderStatusFlowsDal();
        let response = await dalOrderStatusFlows.getOrderStatusFlows();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.orderstatusflows[[
                    response.data[s].FromStatusID,
                    response.data[s].ToStatusID ]] = response.data[s];             
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

    _showError(updatedState, response) {
        var error = JSON.parse(response.data.response);
        updatedState.showError = true;
        updatedState.error = error.Message;
    }

    _redirectToLogin()
    {        
        this._pageHelper.redirectToLogin(this.state.urlThis);  
    }
}

export default withRouter(OrderStatusFlowsPage);