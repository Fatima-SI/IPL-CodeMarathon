import axios from "axios";
const API_BASE_URL = 'http://localhost:5170/api/Ipl';

export const addPlayer = async (player) => {
    try {
      const response = await axios.post(`${API_BASE_URL}/Add Player`, player);
      return response.data;
    } catch (error) {
      console.error("Error adding player: ", error);
      throw error;
    }
};

export const getTopPlayers = async () => {
  try {
    const response = await axios.get(`${API_BASE_URL}/Top Players`);
    return response.data;
  } catch (error) {
    console.error("Error fetching top players: ", error);
    throw error;
  }
        
};
    
export const getMatchStats = async () => {
    try {
      const response = await axios.get(`${API_BASE_URL}/Match Stats`);
      return response.data;
    } catch (error) {
      console.error("Error fetching match statistics: ", error);
      throw error;
    }
  };
  
  
  export const getMatchesByDateRange = async (startDate, endDate) => {
      try {
        const response = await axios.get(`${API_BASE_URL}/Matches By Date Range`, {
          params: {
            startDate: startDate,
            endDate: endDate
          }
        });
        return response.data;
      } catch (error) {
        console.error("Error fetching matches by date range: ", error);
        throw error;
      }
  };