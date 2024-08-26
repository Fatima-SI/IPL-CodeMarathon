namespace IPLApp.Models
{
    public class MatchStats
    {
        public DateTime MatchDate { get; set; }
        public string Venue { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int FanEngagementCount { get; set; }
    }
}
