import api from "../utils/api.ts";

export const getTestProtectedResource = async () => {
  const response = await api.get(`/test`);
  return response.data;
};