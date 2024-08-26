using IPLApp.Models;

namespace IPLApp.DAO
{
    public interface IRepo
    {
        Task<int> AddPlayer(Players player);
        Task<List<MatchStats>> GetMatchStatistics();
        Task<List<TopPlayer>> GetTopPlayersWithHighestEngagements();
        Task<List<MatchDetails>> GetMatchesByDateRange(DateTime startDate, DateTime endDate);
    }
}
