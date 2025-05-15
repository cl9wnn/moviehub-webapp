import React from "react";
import {useNavigate} from "react-router-dom";

const Header: React.FC = () => {
  const navigate = useNavigate();

  const handleSignUp = () => {
    navigate("/register");
  }

  return (
    <header className="w-full fixed top-0 left-0 bg-gray-200 z-50">
      <div className="max-w-7xl mx-auto px-4 py-3 flex items-center justify-between">
        <h1 className="text-xl font-bold text-gray-800">Моё Приложение</h1>
        <div>
          <button
            onClick={handleSignUp}
            className="bg-blue-600 text-white px-4 py-2 rounded-xl hover:bg-blue-700 transition"
          >
            Sign Up
          </button>
        </div>
      </div>
    </header>
  );
};

export default Header;