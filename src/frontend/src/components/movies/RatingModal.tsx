import React, { useEffect, useState } from "react";
import { X } from "lucide-react";

type RatingModalProps = {
  isOpen: boolean;
  onClose: () => void;
  onSubmit: (rating: number) => void;
  selectedRating: number;
  setSelectedRating: (rating: number) => void;
};

const RatingModal: React.FC<RatingModalProps> = ({
                                                   isOpen,
                                                   onClose,
                                                   onSubmit,
                                                   selectedRating,
                                                   setSelectedRating,
                                                 }) => {
  const [visible, setVisible] = useState(false);

  useEffect(() => {
    if (isOpen) {
      setVisible(true);
    } else {
      const timer = setTimeout(() => setVisible(false), 200); // Убираем DOM после анимации
      return () => clearTimeout(timer);
    }
  }, [isOpen]);

  if (!visible) return null;

  return (
    <div
      className={`fixed inset-0 z-50 flex items-center justify-center
        transition-opacity duration-200
        ${isOpen ? "bg-black bg-opacity-65 opacity-100" : "opacity-0 pointer-events-none"}`}
    >
      <div
        className={`bg-white rounded-2xl shadow-lg p-6 w-96 relative transform transition-transform duration-200
          ${isOpen ? "scale-100 opacity-100" : "scale-95 opacity-0"}`}
      >
        <button
          className="absolute top-2 right-2 text-gray-600 hover:text-black"
          onClick={onClose}
        >
          <X/>
        </button>
        <h2 className="text-xl font-bold mb-4 text-center">Оцените фильм</h2>
        <div className="flex flex-col items-center mb-6">
          <div className="flex justify-center space-x-2 mb-2">
            {Array.from({length: 10}, (_, i) => i + 1).map((star) => (
              <span
                key={star}
                className={`text-3xl cursor-pointer transition-transform transform hover:scale-110 ${
                  star <= selectedRating ? "text-yellow-400" : "text-gray-300"
                }`}
                onClick={() => setSelectedRating(star)}
              >
             ★
           </span>
            ))}
          </div>
          <div className="text-lg text-gray-700">
            Ваша оценка: {selectedRating > 0 ? selectedRating : "—"}
          </div>
        </div>
        <button
          onClick={() => {
            onSubmit(selectedRating);
            onClose();
          }}
          disabled={selectedRating === 0}
          className="w-full bg-black text-white py-2 rounded-xl hover:bg-gray-800 transition disabled:opacity-50"
        >
          Сохранить
        </button>
      </div>
    </div>
  );
};

export default RatingModal;
