using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padel_Kaverit.Middleware;
using Padel_Kaverit.Models;
using Padel_Kaverit.Services;

namespace Padel_Kaverit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly PadelContext _context;
        private readonly IGamesService _service;
        private readonly IUserAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public GamesController(PadelContext context, IUserAuthenticationService authenticationService, IUserService userService, IGamesService service)
        {
            _context = context;
            _service = service;
            _authenticationService = authenticationService;
            _userService = userService;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            return await _context.Game.ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{username}")]
        public async Task<ActionResult<GameResultsDTO>> GetGame(string username)
        { // KESKEN KESKEN KESKEN KESKEN KESKEN
            //var game = await _service.GetGamesForUser(username);
            var game = await _service.GetGameResults(username);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(long id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            if (game.player2 != null)
            {
                UserDTO user = await _userService.GetUserAsync(game.player2);
                if (user != null){
                    game.player2username = user.UserName;
                }
            }
            if (game.player3 != null)
            {
                UserDTO user = await _userService.GetUserAsync(game.player3);
                if (user != null)
                {
                    game.player3username = user.UserName;
                }
            }
            if (game.player4 != null)
            {
                UserDTO user = await _userService.GetUserAsync(game.player4);
                if (user != null)
                {
                    game.player4username = user.UserName;
                }
            }

            _context.Game.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
      
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(long id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Game.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(long id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
