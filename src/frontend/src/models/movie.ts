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

export interface MovieResponse {
  id: string;
  title: string;
  description: string;
  year: number;
  durationAtMinutes: number;
  ageRating: string;
  userRating: number;
  posterUrl: string;

  directors: PersonResponse[];
  writers: PersonResponse[];
  movieActors: ActorCardResponse[];
  genres: string[];
  photos: string[];
}