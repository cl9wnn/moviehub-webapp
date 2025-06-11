import React, { useState } from "react";
import {Link, useNavigate} from "react-router-dom";
import FormWrapper from "../components/auth/FormWrapper";
import Button from "../components/auth/Button";
import { createTopic } from "../services/topics/createTopic";
import GenreTag from "../components/common/GenreTag";
import { useSearch } from "../hooks/useSearch";

const TAGS = [
  "Спойлеры", "Фан-теории", "Рецензия", "Сюжетные дыры", "Ошибки и ляпы",
  "Анализ персонажей", "Саундтрек", "Классика", "Новинки", "Недооценённое",
  "Операторская работа"
];

const TAG_ID_MAP = TAGS.reduce((acc, tag, index) => {
  acc[tag] = index + 1;
  return acc;
}, {} as Record<string, number>);

const MAX_TAGS = 3;
const MAX_TITLE_LENGTH = 128;
const MAX_CONTENT_LENGTH = 1024;

const CreateTopicPage: React.FC = () => {
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [selectedTags, setSelectedTags] = useState<string[]>([]);
  const [selectedMovie, setSelectedMovie] = useState< TopicMovie | null>(null);
  const [error, setError] = useState<string | null>(null);

  interface TopicMovie{
    id: string;
    title: string;
    year: number;
  }
  const navigate = useNavigate();

  const {
    searchQuery,
    setSearchQuery,
    filteredMovies
  } = useSearch();

  const toggleTag = (tag: string) => {
    setSelectedTags(prev =>
      prev.includes(tag)
        ? prev.filter(t => t !== tag)
        : prev.length < MAX_TAGS
          ? [...prev, tag]
          : prev
    );
  };

  const handleTitleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const text = e.target.value;
    if (text.length <= MAX_TITLE_LENGTH) {
      setTitle(text);
    }
  };

  const handleContentChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    const text = e.target.value;
    if (text.length <= MAX_CONTENT_LENGTH) {
      setContent(text);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    if (!title.trim()) {
      setError("Введите заголовок топика");
      return;
    }

    if (!content.trim()) {
      setError("Введите содержание топика");
      return;
    }

    if (!selectedMovie) {
      setError("Выберите фильм");
      return;
    }

    try {
      const result = await createTopic({
        title,
        content,
        movieId: selectedMovie.id,
        tagIds: selectedTags.map(tag => TAG_ID_MAP[tag])
      });

      navigate(`/topics/${result.id}`, { state: { successMessage: "Топик успешно создан!" } });
    } catch (err) {
      setError("Не удалось создать топик. Попробуйте позже.");
      console.error(err);
    }
  };

  return (
    <FormWrapper title="Создать обсуждение" containerMaxWidthClass="max-w-2xl">
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Заголовок</label>
          <div className="relative">
            <input
              type="text"
              value={title}
              onChange={handleTitleChange}
              maxLength={MAX_TITLE_LENGTH}
              placeholder="Введите заголовок топика..."
              className="w-full border rounded-lg p-2 pr-10"
            />
            <div className="absolute top-3 right-5 text-xs text-gray-500">
              {title.length}/{MAX_TITLE_LENGTH}
            </div>
          </div>
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Содержание</label>
          <div className="relative">
            <textarea
              value={content}
              onChange={handleContentChange}
              rows={6}
              maxLength={MAX_CONTENT_LENGTH}
              placeholder="Напишите ваши мысли..."
              className="w-full border rounded-lg p-2 pr-10"
            />
            <div
              className={`absolute bottom-3 right-5 text-xs ${
                content.length >= MAX_CONTENT_LENGTH ? "text-red-500 font-medium" : "text-gray-500"
              }`}
            >
              {content.length}/{MAX_CONTENT_LENGTH}
            </div>
          </div>
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Фильм для обсуждения</label>
          <div className="relative">
            <input
              type="text"
              placeholder="Начните вводить название фильма..."
              value={searchQuery}
              onChange={(e) => {
                setSearchQuery(e.target.value);
                if (!e.target.value) setSelectedMovie(null);
              }}
              className="w-full border rounded-lg p-2"
            />

            {selectedMovie && (
              <div className="absolute top-2 right-2">
                <button
                  type="button"
                  onClick={() => {
                    setSelectedMovie(null);
                    setSearchQuery("");
                  }}
                  className="text-gray-500 hover:text-gray-700"
                >
                  ×
                </button>
              </div>
            )}

            {searchQuery && !selectedMovie && filteredMovies.length > 0 && (
              <div
                className="absolute z-10 w-full bg-white border border-gray-200 rounded-lg shadow-lg mt-1 max-h-60 overflow-y-auto">
                {filteredMovies.map((movie) => (
                  <div
                    key={movie.id}
                    className="p-2 hover:bg-gray-100 cursor-pointer"
                    onClick={() => {
                      setSelectedMovie(movie);
                      setSearchQuery(movie.title);
                    }}
                  >
                    {movie.title} ({movie.year})
                  </div>
                ))}
              </div>
            )}
          </div>

          {selectedMovie && (
            <div className="mt-2 p-2 bg-blue-50 rounded-lg">
              <span className="font-medium">Выбранный фильм:</span> {selectedMovie.title} ({selectedMovie.year})
            </div>
          )}
        </div>

        <div>
          <p className="text-sm font-medium text-gray-700 mb-2">
            Теги (максимум {MAX_TAGS})
          </p>
          <div className="flex flex-wrap gap-2">
            {TAGS.map(tag => (
              <GenreTag
                key={tag}
                genre={tag}
                selected={selectedTags.includes(tag)}
                onClick={() => toggleTag(tag)}
              />
            ))}
          </div>
        </div>

        {error && <p className="text-sm text-red-600">{error}</p>}

        <Button type="submit">Создать</Button>

        <p className="text-center text-sm mt-2">
          <Link to="/" className="text-blue-800 hover:underline">
            На главную
          </Link>
        </p>
      </form>
    </FormWrapper>
  );
};

export default CreateTopicPage;
