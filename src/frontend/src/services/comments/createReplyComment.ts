import type {ErrorResponse} from "../../models/ErrorReposnse.ts";
import api from "../../utils/api.ts";
import type {AxiosError} from "axios";

export interface CreateCommentRequest {
  content: string;
}

export async function createReplyComment(id:string, data: CreateCommentRequest): Promise<void> {
  try{
    const response = await api.post(`/comments/${id}/replies`, data);
    return response.data;
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response && error.response.status === 400 && error.response.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Creating reply comment failed. Please try again later.");
  }
}