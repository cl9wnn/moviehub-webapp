import type {MovieDiscussionTopicResponse} from "./topic.ts";

export interface PersonResponse {
  id: string;
  firstName: string;
  lastName: string;
}

export interface ActorCardResponse {
  id: string;
  firstName: string;
  lastName: string;
  photoUrl: string;
  characterName: string;
}

export interface MovieData {
  id: string;
  title: string;
  description: string;
  year: number;
  durationAtMinutes: number;
  ageRating: string;
  userRating: number;
  ratingCount: number;
  posterUrl: string;
  directors: PersonResponse[];
  writers: PersonResponse[];
  movieActors: ActorCardResponse[];
  topics:MovieDiscussionTopicResponse[];
  genres: string[];
  photos: string[];
}

export interface MovieSearchResponse {
  id: string;
  title: string;
  year: number;
  userRating: number;
  actors: ActorSearchResponse[];
}

export interface ActorSearchResponse {
  id: string;
  firstName: string;
  lastName: string;
  photoUrl: string;
}

export interface MovieResponse {
  movie: MovieData;
  isInWatchList: boolean;
  ownRating?: number;
}

