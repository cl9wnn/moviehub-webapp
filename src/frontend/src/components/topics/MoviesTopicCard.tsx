import React from "react";
import type { MovieDiscussionTopicResponse } from "../../models/topic.ts";
import { useNavigate } from "react-router-dom";

interface TopicCardProps {
  topic: MovieDiscussionTopicResponse;
}

const TopicCard: React.FC<TopicCardProps> = ({ topic }) => {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/topics/${topic.id}`);
  };

  return (
    <div
      onClick={handleClick}
      className="min-w-[260px] cursor-pointer"
    >
      <div className="w-64 bg-white rounded-xl shadow border border-gray-300 overflow-hidden p-4 space-y-3">
        <h3 className="text-lg font-semibold text-gray-800 truncate">
          {topic.title}
        </h3>
        <p className="text-sm text-gray-600 line-clamp-3">{topic.content}</p>

        <div className="flex items-center gap-2">
          <img
            src={`http://${topic.user.avatarUrl}`}
            alt={topic.user.username}
            className="w-8 h-8 rounded-full object-cover"
          />
          <span className="text-sm font-medium text-gray-700">{topic.user.username}</span>
        </div>

        <div className="text-xs text-gray-400 flex justify-between pt-1 border-t border-gray-300">
          <span>{new Date(topic.createdAt).toLocaleDateString()}</span>
          <span>{topic.views} views</span>
        </div>
      </div>
    </div>
  );
};

export default TopicCard;
