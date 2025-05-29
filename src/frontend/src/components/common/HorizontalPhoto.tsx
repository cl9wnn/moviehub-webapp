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
      className="h-48 w-auto object-cover rounded-lg"
    />
  );
};

export default HorizontalPhoto;