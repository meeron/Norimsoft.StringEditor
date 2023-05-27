import type { Component } from 'solid-js';
import { Container } from 'solid-bootstrap';
import Sidebar from './components/sidebar';
import { AlertContainer } from './components/alert';

const App: Component = () => {
  return (
    <main>
      <Sidebar />
      <Container>
        <p>This is content</p>
      </Container>
      <AlertContainer />
    </main>
  );
};

export default App;
