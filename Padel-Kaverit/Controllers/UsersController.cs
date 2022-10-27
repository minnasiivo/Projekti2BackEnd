using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
         

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET: api/Users
        [HttpGet]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

          /*  _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
          */

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            UserDTO newUser = await _service.CreateUserAsync(user);

            return CreatedAtAction("GetUser", new { id = newUser.Id }, newUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (await _service.DeleteUserAsync(id)) ;
            {
                return Ok("Deleted");
            }
            return NotFound();
        }


    }
}
