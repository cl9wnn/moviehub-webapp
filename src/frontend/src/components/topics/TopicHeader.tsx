import React from "react";
import { useNavigate } from "react-router-dom";
import { format } from "date-fns";
import { ru } from "date-fns/locale";
import type { ListDiscussionTopicResponse } from "../../models/topic";

interface TopicHeaderProps {
  topic: ListDiscussionTopicResponse;
}

const TopicHeader: React.FC<TopicHeaderProps> = ({ topic }) => {
  const navigate = useNavigate();

  const formatDate = (dateString: string) => {
    return format(new Date(dateString), "d MMMM yyyy, HH:mm", { locale: ru });
  };

  return (
    <div className="bg-white rounded-xl shadow border border-gray-300 p-6 mb-8">
      <div className="flex flex-col md:flex-row gap-6">
        <div className="flex-1">
          <h1 className="text-2xl font-bold mb-2">{topic.title}</h1>

          <div className="flex items-center mb-4 py-3">
            <img
              onClick={() => navigate(`/users/${topic.user.id}`)}
              src={`http://${topic.user.avatarUrl}`}
              alt={topic.user.username}
              className="w-8 h-8 rounded-full mr-2 cursor-pointer"
            />
            <span
              onClick={() => navigate(`/users/${topic.user.id}`)}
              className="font-medium mr-3 cursor-pointer hover:text-blue-600"
            >
              {topic.user.username}
            </span>
            <span className="text-gray-500 text-sm">{formatDate(topic.createdAt)}</span>
          </div>

          <div className="mb-6">
            <p className="text-gray-800 whitespace-pre-line">{topic.content}</p>
          </div>

          <div className="flex flex-wrap gap-2 mb-4">
            {topic.tags.map((tag) => (
              <span
                key={tag}
                className="bg-gray-100 text-gray-800 px-3 py-1 rounded-full text-sm"
              >
                {tag}
              </span>
            ))}
          </div>

          <div className="flex items-center text-gray-500 text-sm">
            <span className="flex items-center mr-4">
              <svg
                className="w-4 h-4 mr-1"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"
                ></path>
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z"
                ></path>
              </svg>
              {topic.views} просмотров
            </span>
          </div>
        </div>

        {topic.movie && (
          <div className="md:w-48 flex-shrink-0">
            <div
              className="rounded-lg overflow-hidden shadow-md cursor-pointer"
              onClick={() => navigate(`/movies/${topic.movie.id}`)}
            >
              <img
                src={`http://${topic.movie.posterUrl}`}
                alt={topic.movie.title}
                className="w-full h-auto"
              />
            </div>
            <h3
              className="mt-2 font-semibold text-center hover:text-blue-600 cursor-pointer"
              onClick={() => navigate(`/movie/${topic.movie.id}`)}
            >
              {topic.movie.title} ({topic.movie.year})
            </h3>
          </div>
        )}
      </div>
    </div>
  );
};

export default TopicHeader;
