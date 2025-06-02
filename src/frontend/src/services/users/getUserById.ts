import api from "../../utils/api.ts";
import type { UserResponse } from "../../models/user";
import type { AxiosError } from "axios";

export interface ErrorGetResponse {
  error: string;
}

export const getUserById = async (id: string): Promise<UserResponse> => {
  try {
    const response = await api.get<UserResponse>(`/users/${id}`);
    return response.data;
  } catch (err) {
    const error = err as AxiosError<ErrorGetResponse>;

    if (error.response && error.response.status === 404 && error.response.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Couldn't upload user");
  }
};