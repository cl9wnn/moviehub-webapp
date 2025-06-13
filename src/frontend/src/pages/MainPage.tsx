import React, { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import type { RecommendationMovieResponse } from "../models/movie.ts";
import { getRecommendationMoviesByRating } from "../services/recommendations/getRecommendationMoviesByRating.ts";
import { getRecommendationMoviesByUser } from "../services/recommendations/getRecommendationMoviesByUser.ts";
import Header from "../components/header/Header.tsx";
import MovieCarousel from "../components/movies/RecommendationMovieCarousel.tsx";
import {useAuth} from "../hooks/UseAuth.tsx";

const MainPage: React.FC = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth();

  const [topMovies, setTopMovies] = useState<RecommendationMovieResponse[]>([]);
  const [userMovies, setUserMovies] = useState<RecommendationMovieResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const message = location.state?.successMessage;
    if (message) {
      toast.success(message);
      navigate(location.pathname, { replace: true, state: {} });
    }
  }, [location, navigate]);

  useEffect(() => {
    const fetchMovies = async () => {
      try {
        const top = await getRecommendationMoviesByRating();
        setTopMovies(top);

        if (isAuthenticated) {
          const user = await getRecommendationMoviesByUser();
          setUserMovies(user);
        }
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

    fetchMovies();
  }, [isAuthenticated]);

  if (loading) return <div className="text-center mt-10 text-lg">Загрузка...</div>;
  if (error) return <div className="text-center mt-10 text-red-600">{error}</div>;

  return (
    <div className="bg-white min-h-screen pt-16 pb-16">
      <Header/>
      <MovieCarousel
        title="Лучшие фильмы"
        movies={topMovies}
        className="mt-12"
      />
      {isAuthenticated ? (
        <MovieCarousel
          title="Рекомендовано вам"
          movies={userMovies}
          className="mt-8 mb-0"
        />
      ) : (
        <div className="mt-8 text-center text-gray-500 text-lg">
          <h2 className="text-3xl font-bold mb-4">Рекомендовано вам</h2>
          Авторизуйтесь для доступа к персональным рекомендациям!
        </div>
      )}
    </div>
  );
};

export default MainPage;
