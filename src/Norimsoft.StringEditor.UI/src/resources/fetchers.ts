import axios from 'axios';
import { App } from './contracts';

export async function getApps() {
  const res = await axios.get<App[]>('/strings/api/v1/apps');
  return res.data;
}
