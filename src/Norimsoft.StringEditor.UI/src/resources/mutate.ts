import axios from 'axios';
import { App } from './contracts';

export async function createApp(displayText: string) {
  const res = await axios.post<App>('/strings/api/v1/apps', { displayText });
  return res.data;
}
