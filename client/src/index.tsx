import React from 'react';
import ReactDOM from 'react-dom/client';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import App from './app/layout/App';
import { store } from './app/store/configureStore';
import reportWebVitals from './reportWebVitals';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    <BrowserRouter>

      <Provider store={store}>

        <App />

      </Provider>

    </BrowserRouter>
  </React.StrictMode>
);

reportWebVitals();


