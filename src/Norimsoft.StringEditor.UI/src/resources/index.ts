import { Accessor, createResource } from 'solid-js';
import { useAlerts } from '../components/alert';

export function createFetcher<T>(fetcher: () => Promise<T>, onError?: (err: Error) => void) {
  const [_, alerts] = useAlerts();
  
  return createResource(async () => {
    try {
      return await fetcher();
    } catch (error: any) {
      onError?.(error);
      alerts.error(error.message);
      return undefined;
    }
  });
}

export function createMutation<TIn, TOut = unknown>(
  accessor: Accessor<TIn>,
  mutation: (data: TIn) => Promise<TOut>,
  onError?: (err: Error) => void) {
  const [_, alerts] = useAlerts();

  return createResource(accessor, async () => {
    try {
      return await mutation(accessor());
    } catch (error: any) {
      onError?.(error);
      alerts.error(error.message);
      return undefined;
    }
  });
}