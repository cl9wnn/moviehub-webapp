import axios from 'axios';

export const getTestMessage = async () => {
  const response = await axios.get(`/api/test`);
  return response.data;
};