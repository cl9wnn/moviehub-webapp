import api from "../../utils/api.ts";
import type { AxiosError } from "axios";

export interface ErrorResponse {
  error: string;
}

export const addFavoriteActor = async (actorId: string): Promise<void> => {
  try {
    await api.post(`users/favorite-actors/${actorId}`);
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response?.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Ошибка при добавлении в избранное");
  }
};

export const removeFavoriteActor = async (actorId: string): Promise<void> => {
  try {
    await api.delete(`users/favorite-actors/${actorId}`);
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response?.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Ошибка при удалении из избранного");
  }
};
