
import './App.css';

import {
  BrowserRouter as Router,
  Route,
  Switch,
  Link,
  Redirect
} from 'react-router-dom';

import React from 'react';
import constants from './constants';

import MainPage from './pages';
import RegisterPage from './pages/register';
import LogingPage from './pages/login';
import LogoutPage from './pages/logout';
import PictureFitPage from './pages/picturefit';

import AddressesPage from './pages/admin/addresses';
import AddressPage from './pages/admin/address';
import AddressTypesPage from './pages/admin/addresstypes';
import AddressTypePage from './pages/admin/addresstype';
import CategoriesPage from './pages/admin/categories';
import CategoryPage from './pages/admin/category';
import CitiesPage from './pages/admin/cities';
import CityPage from './pages/admin/city';
import ContactsPage from './pages/admin/contacts';
import ContactPage from './pages/admin/contact';
import ContactTypesPage from './pages/admin/contacttypes';
import ContactTypePage from './pages/admin/contacttype';
import CountriesPage from './pages/admin/countries';
import CountryPage from './pages/admin/country';
import CurrenciesPage from './pages/admin/currencies';
import CurrencyPage from './pages/admin/currency';
import DeliveryServicesPage from './pages/admin/deliveryservices';
import DeliveryServicePage from './pages/admin/deliveryservice';
import DeliveryServiceCitiesPage from './pages/admin/deliveryservicecities';
import DeliveryServiceCityPage from './pages/admin/deliveryservicecity';
import FrameTypesPage from './pages/admin/frametypes';
import FrameTypePage from './pages/admin/frametype';
import ImagesPage from './pages/admin/images';
import ImagePage from './pages/admin/image';
import ImageCategoriesPage from './pages/admin/imagecategories';
import ImageCategoryPage from './pages/admin/imagecategory';
import ImageRelatedsPage from './pages/admin/imagerelateds';
import ImageRelatedPage from './pages/admin/imagerelated';
import ImageThumbnailsPage from './pages/admin/imagethumbnails';
import ImageThumbnailPage from './pages/admin/imagethumbnail';
import MatsPage from './pages/admin/mats';
import MatPage from './pages/admin/mat';
import MaterialTypesPage from './pages/admin/materialtypes';
import MaterialTypePage from './pages/admin/materialtype';
import MountingTypesPage from './pages/admin/mountingtypes';
import MountingTypePage from './pages/admin/mountingtype';
import OrdersPage from './pages/admin/orders';
import OrderPage from './pages/admin/order';
import OrderItemsPage from './pages/admin/orderitems';
import OrderItemPage from './pages/admin/orderitem';
import OrderPaymentDetailsesPage from './pages/admin/orderpaymentdetailses';
import OrderPaymentDetailsPage from './pages/admin/orderpaymentdetails';
import OrderStatusesPage from './pages/admin/orderstatuses';
import OrderStatusPage from './pages/admin/orderstatus';
import OrderStatusFlowsPage from './pages/admin/orderstatusflows';
import OrderStatusFlowPage from './pages/admin/orderstatusflow';
import OrderTrackingsPage from './pages/admin/ordertrackings';
import OrderTrackingPage from './pages/admin/ordertracking';
import PaymentMethodsPage from './pages/admin/paymentmethods';
import PaymentMethodPage from './pages/admin/paymentmethod';
import PrintingHousesPage from './pages/admin/printinghouses';
import PrintingHousePage from './pages/admin/printinghouse';
import PrintingHouseAddressesPage from './pages/admin/printinghouseaddresses';
import PrintingHouseAddressPage from './pages/admin/printinghouseaddress';
import PrintingHouseContactsPage from './pages/admin/printinghousecontacts';
import PrintingHouseContactPage from './pages/admin/printinghousecontact';
import RegionsPage from './pages/admin/regions';
import RegionPage from './pages/admin/region';
import SizesPage from './pages/admin/sizes';
import SizePage from './pages/admin/size';
import UnitsPage from './pages/admin/units';
import UnitPage from './pages/admin/unit';
import UsersPage from './pages/admin/users';
import UserPage from './pages/admin/user';
import UserAddressesPage from './pages/admin/useraddresses';
import UserAddressPage from './pages/admin/useraddress';
import UserConfirmationsPage from './pages/admin/userconfirmations';
import UserConfirmationPage from './pages/admin/userconfirmation';
import UserContactsPage from './pages/admin/usercontacts';
import UserContactPage from './pages/admin/usercontact';
import UserStatusesPage from './pages/admin/userstatuses';
import UserStatusPage from './pages/admin/userstatus';
import UserTypesPage from './pages/admin/usertypes';
import UserTypePage from './pages/admin/usertype';


