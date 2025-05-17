import React from 'react';
import {Routes, Route} from 'react-router-dom';
import MainPage from '../pages/MainPage';
import LoginPage from '../pages/LoginPage';
import RegisterPage from '../pages/RegisterPage';
import { ToastContainer } from 'react-toastify';
import AccountPage from "../pages/AccountPage.tsx";
import PrivateRoute from "../routes/PrivateRoute.tsx";

const App: React.FC = () => {
  return (
    <>
    <Routes>
      <Route path="/" element={<MainPage />} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage/>}/>
      <Route path="/user/:userId" element={<PrivateRoute><AccountPage/></PrivateRoute>} />
    </Routes>
  <ToastContainer position="top-right" autoClose={3000} />
    </>);
};

export default App;