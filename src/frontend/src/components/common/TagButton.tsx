import React from "react";

interface TagButtonProps {
  children: React.ReactNode;
  onClick?: () => void;
  className?: string;
}

const TagButton: React.FC<TagButtonProps> = ({ children, onClick, className = "" }) => {
  return (
    <button
      onClick={onClick}
      className={`px-6 py-2 bg-gray-200 text-gray-800 rounded-full hover:bg-gray-300 transition-colors ${className}`}
    >
      {children}
    </button>
  );
};

export default TagButton;