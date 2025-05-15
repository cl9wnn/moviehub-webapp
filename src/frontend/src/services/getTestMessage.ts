import api from "../utils/api.ts";

export const getTestMessage = async () => {
  const response = await api.get(`/test`);
  return response.data;
};