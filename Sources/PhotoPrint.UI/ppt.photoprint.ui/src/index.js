const React = require('react');
const ReactDOM = require('react-dom');
import './index.css';
const App = require('./App');
const reportWebVitals = require('./reportWebVitals');

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
