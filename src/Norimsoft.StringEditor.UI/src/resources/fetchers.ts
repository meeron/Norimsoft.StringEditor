import axios from 'axios';
import { App } from './contracts';

export const getApps = (onError: (err: Error) => void) => async () => {
  try {
    const res = await axios.get<App[]>('/strings/api/v1/apps1');

    return res.data;
  } catch (error: any) {
    onError(error);
    return [];
  }
};
