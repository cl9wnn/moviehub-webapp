import React from "react";
import type { MovieResponse } from "../../models/movie.ts";

type MovieInfoProps = {
  movie: MovieResponse;
};


const MovieInfo : React.FC<MovieInfoProps> = ({movie}) => {
  return (
    <div className="space-y-4">
      <h2 className="text-2xl font-bold">О фильме</h2>

      <div className="space-y-3">
        <InfoRow label="Год производства" value={movie.year}/>
        <InfoRow label="Длительность" value={`${movie.durationAtMinutes} мин.`}/>
        {movie.ageRating && <InfoRow label="Возраст" value={`${movie.ageRating}+`}/>}
      </div>

      <div className="space-y-4">
        <ListRow label="Жанры">
          {movie.genres.map((genre, idx) => (
            <span key={idx} className="bg-gray-200 text-gray-800 px-3 mr-2 py-1 rounded-full text-sm">{genre}</span>
          ))}
        </ListRow>

        <ListRow label="Режиссёры">
          {movie.directors.map((d) => (
            <li key={d.id}>{d.firstName} {d.lastName}</li>
          ))}
        </ListRow>

        <ListRow label="Сценаристы">
          {movie.writers.map((w) => (
            <li key={w.id}>{w.firstName} {w.lastName}</li>
          ))}
        </ListRow>
      </div>
    </div>
);
};

const InfoRow = ({ label, value }: { label: string, value: React.ReactNode }) => (
  <div className="flex">
    <div className="w-1/3 font-semibold">{label}</div>
    <div className="w-2/3 text-gray-800 pl-5">{value}</div>
  </div>
);

const ListRow = ({ label, children }: { label: string, children: React.ReactNode }) => (
  <div className="flex items-start">
    <div className="w-1/3 font-semibold">{label}</div>
    <ul className="w-2/3 space-y-1 text-gray-700 pl-5">{children}</ul>
  </div>
);

export default MovieInfo;