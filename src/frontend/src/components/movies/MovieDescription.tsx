import React from "react";

const MovieDescription: React.FC<{ description: string }> = ({ description }) => (
  <div className="mt-10">
    <h2 className="text-2xl font-bold mb-4">Описание</h2>
    <p className="text-gray-800 leading-relaxed text-base">{description}</p>
  </div>
);

export default MovieDescription;