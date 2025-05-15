import React from "react";

const Header: React.FC = () => {
  return (
    <header className="w-full fixed top-0 left-0 bg-gray-200 z-50">
      <div className="max-w-7xl mx-auto px-4 py-3 flex items-center justify-between">
        <h1 className="text-xl font-bold text-gray-800">Моё Приложение</h1>
        <div>
          <button className="bg-blue-600 text-white px-4 py-2 rounded-xl hover:bg-blue-700 transition">
            Sign In
          </button>
        </div>
      </div>
    </header>
  );
};

export default Header;