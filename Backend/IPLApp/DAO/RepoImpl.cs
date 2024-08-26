using System.Data;
using System.Reflection;
using IPLApp.Models;
using Npgsql;
using NpgsqlTypes;

namespace IPLApp.DAO
{
    public class RepoImpl : IRepo
    {
        private readonly NpgsqlConnection _connection;
        public RepoImpl(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> AddPlayer(Players player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            int rowsInserted = 0;

            string insertQuery = $@"insert into ipl.players (player_name, team_id, role, age, matches_played) 
                                                     values (@PlayerName, @TeamId, @Role, @Age, @MatchesPlayed);";

            Console.WriteLine("Query: " + insertQuery);

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, _connection);

                    insertCommand.Parameters.AddWithValue("@PlayerName", player.PlayerName);
                    insertCommand.Parameters.AddWithValue("@TeamId", player.TeamId);
                    insertCommand.Parameters.AddWithValue("@Role", player.Role);
                    insertCommand.Parameters.AddWithValue("@Age", player.Age);
                    insertCommand.Parameters.AddWithValue("@MatchesPlayed", player.MatchesPlayed);

                    rowsInserted = await insertCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                throw;
            }
            return rowsInserted;
        }

        public async Task<List<MatchStats>> GetMatchStatistics()
        {
            string query = @"SELECT m.match_date, m.venue, t1.team_name AS team1, t2.team_name AS team2, 
                            COUNT(fe.engagement_id) AS fan_engagement_count
                            FROM ipl.matches m
                            JOIN ipl.teams t1 ON m.team1_id = t1.team_id
                            JOIN ipl.teams t2 ON m.team2_id = t2.team_id
                            LEFT JOIN ipl.fan_engagement fe ON m.match_id = fe.match_id
                            GROUP BY m.match_id, m.match_date, m.venue, t1.team_name, t2.team_name";

            List<MatchStats> matchStatsList = new List<MatchStats>();
            MatchStats matchStats = null;

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            matchStats = new MatchStats();
                            matchStats.MatchDate = reader.GetDateTime(0);
                            matchStats.Venue = reader.GetString(1);
                            matchStats.Team1 = reader.GetString(2);
                            matchStats.Team2 = reader.GetString(3);
                            matchStats.FanEngagementCount = reader.GetInt32(4);

                            matchStatsList.Add(matchStats);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                throw;
            }
            return matchStatsList;

        }

        public async Task<List<TopPlayer>> GetTopPlayersWithHighestEngagements()
        {
            string query = @"SELECT p.player_name, p.matches_played, SUM(fe.engagement_id) AS engagement_count
                             FROM ipl.players p
                             JOIN ipl.matches m ON p.team_id = m.team1_id OR p.team_id = m.team2_id
                             LEFT JOIN ipl.fan_engagement fe ON m.match_id = fe.match_id
                             GROUP BY p.player_name, p.matches_played
                             ORDER BY engagement_count DESC
                             LIMIT 5";

            List<TopPlayer> topPlayersList = new List<TopPlayer>();
            TopPlayer topPlayer = null;

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            topPlayer = new TopPlayer();
                            topPlayer.PlayerName = reader.GetString(0);
                            topPlayer.MatchesPlayed = reader.GetInt32(1);
                            topPlayer.EngagementCount = reader.GetInt32(2);

                            topPlayersList.Add(topPlayer);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                throw;
            }
            return topPlayersList;
        }

        public async Task<List<MatchDetails>> GetMatchesByDateRange(DateTime startDate, DateTime endDate)
        {
            string query = @"SELECT m.match_date, m.venue, t1.team_name AS team1, t2.team_name AS team2, 
                            COALESCE(tw.team_name, 'None') AS winner
                             FROM ipl.matches m
                             JOIN ipl.teams t1 ON m.team1_id = t1.team_id
                             JOIN ipl.teams t2 ON m.team2_id = t2.team_id
                             LEFT JOIN ipl.teams tw ON m.winner_team_id = tw.team_id
                             WHERE m.match_date BETWEEN @StartDate AND @EndDate";

            List<MatchDetails> matchDetailsList = new List<MatchDetails>();
            MatchDetails matchDetails = null;

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(query, _connection);

                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            matchDetails = new MatchDetails();
                            matchDetails.MatchDate = reader.GetDateTime(0);
                            matchDetails.Venue = reader.GetString(1);
                            matchDetails.Team1 = reader.GetString(2);
                            matchDetails.Team2 = reader.GetString(3);
                            matchDetails.Winner = reader.IsDBNull(4) ? "None" : reader.GetString(4);

                            matchDetailsList.Add(matchDetails);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                throw;
            }
            return matchDetailsList;
        }

        
    }
}
