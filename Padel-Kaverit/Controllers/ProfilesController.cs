using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
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
    public class ProfilesController : ControllerBase
    {
        private readonly PadelContext _context;
        private readonly IProfileService _service;
        private readonly IUserAuthenticationService _authenticationService;
        private readonly IUserService _userService;
       

        public ProfilesController(PadelContext context, IUserAuthenticationService userAuthenticationService, IProfileService service, IUserService userService)
{
            _service = service;
            _authenticationService = userAuthenticationService;
            _context = context;
            _userService = userService;
          
        }

        // GET: api/Profiles
        /// <summary>
        /// Gets profile information for  users
        /// </summary>
        /// <returns>User list as json</returns>
        /// <response code="200">Profile found</response>
        /// <response code="404">Profile not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProfileDTO>>> GetAllProfiles()
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;
            return Ok(await _service.GetAllProfilesAsync());
          
        }

        /// <summary>
        /// Gets profile information for  one user
        /// </summary>
        /// <param name="username">user's username</param>
        /// <returns>User information as json</returns>
        /// <response code="200">User found</response>
        /// <response code="404">User not found</response>
        /// 
        [HttpGet("{username}")]
       [Authorize]

        public async Task<ActionResult<ProfileDTO>> GetProfile(string username)
        {

            ProfileDTO profileDTO = await _service.GetProfleAsync(username);
            if (profileDTO == null)
            {
                return NotFound();
            }

            return Ok(profileDTO);
        }
  

        // PUT: api/Profiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// change profile information for user
        /// </summary>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutProfile(ProfileDTO profile)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;

            if (username != profile.Owner)
            {
                return BadRequest("EI ONNISTU");
            }


            ProfileDTO updateProfile = await _service.UpdateProfileAsync(profile);



            return NoContent();
        }

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// change profile information for user
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProfileDTO>> PostProfile(ProfileDTO profile)
        {
            // tarkistus, että yhdellä Userilla voi olla vain yksi profiili!

            string username = this.User.FindFirst(ClaimTypes.Name).Value;

            ProfileDTO existingProfile = await _service.GetProfleAsync(username);
            if (existingProfile != null)
            { return StatusCode(409); }
            else
            { 
            ProfileDTO newProfile = await _service.AddProfileAsync(profile, username);
            if (newProfile != null)
            {
                    return newProfile;
                //return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
            } 
            }
           

            return StatusCode(500);

        }




        // DELETE: api/Profiles/5
        /// <summary>
        /// delete users user profile
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProfile(long id)
        {
            var profile = await _context.Profile.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profile.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(long id)
        {
            return _context.Profile.Any(e => e.Id == id);
        }
    }
}
