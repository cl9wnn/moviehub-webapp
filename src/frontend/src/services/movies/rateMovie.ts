import api from "../../utils/api.ts";
import type { AxiosError } from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export interface RateMovieProps {
  rating: number;
}

export const rateMovie = async (id: string, data: RateMovieProps): Promise<void> => {
  try {
    await api.post(`movies/${id}/rate`, data);
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response?.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Ошибка при добавлении оценки к фильму");
  }
};