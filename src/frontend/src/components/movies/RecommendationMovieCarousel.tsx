import type { RecommendationMovieResponse } from "../../models/movie.ts";
import React, { useEffect, useRef } from "react";
import { useNavigate } from "react-router-dom";

interface MovieCarouselProps {
  title: string;
  movies: RecommendationMovieResponse[];
  className?: string;
}

const MovieCarousel: React.FC<MovieCarouselProps> = ({ title, movies, className }) => {
  const scrollRef = useRef<HTMLDivElement>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const scrollContainer = scrollRef.current;
    if (!scrollContainer) return;

    const scrollStep = 260;
    const interval = setInterval(() => {
      if (!scrollContainer) return;

      const maxScroll = scrollContainer.scrollWidth - scrollContainer.clientWidth;
      const currentScroll = scrollContainer.scrollLeft;

      if (currentScroll + scrollStep >= maxScroll) {
        scrollContainer.scrollTo({ left: 0, behavior: "smooth" });
      } else {
        scrollContainer.scrollBy({ left: scrollStep, behavior: "smooth" });
      }
    }, 3000);

    return () => clearInterval(interval);
  }, [movies]);

  if (movies.length === 0) {
    return (
      <section className={`my-10 px-4 ${className ?? ""}`}>
        <div className="max-w-screen-2xl mx-auto">
          <h2 className="text-3xl font-bold mb-6 text-center">{title}</h2>
          <p className="text-center text-base text-gray-500">
            Пока рекомендаций нет! Проявляйте больше активности!
          </p>
        </div>
      </section>
    );
  }

  return (
    <section className={`my-10 px-4 ${className ?? ""}`}>
      <div className="max-w-screen-2xl mx-auto">
        <h2 className="text-3xl font-bold mb-6 text-center">{title}</h2>
        <div
          ref={scrollRef}
          className="flex overflow-x-auto space-x-4 scrollbar-hide"
        >
          {movies.map((movie) => (
            <div
              key={movie.id}
              className="w-[240px] flex-shrink-0 cursor-pointer"
              onClick={() => navigate(`/movies/${movie.id}`)}
            >
              <div className="w-full h-[360px] overflow-hidden rounded-lg shadow-md">
                <img
                  src={`http://${movie.posterUrl}`}
                  alt={movie.title}
                  className="w-full h-full object-cover"
                />
              </div>
              <h3 className="mt-2 text-lg font-semibold text-center break-words">
                {movie.title}
              </h3>
              <div className="mt-1 w-full flex items-center justify-center gap-3">
                <p className="text-base text-gray-500">{movie.year}</p>
                <p className="text-base text-yellow-500 flex items-center">
                  <span className="mr-1">★</span>
                  <span className="text-base font-semibold text-black">
                    {movie.userRating}
                  </span>
                </p>
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};

export default MovieCarousel;
