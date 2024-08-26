using IPLApp.DAO;
using IPLApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPLApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IplController : ControllerBase
    {
        private readonly IRepo _repo;

        public IplController(IRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("Add Player")]
        public async Task<ActionResult<int>> AddPlayer(Players player)
        {
            var rowsInserted = await _repo.AddPlayer(player);
            return Ok(rowsInserted);
        }

        [HttpGet("Match Stats")]
        public async Task<ActionResult<List<MatchStats>>> GetMatchStatistics()
        {
            var result = await _repo.GetMatchStatistics();
            return Ok(result);
        }

        [HttpGet("Top Players")]
        public async Task<ActionResult<List<TopPlayer>>> GetTopPlayers()
        {
            var result = await _repo.GetTopPlayersWithHighestEngagements();
            return Ok(result);
        }

        [HttpGet("Matches By Date Range")]
        public async Task<ActionResult<List<MatchDetails>>> GetMatchesByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _repo.GetMatchesByDateRange(startDate, endDate);
            return Ok(result);
        }

    }
}
