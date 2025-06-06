import React, { useEffect, useState, useRef } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../hooks/UseAuth.tsx";
import { logoutUser } from "../../services/auth/logoutUser.ts";
import { getCurrentUserId } from "../../hooks/useCurrentUserId.ts";
import { useSearch } from "../../hooks/useSearch.ts";
import { SearchInput } from "./SearchInput.tsx";
import { SearchResults } from "./SearchResults.tsx";
import { Home } from "lucide-react";
import type { UserResponse } from "../../models/user.ts";
import { getUserById } from "../../services/users/getUserById.ts";

const Header: React.FC = () => {
  const navigate = useNavigate();
  const [user, setUser] = useState<UserResponse | null>(null);
  const [loading, setLoading] = useState(true);
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

  const [menuOpen, setMenuOpen] = useState(false);
  const avatarRef = useRef<HTMLDivElement>(null);

  const handleLogout = async () => {
    try {
      await logoutUser();
      setIsAuthenticated(false);
      navigate("/");
      setMenuOpen(false);
    } catch (error) {
      console.error("Logout failed:", error);
    }
  };

  const handleProfile = () => {
    navigate(`/users/${currentUserId}`);
    setMenuOpen(false);
  };

  useEffect(() => {
    if (!currentUserId) {
      console.log("User not authenticated");
      setLoading(false);
      return;
    }

    const fetchData = async () => {
      try {
        const userResponse = await getUserById(currentUserId);
        setUser(userResponse);
      } catch (err) {
        if (err instanceof Error) {
          console.log(err.message);
        } else {
          console.log("Something went wrong. Please try again.");
        }
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [currentUserId]);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (avatarRef.current && !avatarRef.current.contains(event.target as Node)) {
        setMenuOpen(false);
      }
    };
    if (menuOpen) {
      document.addEventListener("mousedown", handleClickOutside);
    } else {
      document.removeEventListener("mousedown", handleClickOutside);
    }
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [menuOpen]);

  return (
    <header className="w-full fixed top-0 left-0 bg-black z-50">
      <div className="max-w-7xl mx-auto px-4 py-3 flex items-center justify-between space-x-4">
        <div
          className="flex items-center space-x-1 cursor-pointer"
          onClick={() => navigate("/")}
        >
          <Home className="text-gray-300" />
          <span className="text-white font-bold text-2xl">MovieHub</span>
        </div>

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

        <div className="relative flex items-center space-x-2" ref={avatarRef}>
          {isAuthenticated ? (
            <>
              {!loading && user?.avatarUrl ? (
                <img
                  src={`http://${user.avatarUrl}`}
                  alt="Avatar"
                  className="w-10 h-10 rounded-full object-cover cursor-pointer"
                  onClick={() => setMenuOpen(!menuOpen)}
                />
              ) : (
                <div
                  className="w-10 h-10 rounded-full bg-gray-500 flex items-center justify-center text-white text-sm cursor-pointer select-none"
                  onClick={() => setMenuOpen(!menuOpen)}
                >
                  ?
                </div>
              )}

              {menuOpen && (
                <div
                  className="absolute right-0 top-full mt-[12px] w-40 bg-[#474747] rounded-md shadow-lg py-1 text-white text-sm z-50"
                >
                  <button
                    onClick={handleProfile}
                    className="block w-full text-left px-4 py-2 hover:bg-[#696969]"
                  >
                    Мой профиль
                  </button>
                  <button
                    onClick={handleLogout}
                    className="block w-full text-left px-4 py-2 hover:bg-[#696969]"
                  >
                    Выйти
                  </button>
                </div>
              )}
            </>
          ) : (
            <div>
              <a
                href="#"
                onClick={() => navigate("/login")}
                className="text-white text-base px-2 py-2 transition inline-block"
              >
                Войти
              </a>
              <a
                href="#"
                onClick={() => navigate("/register")}
                className="text-white text-base px-2 py-2 transition inline-block"
              >
                Зарегистрироваться
              </a>
            </div>
          )}
        </div>
      </div>
    </header>
  );
};

export default Header;
