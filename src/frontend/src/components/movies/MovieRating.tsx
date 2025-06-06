import React, {useState} from "react";
import RatingModal from "./RatingModal.tsx";
import {rateMovie} from "../../services/movies/rateMovie.ts";
import {getRatingColor} from "../../services/movies/getRatingColor.ts";

type MovieRatingProps = {
  rating: number | null;
  count: number | null;
  movieId: string;
  userRating: number | null;
  onRatingSubmitted?: () => Promise<void>;
};

const MovieRating: React.FC<MovieRatingProps> = ({ rating, count, movieId, userRating, onRatingSubmitted }) => {
  const ratingColor = getRatingColor(rating);
  const userRatingColor = getRatingColor(userRating);
  const [showModal, setShowModal] = useState(false);
  const [selectedRating, setSelectedRating] = useState(0);
  const [error, setError] = useState<string | null>(null);

  const handleRatingSubmit = async (ratingValue: number) => {
    try {
      await rateMovie(movieId, { rating: ratingValue });
      setSelectedRating(ratingValue);
      setShowModal(false);
      setError(null);

      if (onRatingSubmitted) {
        await onRatingSubmitted();
      }
    } catch (err) {
      if (err instanceof Error) {
        setError(err.message);
      } else {
        setError("Не удалось отправить оценку.");
      }
    }
  };

  return (
    <div className="w-full lg:w-1/4 max-w-[240px]">
      <div className="bg-gray-200 p-5 rounded-xl shadow-sm text-center">
        <div className={`text-5xl font-bold ${ratingColor}`}>
          {rating !== null ? rating.toFixed(1) : "-"}
        </div>
        <div className="text-s text-gray-600 mt-1">{count} оценок</div>

        {userRating !== null && (
          <div className="mt-4">
            <div className={`text-2xl font-semibold ${userRatingColor}`}>
              {userRating}
            </div>
            <div className="text-sm text-gray-500">Ваша оценка</div>
          </div>
        )}

        <button
          className="mt-4 px-4 py-2 bg-black text-white rounded-full hover:bg-gray-800 transition w-full"
          onClick={() => setShowModal(true)}
        >
          Оценить фильм
        </button>

        {error && <div className="text-s text-red-600 mt-2">{error}</div>}
      </div>

      <RatingModal
        isOpen={showModal}
        onClose={() => setShowModal(false)}
        onSubmit={handleRatingSubmit}
        selectedRating={selectedRating}
        setSelectedRating={setSelectedRating}
      />
    </div>
  );
};

export default MovieRating;