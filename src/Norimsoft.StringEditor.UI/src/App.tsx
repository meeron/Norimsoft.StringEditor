import type { Component } from 'solid-js';
import { Container } from 'solid-bootstrap';
import Sidebar from './components/sidebar';

const App: Component = () => {
  return (
    <main>
      <Sidebar />
      <Container>
        <p>This is content</p>
      </Container>
    </main>
  );
};

export default App;
