import { Nav, Spinner } from 'solid-bootstrap';
import { createSignal, createEffect, For, Show } from 'solid-js';
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
              <Nav.Link eventKey={app.slug} class='text-white'>
                {app.displayText}
              </Nav.Link>
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
          <Nav.Link eventKey='languages' class='text-white'>
            Languages
          </Nav.Link>
        </Nav.Item>
      </Nav>
    </div>
  );
}
