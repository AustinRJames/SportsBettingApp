import { useState, useEffect } from 'react'
import './App.css'

function App() {
  const [users, setUsers] = useState([]);
  const [predictions, setPredictions] = useState([]);
  const [matches, setMatches] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      setLoading(true);
      await Promise.all([fetchUsers(), fetchPredictions(), fetchMatches()]);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const fetchUsers = async () => {
    const response = await fetch("http://localhost:5290/api/users");
    if (!response.ok) throw new Error('Failed to fetch users');
    const data = await response.json();
    setUsers(data);
  };

  const fetchPredictions = async () => {
    const response = await fetch("http://localhost:5290/api/predictions");
    if (!response.ok) throw new Error('Failed to fetch predictions');
    const data = await response.json();
    setPredictions(data);
  };

  const fetchMatches = async () => {
    const response = await fetch("http://localhost:5290/api/matches");
    if (!response.ok) throw new Error('Failed to fetch matches');
    const data = await response.json();
    setMatches(data);
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <div className="container">
      <h1>Sports Betting App</h1>
      
      <section className="section">
        <h2>Users ({users.length})</h2>
        {users.length === 0 ? (
          <p>No users found</p>
        ) : (
          <ul>
            {users.map(user => (
              <li key={user.id}>
                <strong>{user.firstName} {user.lastName}</strong> - {user.email}
                <br />
                <small>Points: {user.totalPoints} | Streak: {user.streak}</small>
              </li>
            ))}
          </ul>
        )}
      </section>

      <section className="section">
        <h2>Predictions ({predictions.length})</h2>
        {predictions.length === 0 ? (
          <p>No predictions found</p>
        ) : (
          <ul>
            {predictions.map(prediction => (
              <li key={prediction.id}>
                Prediction #{prediction.id} - 
                Score: {prediction.scoreA} - {prediction.scoreB} 
                {prediction.isSettled && (
                  <span className={prediction.isWin ? 'win' : 'loss'}>
                    {prediction.isWin ? ' ✓ Win' : ' ✗ Loss'}
                  </span>
                )}
                <br />
                <small>Points Bet: {prediction.pointsBet}</small>
              </li>
            ))}
          </ul>
        )}
      </section>

      <section className="section">
        <h2>Predictions ({matches.length})</h2>
        {matches.length === 0 ? (
          <p>No Matches found</p>
        ) : (
          <ul>
            {matches.map(match=> (
              <li key={match.id}>
                Match #{match.id} <br/>
                {match.teamA} <strong>{match.scoreA} : {match.scoreB}</strong> {match.teamB} <br/>
                Status: <strong>{match.status}</strong>
                <br/>
              </li>
            ))}
          </ul>
        )}
      </section>
    </div>
  );
}

export default App;