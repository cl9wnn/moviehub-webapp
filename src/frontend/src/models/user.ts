
export interface UserResponse {
  id: string;
  username: string;
  isCurrentUser: boolean;
  favoriteActors: ActorCardResponse[];
  watchList: MovieCardResponse[];
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