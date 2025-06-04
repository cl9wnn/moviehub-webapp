import api from "../../utils/api.ts";
import type { AxiosError } from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export const addToWatchlist = async (movieId: string): Promise<void> => {
  try {
    await api.post(`users/watchlist/${movieId}`);
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response?.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Ошибка при добавлении в в watchlist");
  }
};

export const removeFromWatchlist = async (movieId: string): Promise<void> => {
  try {
    await api.delete(`users/watchlist/${movieId}`);
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response?.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Ошибка при удалении из watchlist");
  }
};
