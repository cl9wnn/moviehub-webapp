import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { format } from 'date-fns';
import { ru } from 'date-fns/locale';
import type {DiscussionTopicResponse} from "../models/topic.ts";
import {getTopicById} from "../services/topics/getTopicById.ts";
import type {CommentResponse} from "../models/comment.ts";
import Header from "../components/header/Header.tsx";
import PageWrapper from "../components/common/PageWrapper.tsx";
import TagButton from "../components/common/TagButton.tsx";
import {createTopicComment} from "../services/comments/createTopicComment.ts";
import {getCurrentUserId} from "../hooks/useCurrentUserId.ts";
import {createReplyComment} from "../services/comments/createReplyComment.ts";

const DiscussionTopicPage: React.FC = () => {
  const { topicId } = useParams<{ topicId: string }>();
  const [topic, setTopic] = useState<DiscussionTopicResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [commentText, setCommentText] = useState('');
  const [isSending, setIsSending] = useState(false);
  const navigate = useNavigate();
  const currentUserId = getCurrentUserId();
  const [replyToComment, setReplyToComment] = useState<CommentResponse | null>(null);

  useEffect(() => {
    if (!topicId) {
      setError("Не указан ID топика");
      setLoading(false);
      return;
    }

    const fetchTopic = async () => {
      try {
        const data = await getTopicById(topicId);
        setTopic(data);
        console.log(data);
      } catch (err) {
        setError(err instanceof Error ? err.message : "Неизвестная ошибка");
      } finally {
        setLoading(false);
      }
    };

    fetchTopic();
  }, [topicId]);

  const formatDate = (dateString: string) => {
    return format(new Date(dateString), 'd MMMM yyyy, HH:mm', { locale: ru });
  };

  const Comment: React.FC<{ comment: CommentResponse, currentUserId?: string | null }> = ({ comment, currentUserId }) => {
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
            <button className="flex items-center text-gray-500 hover:text-blue-600 mr-4">
              <svg className="w-4 h-4 mr-1" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2"
                      d="M14 10h4.764a2 2 0 011.789 2.894l-3.5 7A2 2 0 0115.263 21h-4.017c-.163 0-.326-.02-.485-.06L7 20m7-10V5a2 2 0 00-2-2h-.095c-.5 0-.905.405-.905.905a3.61 3.61 0 01-.608 2.006L7 11v9m7-10h-2M7 20H5a2 2 0 01-2-2v-6a2 2 0 012-2h2.5"></path>
              </svg>
              {comment.likes}
            </button>
            <button
              onClick={() => setReplyToComment(comment)}
              className="text-gray-500 hover:text-blue-600"
            >
              Ответить
            </button>
          </div>
        </div>

        {comment.replies && comment.replies.length > 0 && (
          <div className="pl-[46px] mt-4 border-l-2 border-gray-200">
            {comment.replies.map(reply => (
              <Comment key={reply.id} comment={reply} currentUserId={currentUserId} />
            ))}
          </div>
        )}
      </div>
    );
  };

  const handleSendComment = async () => {
    if (!topicId || !commentText.trim()) return;
    try {
      setIsSending(true);

      if (replyToComment) {
        await createReplyComment(replyToComment.id, { content: commentText.trim() });
      } else {
        await createTopicComment(topicId, { content: commentText.trim() });
      }

      const updatedTopic = await getTopicById(topicId);
      setTopic(updatedTopic);
      setCommentText('');
      setReplyToComment(null); // сбрасываем reply
    } catch (err) {
      alert(err instanceof Error ? err.message : "Ошибка при отправке комментария.");
    } finally {
      setIsSending(false);
    }
  };

  if (loading) return <div className="text-center mt-10 text-lg">Загрузка...</div>;
  if (error) return <div className="text-center mt-10 text-red-600">{error}</div>;

  return (
    <>
      <Header/>
      <PageWrapper>
        <div className="max-w-4xl mx-auto mt-6 px-4">
          <div className="bg-white rounded-xl shadow border border-gray-300 p-6 mb-8">
            <div className="flex flex-col md:flex-row gap-6">
            <div className="flex-1">
                <h1 className="text-2xl font-bold mb-2">{topic?.title}</h1>

                <div className="flex items-center mb-4 py-3">
                  <img
                    onClick={() => navigate(`/users/${topic?.user.id}`)}
                    src={`http://${topic?.user.avatarUrl}`}
                    alt={topic?.user.username}
                    className="w-8 h-8 rounded-full mr-2 cursor-pointer"
                  />
                  <span
                    onClick={() => navigate(`/users/${topic?.user.id}`)}
                    className="font-medium mr-3 cursor-pointer hover:text-blue-600"
                  >
                  {topic?.user.username}
                  </span> <span className="text-gray-500 text-sm">{topic && formatDate(topic.createdAt)}</span>
                </div>

                <div className="mb-6">
                  <p className="text-gray-800 whitespace-pre-line">{topic?.content}</p>
                </div>

                <div className="flex flex-wrap gap-2 mb-4">
                  {topic?.tags.map(tag => (
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
                    <svg className="w-4 h-4 mr-1" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"></path>
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z"></path>
                    </svg>
                    {topic?.views} просмотров
                  </span>
                </div>
              </div>

              {topic?.movie && (
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

          <div className="bg-white rounded-xl shadow border border-gray-300 p-6">
            <h2 className="text-xl font-bold mb-6">
              Комментарии {topic?.comments.length ? `(${topic.comments.length})` : ''}
            </h2>

            {topic?.comments.length ? (
              topic.comments.map(comment => (
                <Comment key={comment.id} comment={comment} currentUserId={currentUserId} />
              ))
            ) : (
              <div className="text-center text-lg py-8 text-gray-500">
                Пока нет комментариев. Будьте первым!
              </div>
            )}

            <div className="mt-8">
              <h3 className="font-medium mb-3">Добавить комментарий</h3>
              {replyToComment && (
                <div className="mb-2 text-sm text-gray-600">
                  Ответ пользователю <span className="font-medium">{replyToComment.user.username}</span>
                  <button
                    className="ml-2 text-xs text-red-500 hover:underline"
                    onClick={() => setReplyToComment(null)}
                  >
                    Отменить
                  </button>
                </div>
              )}
              <textarea
                className="w-full h-24 border border-gray-300 placeholder-gray-500 rounded-lg p-3 mb-3"
                placeholder={
                  replyToComment
                    ? `@${replyToComment.user.username}, `
                    : 'Напишите ваш комментарий...'
                }
                value={commentText}
                onChange={(e) => setCommentText(e.target.value)}
                disabled={isSending}
              />
              <TagButton
                className="bg-gray-100 text-gray-800 hover:bg-gray-200 disabled:opacity-50"
                onClick={handleSendComment}
              >
                {isSending ? 'Отправка...' : 'Отправить'}
              </TagButton>
            </div>
          </div>
        </div>
      </PageWrapper>
    </>
  );
};

export default DiscussionTopicPage;