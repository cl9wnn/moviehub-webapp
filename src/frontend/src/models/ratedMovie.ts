export interface RatedMovieCardResponse {
  id: string;
  title: string;
  year: number;
  posterUrl: string;
  ownRating: number;
  userRating: number;
}