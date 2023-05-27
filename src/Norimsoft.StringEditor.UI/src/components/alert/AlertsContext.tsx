import { createContext, ParentComponent, useContext } from 'solid-js';
import { createStore } from 'solid-js/store';

export type AlertItem = {
  id: number;
  variant: 'danger' | 'success';
  text: string;
};

type ContextValue = [
  state: AlertItem[],
  actions: {
    error: (text: string) => void;
  },
];

const AlertsContext = createContext<ContextValue>([
  [],
  {
    error: () => undefined,
  },
]);

export const AlertsProvider: ParentComponent = (props) => {
  const [state, setState] = createStore<AlertItem[]>([]);

  const error = (text: string) => {
    const id = Date.now();
    
    setState([...state, { id, variant: 'danger', text }]);
    
    setTimeout(() => {
      setState(state.filter(s => s.id !== id));
    }, 5000);
  }

  return <AlertsContext.Provider value={[state, { error }]}>{props.children}</AlertsContext.Provider>;
};

export const useAlerts = () => useContext(AlertsContext);
