import api from "../../utils/api.ts";
import type { DiscussionTopicResponse } from "../../models/topic";
import type { AxiosError } from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export const getTopicById = async (id: string): Promise<DiscussionTopicResponse> => {
  try {
    const response = await api.get<DiscussionTopicResponse>(`/discussionTopics/${id}`);
    return response.data;
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response) {
      if (error.response.status === 404 && error.response.data?.error) {
        throw new Error(error.response.data.error);
      }
      if (error.response.status === 429) {
        throw new Error("Вы превысили лимит запросов");
      }
    }

    throw new Error("Couldn't upload topic");
  }
};