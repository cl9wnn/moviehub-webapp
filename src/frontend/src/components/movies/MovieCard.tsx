import React from "react";
import type { MovieCardResponse } from "../../models/actor.ts";
import { useNavigate } from "react-router-dom";

interface MovieCardProps {
  movie: MovieCardResponse;
}

const MovieCard: React.FC<MovieCardProps> = ({ movie }) => {
  const navigate = useNavigate();

  const handleClick = (): void => {
    navigate(`/movies/${movie.id}`);
  }
  return (
    <div
      onClick={handleClick}
      className="min-w-[210px] text-center cursor-pointer"
    >
      <div className="w-48 flex-shrink-0 bg-white rounded-lg overflow-hidden">
        <img
          src={`http://${movie.posterUrl}`}
          alt={movie.title}
          className="h-68 w-full object-cover rounded-lg"
        />
        <div className="p-2">
          <h3 className="text-m font-bold truncate">{movie.title} ({movie.year})</h3>
          <p className="text-s text-gray-500 truncate">{movie.characterName}</p>
          <div className="flex items-center justify-center">
            <span className="text-yellow-500 mr-1">★</span>
            <span className="font-semibold">{movie.userRating.toFixed(1)}</span>
            <span className="text-gray-500 text-sm ml-2">({movie.ratingCount})</span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default MovieCard;
