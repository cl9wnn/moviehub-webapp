import axios, {type AxiosError} from "axios";

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
}

export interface ErrorRegisterResponse {
  error: string;
}

export async function registerUser(data: RegisterRequest): Promise<void> {
  try{
    await axios.post("/api/users/register", data);
  } catch (err) {
    const error = err as AxiosError<ErrorRegisterResponse>;

    if (error.response && error.response.status === 400 && error.response.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Registration failed. Please try again later.");
  }
}