import React from "react";
import {useNavigate} from "react-router-dom";
import {logoutUser} from "../../services/auth/logoutUser.ts";

import {useAuth} from "../../hooks/UseAuth";

const Header: React.FC = () => {
  const navigate = useNavigate();
  const { isAuthenticated, setIsAuthenticated } = useAuth();

  const handleSignIn = () => navigate("/login");
  const handleProfile = () => navigate("/user/:userId");

  const handleLogout = async () => {
    try{
      await logoutUser();
      setIsAuthenticated(false);
      navigate("/");
    }catch(error){
      console.error("Logout failed:", error);
    }
  }

  return (
    <header className="w-full fixed top-0 left-0 bg-gray-200 z-50">
      <div className="max-w-7xl mx-auto px-4 py-3 space-x-4 flex items-center justify-between">
        <h1 className="text-xl font-bold text-gray-800">Мое приложение</h1>
        <div>
          {isAuthenticated ? (
            <>
              <button
                onClick={handleProfile}
                className="bg-green-600 text-white px-4 py-2 rounded-xl hover:bg-green-700 transition"
              >
                Профиль
              </button>
              <button
                onClick={handleLogout}
                className="bg-red-600 text-white px-4 py-2 rounded-xl hover:bg-red-700 transition ml-2"
              >
                Logout
              </button>
            </>
          ) : (
            <button
              onClick={handleSignIn}
              className="bg-blue-600 text-white px-4 py-2 rounded-xl hover:bg-blue-700 transition"
            >
              Sign In
            </button>
          )}
        </div>
      </div>
    </header>
  );
};

export default Header;