import React,{useState} from "react";
import axios from "axios";
import "../index.css";

const AddPlayer = () => {
    const [player, setPlayer] = useState({
        playerName: '',
        teamId: '',
        role: '',
        age: '',
        matchesPlayed: '',
    })

    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const handleChange = (e) => {
        const {name, value} = e.target;
        setPlayer((prevState) => ({
            ...prevState,
            [name]: value
        }))
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        try{
            const response = await axios.post(`http://localhost:5170/api/Ipl/Add Player`, player);
            if (response.status === 200) {
                setSuccess('Player added successfully');
                setError('');
            } else {
                console.log("First");
                setError('Failed to add player');
                setSuccess('');
            }
            
        } catch(err) {
            console.log("Second");
            console.log(err);
            setError('Failed to add player');
            setSuccess('');
        }
    }

    return (
        <div className="add-player">
      <h2>Add New Player</h2>
      {success && <p className="success-message">{success}</p>}
      {error && <p className="error-message">{error}</p>}
      <form onSubmit={handleSubmit}>
        <label>
          Player Name:
          <input
            type="text"
            name="playerName"
            value={player.playerName}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Team ID:
          <input
            type="number"
            name="teamId"
            value={player.teamId}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Role:
          <input
            type="text"
            name="role"
            value={player.role}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Age:
          <input
            type="number"
            name="age"
            value={player.age}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Matches Played:
          <input
            type="number"
            name="matchesPlayed"
            value={player.matchesPlayed}
            onChange={handleChange}
            required
          />
        </label>
        <button type="submit">Add Player</button>
      </form>
    </div>
    )
}

export default AddPlayer;