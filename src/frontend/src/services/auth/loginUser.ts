import axios, {type AxiosError} from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  token: string;
}

export async function loginUser(data: LoginRequest): Promise<LoginResponse> {
  try{
    const response = await axios.post("/api/auth/login", data);
    return response.data;
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response && error.response.status === 400 && error.response.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Login failed. Please try again later.");
  }
}