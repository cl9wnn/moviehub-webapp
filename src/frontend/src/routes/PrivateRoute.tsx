import { Navigate, useLocation } from "react-router-dom";
import type {JSX} from "react";
import {useAuth} from "../hooks/UseAuth.tsx";

const PrivateRoute = ({ children }: { children: JSX.Element }) => {
  const { isAuthenticated } = useAuth();
  const location = useLocation();

  if (!isAuthenticated) {
    return <Navigate
      to="/login"
      state={{
        from: location,
        errorMessage: "Вы должны войти в систему, чтобы получить доступ к этой странице"
    }} replace />;
  }

  return children;
};

export default PrivateRoute;