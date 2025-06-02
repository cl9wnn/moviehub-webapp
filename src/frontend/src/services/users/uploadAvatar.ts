import api from "../../utils/api.ts";
import type { AxiosError } from "axios";

export interface ErrorResponse {
  error: string;
}

export const uploadAvatar = async (file: File): Promise<void> => {
  const formData = new FormData();
  formData.append("file", file);
  try{
    await api.post("users/avatar", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      }
    });
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response?.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Ошибка при загрузке аватара");
  }
};