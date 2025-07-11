import React, {useEffect, useState} from "react";
import {getMovieById} from "../services/movies/getMovieById.ts";
import {useNavigate, useParams} from "react-router-dom";
import type {MovieData} from "../models/movie.ts";
import MoviePoster from "../components/movies/MoviePoster.tsx";
import MovieRating from "../components/movies/MovieRating.tsx";
import MovieDescription from "../components/movies/MovieDescription.tsx";
import MovieActorsCarousel from "../components/movies/MovieActorsCarousel.tsx";
import PhotoCarousel from "../components/common/PhotosCarousel.tsx";
import MovieInfo from "../components/movies/MovieInfo.tsx";
import MovieTitle from "../components/movies/MovieTitle.tsx";
import PageWrapper from "../components/common/PageWrapper.tsx";
import Header from "../components/header/Header.tsx";
import Divider from "../components/common/Divider.tsx";
import {addToWatchlist, removeFromWatchlist} from "../services/users/toggleToWatchlist.ts";
import MoviesTopicsCarousel from "../components/topics/MoviesTopicsCarousel.tsx";
import {addToNotInterested, removeFromNotInterested} from "../services/users/toggleNotInterested.ts";

const MoviePage: React.FC = () => {
  const { movieId } = useParams();
  const [movie, setMovie] = useState<MovieData | null>(null);
  const [isInWatchList, setIsInWatchList] = useState(false);
  const [isInNotInterested, setIsInNotInterested] = useState(false);
  const [ownRating, setOwnRating] = useState<number | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const toggleWatchlist = async () => {
    if (!movieId) return;

    try {
      if (isInWatchList) {
        await removeFromWatchlist(movieId);
        setIsInWatchList(false);
      } else {
        await addToWatchlist(movieId);
        setIsInWatchList(true);
      }
    } catch (err) {
      if (err instanceof Error) {
        setError(err.message);
      } else {
        setError("Не удалось изменить статус watchlist");
      }
    }
  };

  const toggleNotInterested = async () => {
    if (!movieId) return;

    try {
      if (isInNotInterested) {
        await removeFromNotInterested(movieId);
        setIsInNotInterested(false);
      } else {
        await addToNotInterested(movieId);
        setIsInNotInterested(true);
      }
    } catch (err) {
      if (err instanceof Error) {
        setError(err.message);
      } else {
        setError("Не удалось изменить статус not interested");
      }
    }
  };

  const refreshMovieRatings = async () => {
    if (!movieId) return;

    try {
      const movieResponse = await getMovieById(movieId);
      setMovie(movieResponse.movie);
      setOwnRating(movieResponse.ownRating ?? null);
    } catch (err) {
      console.error("Ошибка обновления рейтингов:", err);
    }
  };

  useEffect(() => {
    if (!movieId) {
      setError("Empty ID in URL");
      setLoading(false);
      return;
    }

    const fetchData = async () => {
      try {
        const movieResponse = await getMovieById(movieId);
        setMovie(movieResponse.movie);
        setIsInWatchList(movieResponse.isInWatchList);
        setIsInNotInterested(movieResponse.isInNotInterested);
        setOwnRating(movieResponse.ownRating ?? null);
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
    <>
      <Header/>
      <PageWrapper>
        <div className="flex flex-col items-center mt-6">
          <div className="w-full flex flex-col lg:flex-row gap-10 justify-center">
            <MoviePoster posterUrl={movie?.posterUrl} title={movie?.title} />

            <div className="flex-1 max-w-3xl space-y-8">
              {movie && <MovieTitle
                  title={movie?.title}
                  isInWatchList={isInWatchList}
                  isInNotInterested={isInNotInterested}
                  onToggleNotInterested={toggleNotInterested}
                  onToggleWatchlist={toggleWatchlist}
              />}
              {movie && <MovieInfo movie={movie} />}
            </div>

            <MovieRating
              rating={movie?.userRating ?? null}
              count={movie?.ratingCount ?? null}
              movieId={movieId!}
              userRating={ownRating ?? null}
              onRatingSubmitted={refreshMovieRatings}
            />
          </div>

          <div className="w-full mt-5 max-w-5xl">

            {movie?.description &&  (
              <>
                <Divider/>
                <MovieDescription description={movie?.description}/>
              </>
            )}

            {(movie?.movieActors ?? []).length > 0 && (
              <>
                <Divider/>
                <div className="mt-10 mb-12">
                  <MovieActorsCarousel actors={movie?.movieActors ?? []} title="Актеры"/>
                </div>
              </>
            )}

            {(movie?.photos ?? []).length > 0 && (
              <>
                <Divider/>
                <div className="mt-10 mb-12">
                  <PhotoCarousel photos={movie?.photos ?? []}/>
                </div>
              </>
            )}

            <Divider/>
            <div className="mt-10 mb-12">
              <h2 className="text-xl font-bold mb-4">Обсуждения</h2>
              <MoviesTopicsCarousel
                topics={movie?.topics ?? []}
                onCreateTopic = {() => navigate("/create-topic")}
              />
            </div>
          </div>
        </div>
      </PageWrapper>
    </>
  );
};

export default MoviePage;