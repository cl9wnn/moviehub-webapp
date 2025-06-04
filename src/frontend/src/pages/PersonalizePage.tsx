import React, {useEffect, useState} from "react";
import FormWrapper from "../components/auth/FormWrapper";
import Button from "../components/auth/Button";
import { personalizeUser } from "../services/auth/personalizeUser";
import GenreTag from "../components/common/GenreTag";
import {useLocation, useNavigate} from "react-router-dom";
import { toast } from "react-toastify";

const GENRES = [
  "Action", "Adventure", "Comedy", "Drama", "Horror", "Sci-Fi", "Fantasy", "Romance",
  "Thriller", "Crime", "Mystery", "Animation", "Documentary", "Western",
];

const GENRE_ID_MAP = GENRES.reduce((acc, genre, index) => {
  acc[genre] = index + 1;
  return acc;
}, {} as Record<string, number>);

const MAX_GENRES = 3;
const MAX_BIO_LENGTH = 256;

const PersonalizePage: React.FC = () => {
  const [bio, setBio] = useState("");
  const [selectedGenres, setSelectedGenres] = useState<string[]>([]);
  const [error, setError] = useState<string | null>(null);
  const location = useLocation();

  const navigate = useNavigate();

  useEffect(() => {
    if (location.state?.successMessage) {
      toast.success(location.state.successMessage);
    }
  }, [location.state]);

  const toggleGenre = (genre: string) => {
    setSelectedGenres((prev) =>
      prev.includes(genre)
        ? prev.filter((g) => g !== genre)
        : prev.length < MAX_GENRES
          ? [...prev, genre]
          : prev
    );
  };

  const handleBioChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    const text = e.target.value;
    if (text.length <= MAX_BIO_LENGTH) {
      setBio(text);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    if (selectedGenres.length !== MAX_GENRES) {
      setError(`Пожалуйста, выберите только ${MAX_GENRES} жанра.`);
      return;
    }

    try {
      const genres = selectedGenres.map((genre) => GENRE_ID_MAP[genre]);

      await personalizeUser({ bio,genres });
      navigate("/", { state: { successMessage: "Профиль успешно обновлён!" } });
    } catch (err) {
      setError("Failed to update profile. Please try again. " + err);
    }
  };

  const handleSkip = async () => {
      navigate("/");
  };

  return (
    <FormWrapper title="Расскажите нам о себе" stepLabel="Шаг 2">
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            О Вас
          </label>
          <div className="relative">
            <textarea
              value={bio}
              onChange={handleBioChange}
              rows={5}
              maxLength={MAX_BIO_LENGTH}
              placeholder="Расскажите немного о себе..."
              className="w-full border rounded-lg p-2 pr-10"
            />
            <div
              className={`absolute bottom-2 right-7 text-xs ${
                bio.length >= MAX_BIO_LENGTH
                  ? "text-red-500 font-medium"
                  : "text-gray-500"
              }`}
            >
              {bio.length}/{MAX_BIO_LENGTH}
            </div>
          </div>
        </div>

        <div>
          <p className="text-sm font-medium text-gray-700 mb-2">
            Выберите 3 любимых жанра
          </p>
          <div className="flex flex-wrap gap-2">
            {GENRES.map((genre) => (
              <GenreTag
                key={genre}
                genre={genre}
                selected={selectedGenres.includes(genre)}
                onClick={() => toggleGenre(genre)}
              />
            ))}
          </div>
        </div>

        {error && <p className="text-sm text-red-600">{error}</p>}

        <Button type="submit">Продолжить</Button>

        <button
          type="button"
          onClick={handleSkip}
          className="text-sm text-gray-500 underline hover:text-gray-700 block mx-auto mt-2"
        >
          Пропустить сейчас
        </button>
      </form>
    </FormWrapper>
  );
};

export default PersonalizePage;