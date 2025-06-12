import Header from "../components/header/Header.tsx";
import React, {useEffect} from "react";
import {useLocation, useNavigate} from "react-router-dom";
import {toast} from "react-toastify";

const MainPage: React.FC = () => {
  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    const message = location.state?.successMessage;
    if (message) {
      toast.success(message);

      navigate(location.pathname, { replace: true, state: {} });
    }
  }, [location, navigate]);

  return (
    <>
    <Header/>
    <div className="pt-20 px-4">
      <div className="text-2xl font-bold text-red-500">Hello, World</div>
    </div>
      </>
  );
};

export default MainPage;