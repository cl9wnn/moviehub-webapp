import React, { useRef } from "react";

interface CarouselProps {
  children: React.ReactNode;
  title?: string;
}

const Carousel: React.FC<CarouselProps> = ({ children, title }) => {
  const containerRef = useRef<HTMLDivElement>(null);
  return (
    <div className="relative mt-6">
      {title && <h2 className="text-2xl font-bold mb-6">{title}</h2>}
      <div className="relative">
        <div
          ref={containerRef}
          className="flex gap-4 overflow-x-auto scrollbar-hide scroll-smooth pr-10"
        >
          {children}
        </div>
      </div>
    </div>
  );
};

export default Carousel;
