const DefaultSize = 16;
const DefaultColor = '#000';

export default function Icon(props: Props) {
  const size = props.size ?? DefaultSize;
  const color = props.color ?? DefaultColor;

  return (
    <svg class="bi" width={size} height={size} fill={color}>
      <use xlink:href={`#${props.name}`}></use>
    </svg>
  );
}

type Props = {
  name: string;
  size?: number;
  color?: string;
};
