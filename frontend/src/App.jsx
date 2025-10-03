import { useState, useEffect } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function App() {
  const [count, setCount] = useState(0)
  const [users, setUsers] = useState([]);
  const [predictions, setPredictions] = useState([]);

  const [data, setData] = useState([]);

  useEffect(() => {
    fetchUsers();
    fetchPredictions();
  }, []);

  const fetchUsers = async () => {
    try {
      const response = await fetch("http://localhost:5290/api/users")
      const data = await response.json();
      setUsers(data);
    } catch (error) {
      console.error('Error fetching users: ', error);
    }
  }

  const fetchPredictions = async () => {
    try {
      const response = await fetch("http://localhost:5290/api/predictions")
      const data = await response.json();
      setPredictions(data);
    } catch (error) {
      console.error('Error fetching predictions: ', error);
    }
  }

  return (
    <>
      <section>
        <h2>Users</h2>
        <ul>
          {users.map(user => (
            <li key={user.id}>{user.email}</li>
          ))}
        </ul>
      </section>

      <section>
        <h2>Predictions</h2>
        <ul>
          {predictions.map(prediction => (
            <li key={prediction.id}>{prediction.created_at}</li>
          ))}
        </ul>
      </section>
    </>
  )
}

export default App
