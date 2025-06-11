import type {UserTopicResponse} from "./topic.ts";

export interface CommentResponse {
  id: string;
  content: string;
  createdAt: string;
  parentCommentId?: string;
  topicId: string;
  likes: number;
  user:UserTopicResponse;
  replies:CommentResponse[];
}