import React from "react";
import type { UserDiscussionTopicResponse } from "../../models/topic.ts";
import { useNavigate } from "react-router-dom";

interface TopicCardProps {
  topic: UserDiscussionTopicResponse;
}

const UsersTopicCard: React.FC<TopicCardProps> = ({ topic }) => {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/topics/${topic.id}`);
  };

  return (
    <div
      onClick={handleClick}
      className="min-w-[250px] text-left cursor-pointer"
    >
      <div className="w-64 flex-shrink-0 bg-white rounded-lg shadow border border-gray-300 overflow-hidden">
        <img
          src={`http://${topic.movie.posterUrl}`}
          alt={topic.movie.title}
          className="h-40 w-full object-cover"
        />
        <div className="p-3">
          <h3 className="text-base font-bold truncate">{topic.title}</h3>
          <p className="text-xs text-gray-500 mt-2 truncate">
            {topic.movie.title} ({topic.movie.year})
          </p>
          <div className="text-xs text-gray-400 flex justify-between mt-1">
            <span>{new Date(topic.createdAt).toLocaleDateString()}</span>
            <span>{topic.views} views</span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default UsersTopicCard;
