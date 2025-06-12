import api from "../../utils/api.ts";
import type {MovieSearchResponse} from "../../models/movie";
import type { AxiosError } from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export const getAllMovies = async (): Promise<MovieSearchResponse[]> => {
  try {
    const response = await api.get<MovieSearchResponse[]>(`/movies/all`);
    return response.data;
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response) {
      if (error.response.status === 404 && error.response.data?.error) {
        throw new Error(error.response.data.error);
      }
      if (error.response.status === 429) {
        throw new Error("Вы превысили лимит запросов");
      }
    }

    throw new Error("Couldn't upload movies");
  }
};