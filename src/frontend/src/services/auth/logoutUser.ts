import api from "../../utils/api.ts"
import type {AxiosError} from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export async function logoutUser():Promise<void> {
  try{
    await api.post("auth/revoke");
  }catch(err){
    const error = err as AxiosError<ErrorResponse>;

    if (error.response && error.response.status === 400 && error.response.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("LogOut failed. Please try again later.");
  } finally {
    localStorage.removeItem("accessToken");
  }
}