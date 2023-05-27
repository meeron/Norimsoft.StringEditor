import { Alert } from 'solid-bootstrap';
import { For } from 'solid-js';
import styles from './AlertContainer.module.css';
import { useAlerts } from './AlertsContext';

export default function AlertContainer() {
  const [alerts] = useAlerts();

  return (
    <div class={styles.container}>
      <For each={alerts}>
        {(alert) => (
          <Alert variant={alert.variant} class={styles.alert}>
            {alert.text}
          </Alert>
        )}
      </For>
    </div>
  );
}
