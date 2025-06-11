import React from "react";
import { useNavigate } from "react-router-dom";
import { format } from "date-fns";
import { ru } from "date-fns/locale";
import type { CommentResponse } from "../../models/comment.ts";

interface CommentProps {
  comment: CommentResponse;
  currentUserId?: string | null;
  onReply: (comment: CommentResponse) => void;
  onDelete: (commentId: string) => void;
  onLikeToggle: (commentId: string) => void;
}

const formatDate = (dateString: string) =>
  format(new Date(dateString), "d MMMM yyyy, HH:mm", { locale: ru });

const TopicComment: React.FC<CommentProps> = ({
                                           comment,
                                           currentUserId,
                                           onReply,
                                           onDelete,
                                           onLikeToggle
                                         }) => {
  const navigate = useNavigate();
  const isOwn = comment.user.id === currentUserId;

  return (
    <div className={`mb-6 border-b pb-6 ${isOwn ? 'bg-gray-50 border-l-4 border-blue-500 pl-4' : ''}`}>
      <div className="flex items-start space-x-3 mb-2">
        <img
          onClick={() => navigate(`/users/${comment.user.id}`)}
          src={`http://${comment.user.avatarUrl}`}
          alt={comment.user.username}
          className="w-10 h-10 rounded-full object-cover cursor-pointer"
        />
        <div>
          <div
            onClick={() => navigate(`/users/${comment.user.id}`)}
            className="font-medium cursor-pointer hover:text-blue-600"
          >
            {comment.user.username}
          </div>
          <div className="text-sm text-gray-500">{formatDate(comment.createdAt)}</div>
        </div>
      </div>

      <div className="pl-[46px]">
        <p className="mb-3 text-gray-800">{comment.content}</p>

        <div className="flex items-center text-sm">
          <button
            onClick={() => onLikeToggle(comment.id)}
            className="flex items-center text-gray-500 hover:text-blue-600 mr-4"
          >
            <svg className="w-5 h-5 mr-1" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2"
                    d="M14 10h4.764a2 2 0 011.789 2.894l-3.5 7A2 2 0 0115.263 21h-4.017c-.163 0-.326-.02-.485-.06L7 20m7-10V5a2 2 0 00-2-2h-.095c-.5 0-.905.405-.905.905a3.61 3.61 0 01-.608 2.006L7 11v9m7-10h-2M7 20H5a2 2 0 01-2-2v-6a2 2 0 012-2h2.5"/>
            </svg>
            {comment.likes}
          </button>
          <button
            onClick={() => onReply(comment)}
            className="text-gray-500 hover:text-blue-600 mr-4"
          >
            Ответить
          </button>
          {isOwn && (
            <button
              onClick={() => onDelete(comment.id)}
              className="text-red-500 hover:text-red-700 ml-auto pr-4"
            >
              <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none"
                   viewBox="0 0 24 24" stroke="currentColor" strokeWidth="2">
                <path strokeLinecap="round" strokeLinejoin="round"
                      d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6M1 7h22M10 3h4a1 1 0 011 1v1H9V4a1 1 0 011-1z"/>
              </svg>
            </button>
          )}
        </div>
      </div>

      {comment.replies && comment.replies.length > 0 && (
        <div className="pl-[46px] mt-4 border-l-2 border-gray-200">
          {comment.replies.map(reply => (
            <TopicComment
              key={reply.id}
              comment={reply}
              currentUserId={currentUserId}
              onReply={onReply}
              onDelete={onDelete}
              onLikeToggle={onLikeToggle}
            />
          ))}
        </div>
      )}
    </div>
  );
};

export default TopicComment;
