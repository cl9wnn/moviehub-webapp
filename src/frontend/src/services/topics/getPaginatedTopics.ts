import api from "../../utils/api.ts";
import type { AxiosError } from "axios";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";
import type {ListDiscussionTopicResponse} from "../../models/topic.ts";

export interface PaginatedResponse<T> {
  items: T[];
  totalCount: number;
}

export const getPaginatedTopics = async (page: number, pageSize: number): Promise<PaginatedResponse<ListDiscussionTopicResponse>> => {
  try {
    const response = await api.get<PaginatedResponse<ListDiscussionTopicResponse>>(`/discussionTopics/paginated?page=${page}&pageSize=${pageSize}`);
    return response.data;
  } catch (err) {
    const error = err as AxiosError<ErrorResponse>;

    if (error.response && error.response.status === 404 && error.response.data?.error) {
      throw new Error(error.response.data.error);
    }

    throw new Error("Couldn't upload topics");
  }
};