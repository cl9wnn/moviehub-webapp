import Carousel from "../common/Carousel";
import React from "react";
import type { ActorCardResponse } from "../../models/movie.ts";
import ActorCard from "../actors/ActorCard.tsx";

const MovieActorsCarousel: React.FC<{ actors: ActorCardResponse[] }> = ({ actors }) => (
  <Carousel title="Актёры">
    {actors.map((actor) => (
      <ActorCard actor={actor} />
    ))}
  </Carousel>
);

export default MovieActorsCarousel;