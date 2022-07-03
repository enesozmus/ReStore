import { Container, createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { useCallback, useEffect, useState } from "react";
import { Routes, Route } from "react-router-dom";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import AboutPage from "../../features/about/AboutPage";
import { fetchCurrentUser } from "../../features/account/accountSlice";
import Login from "../../features/account/Login";
import Register from "../../features/account/Register";
import BasketPage from "../../features/basket/BasketPage";
import { fetchBasketAsync } from "../../features/basket/basketSlice";
import Catalog from "../../features/catalog/Catalog";
import ProductDetails from "../../features/catalog/ProductDetails";
import CheckoutWrapper from "../../features/checkout/CheckoutWrapper";
import ContactPage from "../../features/contact/ContactPage";
import HomePage from "../../features/home/HomePage";
import Orders from "../../features/orders/Orders";
import NotFound from "../errors/NotFound";
import { useAppDispatch } from "../store/configureStore";
import Header from "./Header";
import LoadingComponent from "./LoadingComponent";
import { PrivateRoute } from "./PrivateRoute";

function App() {

  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);

  const initApp = useCallback(async () => {
    try {
      await dispatch(fetchCurrentUser());
      await dispatch(fetchBasketAsync());
    } catch (error) {
      console.log(error);
    }
  }, [dispatch])


  // useEffect
  useEffect(() => {
    initApp().then(() => setLoading(false));
  }, [initApp])

  // Dark Mode
  const [darkMode, setDarkMode] = useState(false)
  const paletteType = darkMode ? 'dark' : 'light';

  const darkTheme = createTheme({
    palette: {
      mode: paletteType,
      background: {
        default: paletteType === 'light' ? '#eaeaea' : '#121212f'
      }
    }
  })

  function handleThemeChange() {
    setDarkMode(!darkMode);
  }

  // Loading
  if (loading) return <LoadingComponent message='Initialising app...' />

  return (
    <ThemeProvider theme={darkTheme}>
      <ToastContainer
        position="top-right"
        hideProgressBar={true}
        autoClose={5000}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
      <CssBaseline />
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange} />
      <Container>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path='/catalog' element={<Catalog />} />
          <Route path='/catalog/:id' element={<ProductDetails />} />
          <Route path='/about' element={<AboutPage />} />
          <Route path='/contact' element={<ContactPage />} />
          <Route path='/basket' element={<BasketPage />} />
          <Route
            path='/checkout'
            element={<PrivateRoute><CheckoutWrapper /></PrivateRoute>}
          />
          <Route
            path='/orders'
            element={<PrivateRoute><Orders /></PrivateRoute>}
          />
          <Route path='/login' element={<Login />} />
          <Route path='/register' element={<Register />} />
          <Route path='*' element={<NotFound />} />
        </Routes>
      </Container>
    </ThemeProvider>
  )
}

export default App;