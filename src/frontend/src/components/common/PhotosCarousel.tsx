import Carousel from "./Carousel.tsx";
import React from "react";
import HorizontalPhoto from "./HorizontalPhoto.tsx";

const PhotosCarousel: React.FC<{ photos: string[] }> = ({ photos }) => (
  <Carousel title="Фотографии">
    {photos.map((url, index) => (
      <HorizontalPhoto url={url} alt={`Photo ${index + 1}`} key={index} />
    ))}
  </Carousel>
);

export default PhotosCarousel;
