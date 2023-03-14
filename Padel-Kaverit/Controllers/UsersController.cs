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
    public class UsersController : ControllerBase
    {
        //private readonly PadelContext _context;
        private readonly IUserService _service;
        private readonly IUserAuthenticationService _UserAuthenticationService;


        public UsersController(IUserService service, IUserAuthenticationService userAuthenticationService)
        {
            _service = service;
            _UserAuthenticationService = userAuthenticationService;
        }

        // GET: api/Users
        /// <summary>
        /// Gets a list af all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
       // [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _service.GetAllUsersAsync());

        }

        // GET: api/Users/5
        [HttpGet("{name}")]
        public async Task<ActionResult<UserDTO>> GetUser(string name)
        {
            var user = await _service.GetUserAsync(name);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Edit user
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutUser(UserDTO user)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;
         
            if (username != user.UserName)
            {
                return BadRequest();
            }
            //tarkista onko oikeus muokata:
            bool isAllowed = await _UserAuthenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, user);

            if (!isAllowed)
            {
                return Unauthorized();
            }
            UserDTO updateUser = await _service.UpdateUserAsync(user);
            if (updateUser == null)
            {
                return StatusCode(500);
            }
            return Ok(updateUser);


            //return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {

            if (await _service.GetUserAsync(user.UserName) == null)
            {
                UserDTO newUser = await _service.CreateUserAsync(user);

                return CreatedAtAction(nameof(PostUser), new { id = newUser.Id }, newUser);
            }
            return StatusCode(409);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (await _service.DeleteUserAsync(id));
            {
                return Ok("Deleted");
            }
            return NotFound();
        }


    }
}
