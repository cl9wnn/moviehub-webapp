import React from "react";

type MoviePosterProps = {
  posterUrl?: string;
  title?: string;
};

const MoviePoster: React.FC<MoviePosterProps> = ({ posterUrl, title }) => (
  <div className="w-full lg:w-1/3 max-w-[300px] ml-10">
    <img
      src={`http://${posterUrl}`}
      alt={title}
      className="w-full rounded-xl object-cover max-h-[600px]"
    />
  </div>
);

export default MoviePoster;