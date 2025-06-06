import React from "react";
import { useNavigate } from "react-router-dom";
import { getRatingColor } from "../../services/movies/getRatingColor";
import type { MovieData, ActorCardResponse } from "../../models/movie";
import { useAuth } from "../../hooks/UseAuth";

type Props = {
  searchQuery: string;
  searchType: "movies" | "actors";
  filteredMovies: MovieData[];
  filteredActors: ActorCardResponse[];
  onSelect: () => void;
};

export const SearchResults: React.FC<Props> = ({
                                                 searchQuery,
                                                 searchType,
                                                 filteredMovies,
                                                 filteredActors,
                                                 onSelect,
                                               }) => {
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth();

  if (!searchQuery) return null;

  if (!isAuthenticated) {
    return (
      <ul className="absolute bg-white text-black w-full mt-1 max-h-60 overflow-y-auto rounded-xl shadow-lg z-50">
        <li className="px-4 py-2 text-red-600 font-semibold">
          Для поиска необходимо войти
        </li>
      </ul>
    );
  }

  const empty = (
    <li className="px-4 py-2 text-gray-500">Ничего не найдено</li>
  );

  return (
    <ul className="absolute bg-white text-black w-full mt-1 max-h-60 overflow-y-auto rounded-xl shadow-lg z-50">
      {searchType === "movies"
        ? filteredMovies.length > 0
          ? filteredMovies.map((movie) => (
            <li
              key={movie.id}
              onClick={() => {
                onSelect();
                navigate(`/movies/${movie.id}`);
              }}
              className="px-4 py-2 hover:bg-gray-200 cursor-pointer flex justify-between"
            >
                <span>
                  🎬 {movie.title} ({movie.year})
                </span>
              <span
                className={`${getRatingColor(movie.userRating)} ml-2 font-semibold`}
              >
                  {movie.userRating?.toFixed(1) ?? "—"}
                </span>
            </li>
          ))
          : empty
        : filteredActors.length > 0
          ? filteredActors.map((actor) => (
            <li
              key={actor.id}
              onClick={() => {
                onSelect();
                navigate(`/actors/${actor.id}`);
              }}
              className="px-4 py-2 hover:bg-gray-200 cursor-pointer flex justify-between items-center"
            >
              <div className="flex items-center space-x-2">
                <img
                  src={`http://${actor.photoUrl}`}
                  alt={`${actor.firstName} ${actor.lastName}`}
                  className="w-8 h-8 rounded-full object-cover"
                />
                <span>
                  {actor.firstName} {actor.lastName}
                </span>
              </div>
            </li>
          ))
          : empty}
    </ul>
  );
};
