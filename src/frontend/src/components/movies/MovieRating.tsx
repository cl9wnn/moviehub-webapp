import React from "react";

type MovieRatingProps = {
  rating: number | null;
};

const MovieRating: React.FC<MovieRatingProps> = ({ rating }) => (
  <div className="w-full lg:w-1/4 max-w-[300px]">
    <div className="bg-gray-200 p-4 rounded-xl lg:ml-8">
      <div className="text-5xl font-bold text-yellow-600 text-center lg:text-left">
        {rating !== null ? rating.toFixed(1) : "-"}
      </div>
      <div className="text-gray-600 mt-1 text-center lg:text-left">0 оценок</div>
      <button className="mt-3 px-4 py-2 bg-black text-white rounded-full hover:bg-gray-800 transition w-full lg:w-auto">
        Оценить фильм
      </button>
    </div>
  </div>
);

export default MovieRating;