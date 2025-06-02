import React from "react";
import {useNavigate} from "react-router-dom";
import {logoutUser} from "../../services/auth/logoutUser.ts";
import { getCurrentUserId } from "../../hooks/useCurrentUserId";

import {useAuth} from "../../hooks/UseAuth";

const Header: React.FC = () => {
  const navigate = useNavigate();
  const { isAuthenticated, setIsAuthenticated } = useAuth();
  const currentUserId = getCurrentUserId();

  const handleSignIn = () => navigate("/login");
  const handleProfile = () => navigate(`/users/${currentUserId}`);
  const handleMain = () => navigate("/");

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
    <header className="w-full fixed top-0 left-0 bg-black z-50">
      <div className="max-w-7xl mx-auto px-4 py-3 space-x-4 flex items-center justify-between">
        <button
          onClick={handleMain}
          className="bg-white text-black px-4 py-2 rounded-xl"
        >
          На главную
        </button>
        <h1 className="text-xl font-bold text-white">Мое приложение</h1>
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