import axios from 'axios';
import { App } from './contracts';

export const getApps = () =>
  axios.get<App[]>('/strings/api/v1/apps').then(res => res.data);
