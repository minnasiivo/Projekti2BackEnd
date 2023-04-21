using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// Gets list of users games
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;
            var games = await _service.GetGamesForUser(username);
            return Ok(games);
       
        }

        // GET: api/Games/5
        /// <summary>
        /// Gets game results for user
        /// </summary>
        /// <param name="name">user's username</param>
        /// <returns>game statistics as json</returns>
        /// <response code="404">game results not found</response>
        [HttpGet("{username}")]
        [Authorize]
        public async Task<ActionResult<GameResultsDTO>> GetGame(string username)
        { 
          
            var game = await _service.GetGameResults(username);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }






        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        ///change game information
        /// </summary>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutGame(Game game)
        {
            Game updatedGame = await _service.UpdateGameInfoAsync(game);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add info of a new game to database
        /// </summary>
        [HttpPost]
        [Authorize]
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
        /// <summary>
        /// Delete game results for user
        /// </summary>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteGame(Game game)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;

            if (username == game.owner)
            {
                _context.Game.Remove(game);
            } else if (username == game.player2username)
            {
                game.player2username = null;
                await _service.UpdateGameInfoAsync(game);

            }else if (username == game.player3username)
            {
                game.player3username = null;
                await _service.UpdateGameInfoAsync(game);
            }
            else if (username == game.player4username)
            {
                game.player4username = null;
                await _service.UpdateGameInfoAsync(game);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(long id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
