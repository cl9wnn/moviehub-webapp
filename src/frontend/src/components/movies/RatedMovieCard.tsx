import React from "react";
import type { RatedMovieCardResponse } from "../../models/ratedMovie.ts";
import { useNavigate } from "react-router-dom";

interface RatedMovieCardProps {
  movie: RatedMovieCardResponse;
}

const RatedMovieCard: React.FC<RatedMovieCardProps> = ({ movie }) => {
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
          className="h-[280px] w-full object-cover rounded-lg"
        />
        <div className="p-2">
          <h3 className="text-m font-bold truncate">{movie.title} ({movie.year})</h3>
          <div className="flex items-center justify-center space-x-4">
            <div className="flex items-center">
              <span className="text-yellow-500">★</span>
              <span className="font-semibold ml-1">{movie.userRating}</span>
            </div>
            <div className="flex items-center">
              <span className="text-blue-500">★</span>
              <span className="font-semibold ml-1">{movie.ownRating}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default RatedMovieCard;
