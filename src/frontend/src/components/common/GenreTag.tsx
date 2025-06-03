import React from "react";

interface GenreTagProps {
  genre: string;
  selected: boolean;
  onClick: () => void;
}

const GenreTag: React.FC<GenreTagProps> = ({ genre, selected, onClick }) => {
  return (
    <button
      type="button"
      onClick={onClick}
      className={`px-3 py-1 rounded-full border text-sm 
        ${selected ? "bg-blue-600 text-white" : "bg-gray-100 text-gray-800"}
      `}
    >
      {genre}
    </button>
  );
};

export default GenreTag;