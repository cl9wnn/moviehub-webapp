import api from "../../utils/api.ts";
import type { AxiosError } from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export const likeComment = async (commentId: string): Promise<void> => {
  try {
    await api.post(`comments/${commentId}/like`);
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response?.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Ошибка при добавлении лайка");
  }
};

export const unlikeComment = async (commentId: string): Promise<void> => {
  try {
    await api.delete(`comments/${commentId}/like`);
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response?.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Ошибка при удалении лайка");
  }
};
