import React from 'react';
//import ReactDOM from 'react-dom';
import { createRoot } from "react-dom/client";
import './index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App';
import Menu from './components/Menu';
import 'bootswatch/dist/simplex/bootstrap.min.css';
import { BrowserRouter as Router } from 'react-router-dom';

const rootElement = document.getElementById("root");
const root = createRoot(rootElement);

root.render(
    <Router>
        <Menu />
        <div className='container'>
            <App />
        </div>
    </Router>,
    //document.getElementById('root')
);
