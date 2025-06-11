import type {CommentResponse} from "./comment.ts";

export interface UserDiscussionTopicResponse{
  id: string;
  title: string;
  content: string;
  views:number;
  createdAt: string;
  movie: TopicMovieResponse;
}

export interface MovieDiscussionTopicResponse{
  id: string;
  title: string;
  content: string;
  views:number;
  createdAt: string;
  user: UserTopicResponse;
}

export interface TopicMovieResponse{
  id: string;
  title: string;
  year: number;
  posterUrl: string;
}

export interface UserTopicResponse{
  id: string;
  username: string;
  avatarUrl: string;
}

export interface DiscussionTopicResponse{
  id: string;
  title: string;
  content: string;
  views:number;
  createdAt: string;
  movie:TopicMovieResponse;
  user: UserTopicResponse;
  tags: string[];
  comments: CommentResponse[];
}


export interface ListDiscussionTopicResponse{
  id: string;
  title: string;
  content: string;
  views:number;
  createdAt: string;
  movie:TopicMovieResponse;
  user: UserTopicResponse;
  tags: string[];
}

export const TAGS = [
  "Спойлеры", "Фан-теории", "Рецензия", "Сюжетные дыры", "Ошибки и ляпы",
  "Анализ персонажей", "Саундтрек", "Классика", "Новинки", "Недооценённое",
  "Операторская работа"
];