import { Nav } from 'solid-bootstrap';
import { createEffect, createResource, createSignal, For } from 'solid-js';
import { getApps } from '../../resources/fetchers';
import NewAppNavItem from './NewAppNavItem';
import { createApp } from '../../resources/mutate';
import { useAlerts } from '../alert';

export default function Sidebar() {
  const [loadState, setLoadState] = createSignal(0);
  const [_, { error }] = useAlerts();
  const [apps, { refetch }] = createResource(
    loadState,
    getApps((err) => error(err.message)),
  );

  const addNewApp = async (name: string) => {
    await createApp(name);
    await refetch();
  };

  return (
    <div class="d-flex flex-column flex-shrink-0 p-3 text-white bg-dark" style="width: 280px;">
      <a href="/" class="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-white text-decoration-none">
        <span class="fs-4">String editor</span>
      </a>
      <hr></hr>
      <Nav variant="pills" class="flex-column mb-auto">
        <For each={apps()}>
          {(app) => (
            <Nav.Item>
              <Nav.Link eventKey={app.slug} class="text-white">
                {app.displayText}
              </Nav.Link>
            </Nav.Item>
          )}
        </For>
        <NewAppNavItem onSave={addNewApp} />
        <hr></hr>
        <Nav.Item>
          <Nav.Link eventKey="languages" class="text-white">
            Languages
          </Nav.Link>
        </Nav.Item>
      </Nav>
    </div>
  );
}
