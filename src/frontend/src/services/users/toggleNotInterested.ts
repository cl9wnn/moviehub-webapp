import api from "../../utils/api.ts";
import type { AxiosError } from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export const addToNotInterested = async (movieId: string): Promise<void> => {
  try {
    await api.post(`users/not-interested/${movieId}`);
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response?.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Ошибка при добавлении в not interested");
  }
};

export const removeFromNotInterested = async (movieId: string): Promise<void> => {
  try {
    await api.delete(`users/not-interested/${movieId}`);
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response?.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Ошибка при удалении из not interested");
  }
};
