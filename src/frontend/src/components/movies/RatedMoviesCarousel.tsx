import React from "react";
import Carousel from "../common/Carousel";
import RatedMovieCard from "../movies/RatedMovieCard";
import type { RatedMovieCardResponse } from "../../models/ratedMovie.ts";

interface RatedMoviesCarouselProps {
  movies: RatedMovieCardResponse[];
  title: string;
}

const RatedMoviesCarousel: React.FC<RatedMoviesCarouselProps> = ({ movies, title }) => {
  if (movies.length === 0) return null;

  return (
    <Carousel title={title}>
      {movies.map((movie) => (
          <RatedMovieCard key={movie.id} movie={movie} />
))}
  </Carousel>);
};

export default RatedMoviesCarousel;
