using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using SportsAPI.Context;
using SportsAPI.Models;
using Microsoft.AspNetCore.OData.Results;

namespace SportsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : ODataController
    {
        private readonly SportsContext _context;

        public TeamsController(SportsContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            try { 
                return Ok(_context.Teams);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [EnableQuery]
        public IActionResult Get([FromODataUri] int key)
        {
            try
            {
                return Ok(SingleResult.Create(_context.Teams.Where(c => c.TeamId == key)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [EnableQuery]
        public IActionResult Post([FromBody] Team team)
        {
            try
            {
                _context.Teams.Add(team);
                _context.SaveChanges();
                return Ok(Created(team));
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/v{version}/Teams/{teamId}/Assign/{playerId}")]
        public IActionResult Assign(int teamId, int playerId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Missing required parameters");

                var player = _context.Players.Find(playerId);

                if (player == null) return NotFound("Player does not exist");

                if (player.TeamId != null && player.TeamId != teamId) return BadRequest("Player already belongs to a team");

                if (player.TeamId != null && player.TeamId == teamId) return BadRequest("Player is already on this team");

                var team = _context.Teams.Include(t => t.Players).FirstOrDefault(t => t.TeamId == teamId);

                if (team == null) return BadRequest("Team does not exist");

                if (team.Players.Count == 8) return BadRequest("Team cannot have more than 8 players");

                team.Players.Add(player);

                _context.SaveChanges();

                return Ok(Updated(team));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/v{version}/Teams/{teamId}/Unassign/{playerId}")]
        public IActionResult Unassign(int teamId, int playerId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Missing required parameters");

                var player = _context.Players.Find(playerId);

                if (player == null) return NotFound("Player does not exist");

                if (player.TeamId != null && player.TeamId != teamId) return BadRequest("Player is not on this team");

                var team = _context.Teams.Include(t => t.Players).FirstOrDefault(t => t.TeamId == teamId);

                if (team == null) return BadRequest("Team does not exist");

                team.Players.Remove(player);

                _context.SaveChanges();

                return Ok(Updated(team));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}