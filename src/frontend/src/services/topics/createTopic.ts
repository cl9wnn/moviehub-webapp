import type {ErrorResponse} from "../../models/ErrorReposnse.ts";
import api from "../../utils/api.ts";
import type {AxiosError} from "axios";

export interface CreateDiscussionTopicRequest {
  title: string;
  content: string;
  movieId: string;
  tagIds: number[];
}

export interface CreateDiscussionTopicResponse {
  id: string;
}

export async function createTopic(data: CreateDiscussionTopicRequest): Promise<CreateDiscussionTopicResponse> {
  try{
    const response = await api.post("/discussionTopics/", data);
    return response.data;
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response && error.response.status === 400 && error.response.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Creating topic failed. Please try again later.");
  }
}