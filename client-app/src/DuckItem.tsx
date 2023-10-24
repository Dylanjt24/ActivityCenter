import { Duck } from "./duck"

interface Props {
    duck: Duck
}

export default function DuckItem({duck}: Props) {
  return (
    <div key={duck.name}>
        <span>{duck.name}</span>
        <button onClick={() => duck.makeSound(duck.name + ' quack')}>Make sound</button>
    </div>
  )
}
