import axios, {type AxiosError} from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
}

export interface RegisterResponse {
  token: string;
}

export async function registerUser(data: RegisterRequest): Promise<RegisterResponse> {
  try{
    const response = await axios.post("/api/users/register", data);
    return response.data;
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response && error.response.status === 400 && error.response.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Registration failed. Please try again later.");
  }
}