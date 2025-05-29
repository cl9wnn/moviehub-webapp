import React from "react";
import Carousel from "../common/Carousel";
import MovieCard from "../movies/MovieCard";
import type { MovieCardResponse } from "../../models/actor.ts";

interface ActorMoviesCarouselProps {
  movies: MovieCardResponse[];
}

const ActorMoviesCarousel: React.FC<ActorMoviesCarouselProps> = ({ movies }) => {
  if (movies.length === 0) return null;

  return (
    <Carousel title="Фильмография">
      {movies.map((movie) => (
        <MovieCard key={movie.id} movie={movie} />
      ))}
    </Carousel>
  );
};

export default ActorMoviesCarousel;
