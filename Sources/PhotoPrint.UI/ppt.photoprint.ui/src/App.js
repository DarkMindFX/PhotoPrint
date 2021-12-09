import logo from './logo.svg';
import './App.css';

const {
  BrowserRouter,
  Route,
  Switch,
  Link,
  Redirect
} = require('react-router-dom');

const React = require('react');

const MainPage = require('./pages');
const RegisterPage = require('./pages/register');
const LogingPage = require('./pages/login');
const LogoutPage = require('./pages/logout');

const AddressesPage = require('./pages/admin/addresses');
const AddressPage = require('./pages/admin/address');
const AddressTypesPage = require('./pages/admin/addresstypes');
const AddressTypePage = require('./pages/admin/addresstype');
const CategoriesPage = require('./pages/admin/categories');
const CategoryPage = require('./pages/admin/category');
const CitiesPage = require('./pages/admin/cities');
const CityPage = require('./pages/admin/city');
const ContactsPage = require('./pages/admin/contacts');
const ContactPage = require('./pages/admin/contact');
const ContactTypesPage = require('./pages/admin/contacttypes');
const ContactTypePage = require('./pages/admin/contacttype');
const CountriesPage = require('./pages/admin/countries');
const CountryPage = require('./pages/admin/country');
const CurrenciesPage = require('./pages/admin/currencies');
const CurrencyPage = require('./pages/admin/currency');
const DeliveryServicesPage = require('./pages/admin/deliveryservices');
const DeliveryServicePage = require('./pages/admin/deliveryservice');
const DeliveryServiceCitiesPage = require('./pages/admin/deliveryservicecities');
const DeliveryServiceCityPage = require('./pages/admin/deliveryservicecity');
const FrameTypesPage = require('./pages/admin/frametypes');
const FrameTypePage = require('./pages/admin/frametype');
const ImagesPage = require('./pages/admin/images');
const ImagePage = require('./pages/admin/image');
const ImageCategoriesPage = require('./pages/admin/imagecategories');
const ImageCategoryPage = require('./pages/admin/imagecategory');
const ImageRelatedsPage = require('./pages/admin/imagerelateds');
const ImageRelatedPage = require('./pages/admin/imagerelated');
const ImageThumbnailsPage = require('./pages/admin/imagethumbnails');
const ImageThumbnailPage = require('./pages/admin/imagethumbnail');
const MatsPage = require('./pages/admin/mats');
const MatPage = require('./pages/admin/mat');
const MaterialTypesPage = require('./pages/admin/materialtypes');
const MaterialTypePage = require('./pages/admin/materialtype');
const MountingTypesPage = require('./pages/admin/mountingtypes');
const MountingTypePage = require('./pages/admin/mountingtype');
const OrdersPage = require('./pages/admin/orders');
const OrderPage = require('./pages/admin/order');
const OrderItemsPage = require('./pages/admin/orderitems');
const OrderItemPage = require('./pages/admin/orderitem');
const OrderPaymentDetailsesPage = require('./pages/admin/orderpaymentdetailses');
const OrderPaymentDetailsPage = require('./pages/admin/orderpaymentdetails');
const OrderStatusesPage = require('./pages/admin/orderstatuses');
const OrderStatusPage = require('./pages/admin/orderstatus');
const OrderStatusFlowsPage = require('./pages/admin/orderstatusflows');
const OrderStatusFlowPage = require('./pages/admin/orderstatusflow');
const OrderTrackingsPage = require('./pages/admin/ordertrackings');
const OrderTrackingPage = require('./pages/admin/ordertracking');
const PaymentMethodsPage = require('./pages/admin/paymentmethods');
const PaymentMethodPage = require('./pages/admin/paymentmethod');
const PrintingHousesPage = require('./pages/admin/printinghouses');
const PrintingHousePage = require('./pages/admin/printinghouse');
const PrintingHouseAddressesPage = require('./pages/admin/printinghouseaddresses');
const PrintingHouseAddressPage = require('./pages/admin/printinghouseaddress');
const PrintingHouseContactsPage = require('./pages/admin/printinghousecontacts');
const PrintingHouseContactPage = require('./pages/admin/printinghousecontact');
const RegionsPage = require('./pages/admin/regions');
const RegionPage = require('./pages/admin/region');
const SizesPage = require('./pages/admin/sizes');
const SizePage = require('./pages/admin/size');
const UnitsPage = require('./pages/admin/units');
const UnitPage = require('./pages/admin/unit');
const UsersPage = require('./pages/admin/users');
const UserPage = require('./pages/admin/user');
const UserAddressesPage = require('./pages/admin/useraddresses');
const UserAddressPage = require('./pages/admin/useraddress');
const UserConfirmationsPage = require('./pages/admin/userconfirmations');
const UserConfirmationPage = require('./pages/admin/userconfirmation');
const UserContactsPage = require('./pages/admin/usercontacts');
const UserContactPage = require('./pages/admin/usercontact');
const UserStatusesPage = require('./pages/admin/userstatuses');
const UserStatusPage = require('./pages/admin/userstatus');
const UserTypesPage = require('./pages/admin/usertypes');
const UserTypePage = require('./pages/admin/usertype');


class App extends React.Component {

  render() {
    return (
      <BrowserRouter>
       {/*All our Routes goes here!*/}
       <Route exact path="/" component={MainPage} />
       <Route exact path="/register" component={RegisterPage} />
       <Route exact path="/login" component={LogingPage} />
       <Route exact path="/logout" component={LogoutPage} />
       

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
      
      </BrowserRouter>
    );
  }
}

export default App;
