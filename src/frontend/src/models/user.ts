import type {RatedMovieCardResponse} from "./ratedMovie.ts";

export interface UserResponse {
  id: string;
  username: string;
  isCurrentUser: boolean;
  avatarUrl: string;
  registrationDate: string;
  bio: string;
  favoriteActors: ActorCardResponse[];
  watchList: MovieCardResponse[];
  movieRatings: RatedMovieCardResponse[];
}

export interface ActorCardResponse {
  id: string;
  firstName: string;
  lastName: string;
  photoUrl: string;
  characterName: string;
}

export interface MovieCardResponse {
  id: string;
  title: string;
  year: number;
  posterUrl: string;
  userRating: number;
  ratingCount: number;
  characterName: string;
}