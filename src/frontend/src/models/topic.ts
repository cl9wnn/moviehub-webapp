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