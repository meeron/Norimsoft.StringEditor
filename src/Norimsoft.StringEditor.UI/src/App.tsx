import type { Component } from 'solid-js';
import { Container } from 'solid-bootstrap';
import { Route, Routes } from '@solidjs/router';

import Sidebar from './components/sidebar';
import { AlertContainer } from './components/alert';
import LanguagesPage from './pages/languages/LanguagesPage';
import AppDetailsPage from './pages/app-details/AppDetailsPage';

const App: Component = () => {
  return (
    <main>
      <Sidebar />
      <Container>
        <Routes>
          <Route path='/strings/languages' component={LanguagesPage} />
          <Route path='/strings/:appSlug' component={AppDetailsPage} />
        </Routes>
      </Container>
      <AlertContainer />
    </main>
  );
};

export default App;
