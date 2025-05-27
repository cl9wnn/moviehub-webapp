import React from "react";

type MovieRatingProps = {
  rating: number | null;
  count: number | null;
};

const getRatingColor = (rating: number | null): string => {
  if (rating === null) return "text-gray-500";
  if (rating < 3) return "text-red-600";
  if (rating < 5) return "text-orange-400";
  if (rating < 7) return "text-yellow-500";
  return "text-green-600";
};

const MovieRating: React.FC<MovieRatingProps> = ({ rating, count }) => {
    const ratingColor = getRatingColor(rating);

    return (<div className="w-full lg:w-1/4 max-w-[300px]">
      <div className="bg-gray-200 p-4 rounded-xl lg:ml-8">
        <div className={`text-5xl font-bold text-center lg:text-left ${ratingColor}`}>
          {rating !== null ? rating.toFixed(1) : "-"}
        </div>
        <div className="text-gray-600 mt-1 text-center lg:text-left">{count} оценок</div>
        <button className="mt-3 px-4 py-2 bg-black text-white rounded-full hover:bg-gray-800 transition w-full lg:w-auto">
          Оценить фильм
        </button>
      </div>
    </div>
    );
};

export default MovieRating;