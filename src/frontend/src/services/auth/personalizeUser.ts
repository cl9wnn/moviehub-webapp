import api from "../../utils/api.ts";
import type { AxiosError } from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export interface PersonalizeUserRequest {
  bio: string;
  genres: number[];
}

export const personalizeUser = async (data: PersonalizeUserRequest): Promise<void> => {
  try {
    await api.patch("users/personalize", data);
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response?.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Ошибка при обновлении предпочтений пользователя");
  }
};