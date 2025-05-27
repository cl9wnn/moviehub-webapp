import React from "react";

interface MoviePhotoProps {
  url: string;
  alt?: string;
}

const HorizontalPhoto: React.FC<MoviePhotoProps> = ({ url, alt }) => {
  return (
    <img
      src={`http://${url}`}
      alt={alt ?? "Movie photo"}
      className="min-w-[304px] h-48 object-cover rounded-lg"
    />
  );
};

export default HorizontalPhoto;