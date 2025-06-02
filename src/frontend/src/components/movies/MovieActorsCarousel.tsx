import Carousel from "../common/Carousel";
import React from "react";
import type { ActorCardResponse } from "../../models/movie.ts";
import ActorCard from "../actors/ActorCard.tsx";

export interface MovieActorsCarouselProps {
  actors: ActorCardResponse[];
  title: string;
}

const MovieActorsCarousel: React.FC<MovieActorsCarouselProps> = ({ actors, title }) => (
  <Carousel title={title}>
    {actors.map((actor) => (
      <ActorCard actor={actor} />
    ))}
  </Carousel>
);

export default MovieActorsCarousel;