import { Nav, Spinner } from 'solid-bootstrap';
import { createSignal, createEffect, For, Show } from 'solid-js';
import { A } from '@solidjs/router';

import { getApps } from '../../resources/fetchers';
import NewAppNavItem from './NewAppNavItem';
import { createApp } from '../../resources/mutate';
import { createFetcher, createMutation } from '../../resources';

export default function Sidebar() {
  const [newAppName, setNewAppName] = createSignal("");
  const [apps, { refetch }] = createFetcher(getApps);
  const [newApp] = createMutation(newAppName, createApp);
  
  createEffect(() => {
    if (!newApp.loading) {
      setNewAppName("");
      void refetch();
    }
  })

  return (
    <div class='d-flex flex-column flex-shrink-0 p-3 text-white bg-dark' style='width: 280px;'>
      <a href='/' class='d-flex align-items-center mb-3 mb-md-0 me-md-auto text-white text-decoration-none'>
        <span class='fs-4'>String editor</span>
      </a>
      <hr></hr>
      <Nav variant='pills' class='flex-column mb-auto'>
        <Show when={apps.loading}>
          <Nav.Item class='d-flex justify-content-center'>
            <Spinner animation='border' variant='primary' />
          </Nav.Item>
        </Show>
        <For each={apps()}>
          {(app) => (
            <Nav.Item>
              <A href={`/strings/${app.slug}`} class='text-white nav-link'>{app.displayText}</A>
            </Nav.Item>
          )}
        </For>
        <Show when={newApp.loading}>
          <Nav.Item class='d-flex justify-content-center'>
            <Spinner animation='border' variant='primary' />
          </Nav.Item>
        </Show>
        <Show when={!newApp.loading}>
          <NewAppNavItem onEnter={setNewAppName} />
        </Show>
        <hr></hr>
        <Nav.Item>
          <A href='/strings/languages' class='text-white nav-link'>Languages</A>
        </Nav.Item>
      </Nav>
    </div>
  );
}
