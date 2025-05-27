import React, {useEffect, useState} from "react";
import {getMovieById} from "../services/movies/getMovieById.ts";
import { useParams } from "react-router-dom";
import type {MovieResponse} from "../models/movie.ts";
import MoviePoster from "../components/movies/MoviePoster.tsx";
import MovieRating from "../components/movies/MovieRating.tsx";
import MovieDescription from "../components/movies/MovieDescription.tsx";
import MovieActorsCarousel from "../components/movies/MovieActorsCarousel.tsx";
import MoviePhotosCarousel from "../components/movies/MoviePhotosCarousel.tsx";
import MovieInfo from "../components/movies/MovieInfo.tsx";
import MovieTitle from "../components/movies/MovieTitle.tsx";
import PageWrapper from "../components/common/PageWrapper.tsx";

const MoviePage: React.FC = () => {
  const { movieId } = useParams();
  const [movie, setMovie] = useState<MovieResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!movieId) {
      setError("Empty ID in URL");
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

  if (loading) return <div className="text-center mt-10 text-lg">Загрузка...</div>;
  if (error) return <div className="text-center mt-10 text-red-600">{error}</div>;

  return (
    <PageWrapper>
      <div className="flex flex-col items-center">
        <div className="w-full flex flex-col lg:flex-row gap-10 justify-center">
          <MoviePoster posterUrl={movie?.posterUrl} title={movie?.title} />

          <div className="flex-1 max-w-3xl space-y-8">
            {movie && <MovieTitle title={movie?.title} />}
            {movie && <MovieInfo movie={movie} />}
          </div>

          <MovieRating rating={movie?.userRating ?? null} />
        </div>

        <div className="w-full mt-5 max-w-5xl">
          {movie?.description && <MovieDescription description={movie?.description}/>}

          {(movie?.movieActors ?? []).length > 0 && (
            <div className="mt-14 mb-12">
              <MovieActorsCarousel actors={movie?.movieActors ?? []} />
            </div>
          )}

          {(movie?.photos ?? []).length > 0 && (
            <div className="mt-14 mb-12">
              <MoviePhotosCarousel photos={movie?.photos ?? []} />
            </div>
          )}
        </div>
      </div>
    </PageWrapper>
  );
};

export default MoviePage;