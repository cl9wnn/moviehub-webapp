import React from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../hooks/UseAuth.tsx";
import { logoutUser } from "../../services/auth/logoutUser.ts";
import { getCurrentUserId } from "../../hooks/useCurrentUserId.ts";
import {useSearch} from "../../hooks/useSearch.ts";
import {SearchInput} from "./SearchInput.tsx";
import {SearchResults} from "./SearchResults.tsx";

const Header: React.FC = () => {
  const navigate = useNavigate();
  const { isAuthenticated, setIsAuthenticated } = useAuth();
  const currentUserId = getCurrentUserId();
  const {
    searchQuery,
    setSearchQuery,
    searchType,
    setSearchType,
    filteredMovies,
    filteredActors,
  } = useSearch();

  const handleLogout = async () => {
    try {
      await logoutUser();
      setIsAuthenticated(false);
      navigate("/");
    } catch (error) {
      console.error("Logout failed:", error);
    }
  };

  const handleProfile = () => navigate(`/users/${currentUserId}`);
  const handleSignIn = () => navigate("/login");

  return (
    <header className="w-full fixed top-0 left-0 bg-black z-50">
      <div className="max-w-7xl mx-auto px-4 py-3 flex items-center justify-between space-x-4">
        <button
          onClick={() => navigate("/")}
          className="bg-white text-black px-4 py-2 rounded-xl">
          На главную
        </button>

        <div className="flex-grow mx-4 relative max-w-xl">
          <SearchInput
            searchQuery={searchQuery}
            setSearchQuery={setSearchQuery}
            searchType={searchType}
            setSearchType={setSearchType}
          />
          <SearchResults
            searchQuery={searchQuery}
            searchType={searchType}
            filteredMovies={filteredMovies}
            filteredActors={filteredActors}
            onSelect={() => setSearchQuery("")}
          />
        </div>

        <div className="flex items-center space-x-2">
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
                className="bg-red-600 text-white px-4 py-2 rounded-xl hover:bg-red-700 transition"
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
