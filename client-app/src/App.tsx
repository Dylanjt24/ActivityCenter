import './App.css'
import DuckItem from './DuckItem'
import { ducks } from './duck'

function App() {

  return (
    <>
      <h1>Activity Center</h1>
      <p>React refreshes each component as needed</p>
      {ducks.map(duck => (
        <DuckItem key={duck.name} duck={duck} />
      ))}
    </>
  )
}

export default App
