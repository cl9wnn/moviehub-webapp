import api from "../../utils/api.ts";
import type { AxiosError } from "axios";
import type {ActorResponse} from "../../models/actor.ts";
import type {ErrorResponse} from "../../models/ErrorReposnse.ts";

export const getActorById = async (id: string): Promise<ActorResponse> => {
  try {
    const response = await api.get<ActorResponse>(`/actors/${id}`);
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

    throw new Error("Couldn't upload actor");
  }
};