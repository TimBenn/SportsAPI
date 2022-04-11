using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SportsAPI.Context;
using SportsAPI.Models;

namespace SportsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ODataController
    {
        private readonly SportsContext _context;

        public PlayersController(SportsContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                return Ok(_context.Players);
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
                return Ok(SingleResult.Create(_context.Players.Where(p => p.PlayerId == key)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [EnableQuery]
        public IActionResult Post([FromBody] Player player)
        {
            try
            {
                _context.Players.Add(player);
                _context.SaveChanges();
                return Created(player);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}