class App extends React.Component {

  render() {



    console.log('PPT_API_HOST:', constants.PPT_API_HOST);
    console.log('PPT_API_VERSION:', constants.PPT_API_VERSION);

    return (
      <Router>
       {/*All our Routes goes here!*/}
       <Route exact path="/" component={MainPage} />
       <Route exact path="/register" component={RegisterPage} />
       <Route exact path="/login" component={LogingPage} />
       <Route exact path="/logout" component={LogoutPage} />
       <Route exact path="/picturefit" component={PictureFitPage} />
       

       {/*Admin pages*/}
       <Route exact path="/admin/addresses" component={AddressesPage} />
       <Route exact path="/admin/address/:operation/:id?" component={AddressPage} />
       <Route exact path="/admin/addresstypes" component={AddressTypesPage} />
       <Route exact path="/admin/addresstype/:operation/:id?" component={AddressTypePage} />
       <Route exact path="/admin/categories" component={CategoriesPage} />
       <Route exact path="/admin/category/:operation/:id?" component={CategoryPage} />
       <Route exact path="/admin/cities" component={CitiesPage} />
       <Route exact path="/admin/city/:operation/:id?" component={CityPage} />
       <Route exact path="/admin/contacts" component={ContactsPage} />
       <Route exact path="/admin/contact/:operation/:id?" component={ContactPage} />
       <Route exact path="/admin/contacttypes" component={ContactTypesPage} />
       <Route exact path="/admin/contacttype/:operation/:id?" component={ContactTypePage} />
       <Route exact path="/admin/countries" component={CountriesPage} />
       <Route exact path="/admin/country/:operation/:id?" component={CountryPage} />
       <Route exact path="/admin/currencies" component={CurrenciesPage} />
       <Route exact path="/admin/currency/:operation/:id?" component={CurrencyPage} />
       <Route exact path="/admin/deliveryservices" component={DeliveryServicesPage} />
       <Route exact path="/admin/deliveryservice/:operation/:id?" component={DeliveryServicePage} />
       <Route exact path="/admin/deliveryservicecities" component={DeliveryServiceCitiesPage} />
       <Route exact path="/admin/deliveryservicecity/:operation/:deliveryserviceid?/:cityid?" component={DeliveryServiceCityPage} />
       <Route exact path="/admin/frametypes" component={FrameTypesPage} />
       <Route exact path="/admin/frametype/:operation/:id?" component={FrameTypePage} />
       <Route exact path="/admin/images" component={ImagesPage} />
       <Route exact path="/admin/image/:operation/:id?" component={ImagePage} />
       <Route exact path="/admin/imagecategories" component={ImageCategoriesPage} />
       <Route exact path="/admin/imagecategory/:operation/:imageid?/:categoryid?" component={ImageCategoryPage} />
       <Route exact path="/admin/imagerelateds" component={ImageRelatedsPage} />
       <Route exact path="/admin/imagerelated/:operation/:imageid?/:relatedimageid?" component={ImageRelatedPage} />
       <Route exact path="/admin/imagethumbnails" component={ImageThumbnailsPage} />
       <Route exact path="/admin/imagethumbnail/:operation/:id?" component={ImageThumbnailPage} />
       <Route exact path="/admin/mats" component={MatsPage} />
       <Route exact path="/admin/mat/:operation/:id?" component={MatPage} />
       <Route exact path="/admin/materialtypes" component={MaterialTypesPage} />
       <Route exact path="/admin/materialtype/:operation/:id?" component={MaterialTypePage} />
       <Route exact path="/admin/mountingtypes" component={MountingTypesPage} />
       <Route exact path="/admin/mountingtype/:operation/:id?" component={MountingTypePage} />
       <Route exact path="/admin/orders" component={OrdersPage} />
       <Route exact path="/admin/order/:operation/:id?" component={OrderPage} />
       <Route exact path="/admin/orderitems" component={OrderItemsPage} />
       <Route exact path="/admin/orderitem/:operation/:id?" component={OrderItemPage} />
       <Route exact path="/admin/orderpaymentdetailses" component={OrderPaymentDetailsesPage} />
       <Route exact path="/admin/orderpaymentdetails/:operation/:id?" component={OrderPaymentDetailsPage} />
       <Route exact path="/admin/orderstatuses" component={OrderStatusesPage} />
       <Route exact path="/admin/orderstatus/:operation/:id?" component={OrderStatusPage} />
       <Route exact path="/admin/orderstatusflows" component={OrderStatusFlowsPage} />
       <Route exact path="/admin/orderstatusflow/:operation/:fromstatusid?/:tostatusid?" component={OrderStatusFlowPage} />
       <Route exact path="/admin/ordertrackings" component={OrderTrackingsPage} />
       <Route exact path="/admin/ordertracking/:operation/:id?" component={OrderTrackingPage} />
       <Route exact path="/admin/paymentmethods" component={PaymentMethodsPage} />
       <Route exact path="/admin/paymentmethod/:operation/:id?" component={PaymentMethodPage} />
       <Route exact path="/admin/printinghouses" component={PrintingHousesPage} />
       <Route exact path="/admin/printinghouse/:operation/:id?" component={PrintingHousePage} />
       <Route exact path="/admin/printinghouseaddresses" component={PrintingHouseAddressesPage} />
       <Route exact path="/admin/printinghouseaddress/:operation/:printinghouseid?/:addressid?" component={PrintingHouseAddressPage} />
       <Route exact path="/admin/printinghousecontacts" component={PrintingHouseContactsPage} />
       <Route exact path="/admin/printinghousecontact/:operation/:printinghouseid?/:contactid?" component={PrintingHouseContactPage} />
       <Route exact path="/admin/regions" component={RegionsPage} />
       <Route exact path="/admin/region/:operation/:id?" component={RegionPage} />
       <Route exact path="/admin/sizes" component={SizesPage} />
       <Route exact path="/admin/size/:operation/:id?" component={SizePage} />
       <Route exact path="/admin/units" component={UnitsPage} />
       <Route exact path="/admin/unit/:operation/:id?" component={UnitPage} />
       <Route exact path="/admin/users" component={UsersPage} />
       <Route exact path="/admin/user/:operation/:id?" component={UserPage} />
       <Route exact path="/admin/useraddresses" component={UserAddressesPage} />
       <Route exact path="/admin/useraddress/:operation/:userid?/:addressid?" component={UserAddressPage} />
       <Route exact path="/admin/userconfirmations" component={UserConfirmationsPage} />
       <Route exact path="/admin/userconfirmation/:operation/:id?" component={UserConfirmationPage} />
       <Route exact path="/admin/usercontacts" component={UserContactsPage} />
       <Route exact path="/admin/usercontact/:operation/:userid?/:contactid??" component={UserContactPage} />
       <Route exact path="/admin/userstatuses" component={UserStatusesPage} />
       <Route exact path="/admin/userstatus/:operation/:id?" component={UserStatusPage} />
       <Route exact path="/admin/usertypes" component={UserTypesPage} />
       <Route exact path="/admin/usertype/:operation/:id?" component={UserTypePage} />
      
      </Router>
    );
  }
}

export default App;
