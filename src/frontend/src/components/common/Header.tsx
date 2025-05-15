import React from "react";
import {useNavigate} from "react-router-dom";
import {logoutUser} from "../../services/auth/logoutUser.ts";

const Header: React.FC = () => {
  const navigate = useNavigate();

  const handleSignUp = () => {
    navigate("/register");
  }
  const handleLogout = async () => {
    try{
      await logoutUser();
      navigate("/login");
    }catch(error){
      console.error("Logout failed:", error);
    }
  }
  return (
    <header className="w-full fixed top-0 left-0 bg-gray-200 z-50">
      <div className="max-w-7xl mx-auto px-4 py-3 space-x-4 flex items-center justify-between">
        <h1 className="text-xl font-bold text-gray-800">Мое приложение</h1>
        <div>
          <button
            onClick={handleSignUp}
            className="bg-blue-600 text-white px-4 py-2 rounded-xl hover:bg-blue-700 transition"
          >
            Sign Up
          </button>

          <button
            onClick={handleLogout}
            className="bg-blue-600 text-white px-4 py-2 rounded-xl hover:bg-blue-700 transition"
          >
            Logout
          </button>
        </div>
      </div>
    </header>
  );
};

export default Header;