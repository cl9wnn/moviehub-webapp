import React, { useState, useEffect } from 'react';
import {useParams, useLocation} from 'react-router-dom';
import type {DiscussionTopicResponse} from "../models/topic.ts";
import {getTopicById} from "../services/topics/getTopicById.ts";
import type {CommentResponse} from "../models/comment.ts";
import Header from "../components/header/Header.tsx";
import PageWrapper from "../components/common/PageWrapper.tsx";
import TagButton from "../components/common/TagButton.tsx";
import {createTopicComment} from "../services/comments/createTopicComment.ts";
import {getCurrentUserId} from "../hooks/useCurrentUserId.ts";
import {createReplyComment} from "../services/comments/createReplyComment.ts";
import {deleteOwnComment} from "../services/comments/deleteOwnComment.ts";
import {toast} from "react-toastify";
import {likeComment, unlikeComment} from "../services/comments/toggleLikeComment.ts";
import TopicComment from "../components/topics/TopicComment.tsx";
import TopicHeader from "../components/topics/TopicHeader.tsx";

const DiscussionTopicPage: React.FC = () => {
  const { topicId } = useParams<{ topicId: string }>();
  const [topic, setTopic] = useState<DiscussionTopicResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [commentText, setCommentText] = useState('');
  const [isSending, setIsSending] = useState(false);
  const currentUserId = getCurrentUserId();
  const [replyToComment, setReplyToComment] = useState<CommentResponse | null>(null);
  const location = useLocation();

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

  useEffect(() => {
    if (location.state?.successMessage) {
      toast.success(location.state.successMessage);
      window.history.replaceState({}, document.title);
    }
  }, [location.state]);

  const handleSendComment = async () => {
    if (!topicId || !commentText.trim()) return;
    try {
      setIsSending(true);

      if (replyToComment) {
        await createReplyComment(replyToComment.id, {content: commentText.trim()});
      } else {
        await createTopicComment(topicId, {content: commentText.trim()});
      }

      const updatedTopic = await getTopicById(topicId);
      setTopic(updatedTopic);
      setCommentText('');
      setReplyToComment(null);
    } catch (err) {
      alert(err instanceof Error ? err.message : "Ошибка при отправке комментария.");
    } finally {
      setIsSending(false);
    }
  };

  const handleDeleteComment = async (commentId: string) => {
    try {
      await deleteOwnComment(commentId);
      const updatedTopic = await getTopicById(topicId!);
      setTopic(updatedTopic);
    } catch (err) {
      alert(err instanceof Error ? err.message : "Ошибка при удалении комментария.");
    }
  };

  const handleLikeToggle = async (commentId: string) => {
    try {
      await likeComment(commentId);
    } catch (err) {
      const message = err instanceof Error ? err.message : "";
      if (message.includes("already liked")) {
        try {
          await unlikeComment(commentId);
        } catch (unlikeErr) {
          toast.error(unlikeErr instanceof Error ? unlikeErr.message : "Ошибка при удалении лайка");
          return;
        }
      } else {
        toast.error(message || "Ошибка при лайке");
        return;
      }
    }

    try {
      const updatedTopic = await getTopicById(topicId!);
      setTopic(updatedTopic);
    } catch (err) {
      toast.error(`Ошибка при обновлении комментариев после лайка: ${err}`);
    }
  };

  if (loading) return <div className="text-center mt-10 text-lg">Загрузка...</div>;
  if (error) return <div className="text-center mt-10 text-red-600">{error}</div>;

  return (
    <>
      <Header/>
      <PageWrapper backgroundClass="bg-[#d8d8d8]">
        <div className="max-w-4xl mx-auto mt-6 px-4">
          {topic && <TopicHeader topic={topic} />}

          <div className="bg-white rounded-xl shadow border border-gray-300 p-6">
            <h2 className="text-xl font-bold mb-6">
              Комментарии {topic?.comments.length ? `(${topic.comments.length})` : ''}
            </h2>

            {topic?.comments.length ? (
              topic.comments.map(comment => (
                <TopicComment
                  key={comment.id}
                  comment={comment}
                  currentUserId={currentUserId}
                  onReply={setReplyToComment}
                  onDelete={handleDeleteComment}
                  onLikeToggle={handleLikeToggle}
                />              ))
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
              <div className="flex justify-center">
                <TagButton
                  className="bg-gray-100 text-gray-800 hover:bg-gray-200 disabled:opacity-50"
                  onClick={handleSendComment}
                >
                  {isSending ? 'Отправка...' : 'Отправить'}
                </TagButton>
              </div>
            </div>
          </div>
        </div>
      </PageWrapper>
    </>
  );
};

export default DiscussionTopicPage;