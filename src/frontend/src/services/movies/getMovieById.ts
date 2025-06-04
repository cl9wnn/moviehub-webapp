import api from "../../utils/api.ts";
import type { MovieResponse } from "../../models/movie";
import type { AxiosError } from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export const getMovieById = async (id: string): Promise<MovieResponse> => {
  try {
    const response = await api.get<MovieResponse>(`/movies/${id}`);
    return response.data;
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response && error.response.status === 404 && error.response.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Couldn't upload movie");
  }
};