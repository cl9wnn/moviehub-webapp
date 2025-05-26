import React, {useEffect, useState} from "react";
import {getMovieById} from "../services/movies/getMovieById.ts";
import { useParams } from "react-router-dom";
import type {MovieResponse} from "../models/movie.ts";
import Carousel from "../components/common/Carousel.tsx";

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
    <div className="max-w-6xl mx-auto p-6 bg-white">
      <div className="flex flex-col items-center">
        {/* Основные колонки */}
        <div className="w-full flex flex-col lg:flex-row gap-10 justify-center">
          {/* Левый столбец - постер */}
          <div className="w-full lg:w-1/3 max-w-[300px] ml-10">
            <img
              src={`http://${movie?.posterUrl}`}
              alt={movie?.title}
              className="w-full rounded-xl object-cover max-h-[600px]"
            />
          </div>

          {/* Центральная колонка */}
          <div className="flex-1 max-w-3xl space-y-8">
            <div className="space-y-4">
              <h1 className="text-5xl font-bold">{movie?.title}</h1>
              <div className="flex gap-4 flex-wrap">
                <button className="px-6 py-2 bg-gray-200 text-gray-800 rounded-full hover:bg-gray-300 transition-colors">
                  Буду смотреть
                </button>
                <button className="px-6 py-2 bg-gray-200 text-gray-800 rounded-full hover:bg-gray-300 transition-colors">
                  Неинтересно
                </button>
              </div>
            </div>

            <div className="space-y-4">
              <h2 className="text-2xl font-bold">О фильме</h2>

              <div className="space-y-3">
                <div className="flex">
                  <div className="w-1/3 font-semibold">Год производства</div>
                  <div className="w-2/3 text-gray-800">{movie?.year}</div>
                </div>

                <div className="flex">
                  <div className="w-1/3 font-semibold">Длительность</div>
                  <div className="w-2/3 text-gray-800">{movie?.durationAtMinutes} мин.</div>
                </div>

                {movie?.ageRating && (
                  <div className="flex">
                    <div className="w-1/3 font-semibold">Возраст</div>
                    <div className="w-2/3 text-gray-800">{movie.ageRating}+</div>
                  </div>
                )}
              </div>

              <div className="space-y-4">
                <div className="flex items-start">
                  <div className="w-1/3 font-semibold">Жанры</div>
                  <div className="w-2/3 flex flex-wrap gap-2">
                    {movie?.genres.map((genre, idx) => (
                      <span key={idx} className="bg-gray-200 text-gray-800 px-3 py-1 rounded-full text-sm">
                      {genre}
                    </span>
                    ))}
                  </div>
                </div>

                <div className="flex items-start">
                  <div className="w-1/3 font-semibold">Режиссёры</div>
                  <ul className="w-2/3 space-y-1 text-gray-700">
                    {movie?.directors.map((d) => (
                      <li key={d.id}>{d.firstName} {d.lastName}</li>
                    ))}
                  </ul>
                </div>

                <div className="flex items-start">
                  <div className="w-1/3 font-semibold">Сценаристы</div>
                  <ul className="w-2/3 space-y-1 text-gray-700">
                    {movie?.writers.map((w) => (
                      <li key={w.id}>{w.firstName} {w.lastName}</li>
                    ))}
                  </ul>
                </div>
              </div>
            </div>
          </div>

          {/* Правый столбец - рейтинг */}
          <div className="w-full lg:w-1/4 max-w-[300px]">
            <div className="bg-gray-200 p-4 rounded-xl lg:ml-8">
              <div className="text-5xl font-bold text-yellow-600 text-center lg:text-left">
                {movie?.userRating?.toFixed(1) ?? "-"}
              </div>
              <div className="text-gray-600 mt-1 text-center lg:text-left">{0} оценок</div>
              <button className="mt-3 px-4 py-2 bg-black text-white rounded-full hover:bg-gray-800 transition w-full lg:w-auto">
                Оценить фильм
              </button>
            </div>
          </div>
        </div>

        {/* Нижние секции */}
        <div className="w-full mt-5 max-w-5xl">
          {/* Новый раздел с описанием */}
          {movie?.description && (
            <div className="mt-10">
              <h2 className="text-2xl font-bold mb-4">Описание</h2>
              <p className="text-gray-800 leading-relaxed text-base">
                {movie.description}
              </p>
            </div>
          )}

          <div className="my-14">
            <Carousel title="Актёры">
              {movie?.movieActors.map((actor) => (
                <div key={actor.id} className="min-w-[210px] text-center">
                  <div className="relative w-full pt-[135%] overflow-hidden rounded-lg">
                    <img
                      src={`http://${actor.photoUrl}`}
                      alt={`${actor.firstName} ${actor.lastName}`}
                      className="absolute top-0 left-0 w-full h-full object-cover"
                    />
                  </div>
                  <div className="mt-3 text-base font-semibold">{actor.firstName} {actor.lastName}</div>
                  <div className="text-sm text-gray-600">"{actor.characterName}"</div>
                </div>
              ))}
            </Carousel>
          </div>

            {/* Фотографии */}
            {(movie?.photos ?? []).length > 0 && (
              <div className="mt-14 mb-12">
                <Carousel title="Фотографии">
                  {movie?.photos.map((url, index) => (
                    <img
                      key={index}
                      src={`http://${url}`}
                      alt={`Фото ${index + 1}`}
                      className="min-w-[304px] h-48 object-cover rounded-lg"
                    />
                  ))}
                </Carousel>
              </div>
            )}
          </div>
        </div>
      </div>
      );
      };

      export default MoviePage;