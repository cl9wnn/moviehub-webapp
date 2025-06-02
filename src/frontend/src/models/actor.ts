export interface ActorResponse  {
  actor: ActorData;
  isFavorite: boolean;
}

export interface ActorData {
  id: string;
  firstName: string;
  lastName: string;
  biography: string;
  birthDate: string;
  photoUrl: string;
  photos: string[];
  movies: MovieCardResponse[];
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