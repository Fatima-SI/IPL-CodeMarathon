import React, {useState, useEffect} from 'react';
import { getTopPlayers } from '../services/ApiUtilityService';
import "../index.css";

const TopPlayers = () => {
    const [topPlayers, setTopPlayers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const loadPlayers = async () => {
            try {
                const data = await getTopPlayers();
                setTopPlayers(data);
                setLoading(false);
            } catch (error) {
                setError(error);
            } finally {
                setLoading(false);
            }
        }
        loadPlayers();
    }, []);
    
    if (loading) return <div>Loading...</div>;
    if (error) return <div>{error}</div>;

    return (
        <div className="top-players-list">
      <h2>Top 5 Players</h2>
      <ul>
        {topPlayers.map((topPlayer) => (
          <li key={topPlayer.playerName}>
            <strong>{topPlayer.playerName}</strong> - {topPlayer.matchesPlayed} - {topPlayer.engagementCount}
          </li>
        ))}
      </ul>
    </div>
    )

} 

export default TopPlayers;