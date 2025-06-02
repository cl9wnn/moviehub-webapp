import React from "react";
import type {MovieData} from "../../models/movie.ts";
import InfoRow from "../common/InfoRow.tsx";
import ListRow from "../common/ListRow.tsx";

type MovieInfoProps = {
  movie: MovieData;
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

export default MovieInfo;