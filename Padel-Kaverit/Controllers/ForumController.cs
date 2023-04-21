using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padel_Kaverit.Middleware;
using Padel_Kaverit.Models;
using Padel_Kaverit.Repositories;
using Padel_Kaverit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Padel_Kaverit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IForumPostService _service;
        private readonly PadelContext _context;
        private readonly IUserAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public ForumController(IForumPostService service, PadelContext context, IUserAuthenticationService authenticationService, IUserService userService)
        {
            _service = service;
            _context = context;
            _authenticationService = authenticationService;
            _userService = userService;
        }

        // GET: api/Forum
        /// <summary>
        /// Gets all forum posts
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForumPost>>> GetPosts()
        {
            return await _context.ForumPost.ToListAsync();
        }

        // GET: api/Forum/5
        /// <summary>
        /// Gets one forum post by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ForumPost>> GetPosts(long id)
        {
            var post = await _context.ForumPost.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/Forum/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Change post 
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPost(long id, ForumPost post)
        {

            // Pitää tarkistaa, että muokata vain omia postauksiaan 
            //tai admin oikeuksilla saa muokata kaikkia


            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
             
                    return NotFound();
               
            }

            return NoContent();
        }

        // POST: api/Forum
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        ///Post to forum
        /// </summary>
        [HttpPost("{username}")]
        [Authorize]
        public async Task<ActionResult<ForumPost>> PostForum(ForumPost post)
        {

            string username = this.User.FindFirst(ClaimTypes.Name).Value;
            post.Writer = username;


            ForumPost newPost = await _service.AddPostAsync(post, username);
            if (newPost != null)
            {
                return newPost;
              
            }
            return StatusCode(502);

      
        }

        // DELETE: api/Farum/
        /// <summary>
        ///Delete post from forum
        /// </summary>
        [HttpDelete]
        [Authorize]
      
        public async Task<IActionResult> DeletePost(long id)
        {
          //  tarkistaa, että poistaa vain omia postauksiaan 
            
            string username = this.User.FindFirst(ClaimTypes.Name).Value;

            var post = await _context.ForumPost.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            if (post.Writer == username )
            {

                _context.ForumPost.Remove(post);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool ForumPostExists(long id)
        {
            return _context.ForumPost.Any(e => e.Id == id);
        }



        // DELETE: api/Farum/5
        /// <summary>
        ///Delete post from forum by Admin user
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDeleteAnyPosts(long id)
        {
            
            //admin oikeuksilla saa poistaa kaikkia

            var post = await _context.ForumPost.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.ForumPost.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(long id)
        {
            return _context.ForumPost.Any(e => e.Id == id);
        }

    }

}

