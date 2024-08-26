import React,{useState, useEffect} from "react";
import { getMatchStats } from "../services/ApiUtilityService";

import "../index.css";

const MatchStats = () => {
    const [matchStats, setMatchStats] = useState([]);
    const [error, setError] = useState('');
  
    useEffect(() => {
      const fetchMatchStatistics = async () => {
        try {
          const data = await getMatchStats();
          console.log("Fetched Match Data:", data);  
          setMatchStats(data);  
        } catch (err) {
          console.error("Failed to fetch match statistics: ", err);  
          setError('Failed to fetch match statistics.');
        }
      };
  
      fetchMatchStatistics();
    }, []);
  
    return (
      <div className="match-stats">
        <h2>Match Statistics with Fan Engagements</h2>
        {error && <p className="error-message">{error}</p>}
        <table>
          <thead>
            <tr>
              <th>Match Date</th>
              <th>Venue</th>
              <th>Team 1</th>
              <th>Team 2</th>
              <th>Total Engagements</th>
            </tr>
          </thead>
          <tbody>
            {matchStats && matchStats.length > 0 ? (
              matchStats.map((match, index) => (
                <tr key={index}>
                  <td>{match.matchDate ? new Date(match.matchDate).toLocaleDateString() : 'Invalid Date'}</td>
                  <td>{match.venue || 'N/A'}</td>
                  <td>{match.team1 || 'N/A'}</td>
                  <td>{match.team2 || 'N/A'}</td>
                  <td>{match.fanEngagementCount || 0}</td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan="5">No match stats available.</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    );
  };

  export default MatchStats;
  