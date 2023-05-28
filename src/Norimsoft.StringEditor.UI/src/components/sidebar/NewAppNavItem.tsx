import { createSignal, Show, createEffect } from 'solid-js';
import { Nav } from 'solid-bootstrap';
import Icon from '../Icon';

export default function NewAppNavItem(props: Props) {
  let inputRef: HTMLInputElement | undefined;
  const [isEditing, setIsEditing] = createSignal(false);
  const [name, setName] = createSignal('');

  const onKeyUp = (e: KeyboardEvent) => {
    if (e.code === 'Escape') {
      setIsEditing(false);
      return;
    }

    if (e.code === 'Enter' && inputRef?.value) {
      void props.onEnter(inputRef.value);
      setIsEditing(false)
      return;
    }
  };

  createEffect(() => {
    if (!isEditing()) return;

    inputRef?.focus();
  });

  return (
    <Nav.Item>
      <Show when={!isEditing()}>
        <Nav.Link class="text-white d-flex align-items-center" onClick={() => setIsEditing(true)}>
          <Icon name="bi-plus-lg" size={18} color="#fff" />
          <span>New app</span>
        </Nav.Link>
      </Show>
      <Show when={isEditing()}>
        <input ref={inputRef} type="text" class="form-control" onKeyUp={onKeyUp} />
      </Show>
    </Nav.Item>
  );
}

type Props = {
  onEnter: (name: string) => void;
};
