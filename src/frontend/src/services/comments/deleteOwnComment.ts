import type {ErrorResponse} from "../../models/ErrorReposnse.ts";
import api from "../../utils/api.ts";
import type {AxiosError} from "axios";

export async function deleteOwnComment(id:string): Promise<void> {
  try{
    const response = await api.delete(`/comments/${id}`);
    return response.data;
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response && error.response.status === 400 && error.response.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Deleting comment failed. Please try again later.");
  }
}