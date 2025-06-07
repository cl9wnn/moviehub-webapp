import { useEffect, useMemo, useState } from "react";
import { getAllMovies } from "../services/movies/getAllMovies";
import type {ActorSearchResponse, MovieSearchResponse} from "../models/movie";

export const useSearch = () => {
  const [movies, setMovies] = useState<MovieSearchResponse[]>([]);
  const [searchQuery, setSearchQuery] = useState("");
  const [searchType, setSearchType] = useState<"movies" | "actors">("movies");
  const [hasFetched, setHasFetched] = useState(false);

  useEffect(() => {
    if (!searchQuery.trim() || hasFetched) return;
    setHasFetched(true);

    getAllMovies()
      .then(setMovies)
      .catch((err) => console.error("Ошибка при загрузке фильмов:", err));
  }, [searchQuery, hasFetched]);

  const filteredMovies = useMemo(
    () =>
      searchQuery.trim()
        ? movies.filter((m) =>
          m.title.toLowerCase().includes(searchQuery.toLowerCase())
        )
        : [],
    [movies, searchQuery]
  );

  const allActors = useMemo(() => {
    const map = new Map<string, ActorSearchResponse>();
    movies.forEach((m) =>
      m.actors.forEach((actor) => {
        if (!map.has(actor.id)) map.set(actor.id, actor);
      })
    );
    return Array.from(map.values());
  }, [movies]);

  const filteredActors = useMemo(() => {
    const lower = searchQuery.toLowerCase();
    return searchQuery.trim()
      ? allActors.filter((a) =>
        `${a.firstName} ${a.lastName}`.toLowerCase().includes(lower)
      )
      : [];
  }, [allActors, searchQuery]);

  return {
    searchQuery,
    setSearchQuery,
    searchType,
    setSearchType,
    filteredMovies,
    filteredActors,
  };
};
