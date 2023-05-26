import type { Component } from 'solid-js';
import { createResource, For } from 'solid-js';
import { Container, Nav } from 'solid-bootstrap';
import Icon from './components/Icon';
import { getApps } from './resources/fetchers';

const App: Component = () => {
  const [apps] = createResource(getApps);

  return (
    <main>
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
          <Nav.Item>
            <Nav.Link class="text-white d-flex align-items-center">
              <Icon name="bi-plus-lg" size={18} color="#fff" />
              <span>New app</span>
            </Nav.Link>
          </Nav.Item>
          <hr></hr>
          <Nav.Item>
            <Nav.Link eventKey="languages" class="text-white">
              Languages
            </Nav.Link>
          </Nav.Item>
        </Nav>
      </div>
      <Container>
        <p>This is content</p>
      </Container>
    </main>
  );
};

export default App;
