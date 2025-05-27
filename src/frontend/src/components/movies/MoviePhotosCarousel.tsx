import Carousel from "../common/Carousel";
import React from "react";
import HorizontalPhoto from "../common/HorizontalPhoto.tsx";

const MoviePhotosCarousel: React.FC<{ photos: string[] }> = ({ photos }) => (
  <Carousel title="Фотографии">
    {photos.map((url, index) => (
      <HorizontalPhoto url={url} alt={`Photo ${index + 1}`} key={index} />
    ))}
  </Carousel>
);

export default MoviePhotosCarousel;
