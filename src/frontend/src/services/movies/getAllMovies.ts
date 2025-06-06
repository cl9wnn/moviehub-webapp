import api from "../../utils/api.ts";
import type {MovieData} from "../../models/movie";
import type { AxiosError } from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export const getAllMovies = async (): Promise<MovieData[]> => {
  try {
    const response = await api.get<MovieData[]>(`/movies/all`);
    return response.data;
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response && error.response.status === 404 && error.response.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Couldn't upload movies");
  }
};