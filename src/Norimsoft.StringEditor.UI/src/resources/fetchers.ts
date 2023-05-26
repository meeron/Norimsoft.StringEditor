import { App } from './contracts';

export async function getApps() {
  const res = await fetch('/strings/api/v1/apps');

  const apps = await res.json();

  return <App[]>apps;
}
