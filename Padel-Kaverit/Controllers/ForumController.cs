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
        private readonly ForumPostService _service;
        private readonly PadelContext _context;
        private readonly IUserAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public ForumController(ForumPostService service, PadelContext context, IUserAuthenticationService authenticationService, IUserService userService)
        {
            _service = service;
            _context = context;
            _authenticationService = authenticationService;
            _userService = userService;
        }

        // GET: api/Forum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForumPost>>> GetPosts()
        {
            return await _context.ForumPost.ToListAsync();
        }

        // GET: api/Forum/5
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(long id, ForumPost post)
        {
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
        [HttpPost]
        public async Task<ActionResult<ForumPost>> PostForum(ForumPost post)
        {

            string username = this.User.FindFirst(ClaimTypes.Name).Value;
            post.Writer = username;


            ForumPost newPost = await _service.AddPostAsync(post, username);
            if (newPost != null)
            {
                return newPost;
              
            }
            return StatusCode(666);

            // _context.ForumPost.Add(post);
            // await _context.SaveChangesAsync();
            //return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Farum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(long id)
        {
            var post = await _context.ForumPost.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.ForumPost.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ForumPostExists(long id)
        {
            return _context.ForumPost.Any(e => e.Id == id);
        }
    }

}

