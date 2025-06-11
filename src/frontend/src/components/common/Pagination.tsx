import React from "react";
import { ChevronLeft, ChevronRight } from "lucide-react";

interface PaginationProps {
  currentPage: number;
  totalPages: number;
  onPageChange: (page: number) => void;
}

const Pagination: React.FC<PaginationProps> = ({ currentPage, totalPages, onPageChange }) => {
  const handlePrev = () => {
    if (currentPage > 1) onPageChange(currentPage - 1);
  };

  const handleNext = () => {
    if (currentPage < totalPages) onPageChange(currentPage + 1);
  };

  return (
    <div className="flex justify-center items-center space-x-4">
      <button
        onClick={handlePrev}
        disabled={currentPage === 1}
        className="p-2 rounded-full border border-gray-300 bg-white hover:bg-gray-100 disabled:opacity-40 disabled:cursor-not-allowed"
        title="Назад"
      >
        <ChevronLeft />
      </button>

      <span className="text-gray-700 font-medium">
        Страница <span className="font-bold">{currentPage}</span> из {totalPages}
      </span>

      <button
        onClick={handleNext}
        disabled={currentPage === totalPages}
        className="p-2 rounded-full border border-gray-300 bg-white hover:bg-gray-100 disabled:opacity-40 disabled:cursor-not-allowed"
        title="Вперёд"
      >
        <ChevronRight />
      </button>
    </div>
  );
};

export default Pagination;