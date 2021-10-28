import logo from './logo.svg';
import './App.css';

import {
  BrowserRouter as Router,
  Route,
  Switch,
  Link,
  Redirect
} from 'react-router-dom';

import React from 'react';

import MainPage from './pages';
import RegisterPage from './pages/register';
import LogingPage from './pages/login';
import LogoutPage from './pages/logout';

import AddressesPage from './pages/admin/addresses';
import AddressPage from './pages/admin/address';
import AddressTypesPage from './pages/admin/addresstypes';
import AddressTypePage from './pages/admin/addresstype';
import OrdersPage from './pages/admin/orders';
import OrderPage from './pages/admin/order';
import UsersPage from './pages/admin/users';
import UserPage from './pages/admin/user';


class App extends React.Component {

  render() {
    return (
      <Router>
       {/*All our Routes goes here!*/}
       <Route exact path="/" component={MainPage} />
       <Route exact path="/register" component={RegisterPage} />
       <Route exact path="/login" component={LogingPage} />
       <Route exact path="/logout" component={LogoutPage} />
       

       {/*Admin pages*/}
       <Route exact path="/admin/addresses" component={AddressesPage} />
       <Route exact path="/admin/addresses/:operation/:id?" component={AddressPage} />
       <Route exact path="/admin/addresstypes" component={AddressTypesPage} />
       <Route exact path="/admin/addresstype/:operation/:id?" component={AddressTypePage} />
       <Route exact path="/admin/orders" component={OrdersPage} />
       <Route exact path="/admin/order/:operation/:id?" component={OrderPage} />
       <Route exact path="/admin/users" component={UsersPage} />
       <Route exact path="/admin/user/:operation/:id?" component={UserPage} />
      
      </Router>
    );
  }
}

export default App;
