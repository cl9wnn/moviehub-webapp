import React, {useEffect, useState} from "react";
import {getMovieById} from "../services/movies/getMovieById.ts";
import { useParams } from "react-router-dom";
import type {MovieResponse} from "../models/movie.ts";

const MoviePage: React.FC = () => {
  const { movieId } = useParams();
  const [movie, setMovie] = useState<MovieResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!movieId) {
      setError("Empty ID in Url");
      setLoading(false);
      return;
    }

    const fetchData = async () => {
      try {
        const data = await getMovieById(movieId);
        setMovie(data);
      } catch (err) {
        if (err instanceof Error) {
          setError(err.message);
        } else {
          setError("Something went wrong. Please try again.");
        }
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [movieId]);

  if (loading) return <div>Загрузка...</div>;
  if (error) return <div style={{ color: "red" }}>{error}</div>;

  return (
    <div>
      <h1>{movie?.title}</h1>
      <pre>{JSON.stringify(movie, null, 2)}</pre>
    </div>
  );
}

export default MoviePage;