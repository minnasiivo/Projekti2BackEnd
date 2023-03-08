using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly PadelContext _context;
        public ForumController(PadelContext context)
        {
            _context = context;
        }

        // GET: api/Forum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForumPost>>> GetPosts()
        {
            return await _context.ForumPosts.ToListAsync();
        }

        // GET: api/Forum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ForumPost>> GetPosts(long id)
        {
            var post = await _context.ForumPosts.FindAsync(id);

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
            _context.ForumPosts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Farum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(long id)
        {
            var post = await _context.ForumPosts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.ForumPosts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ForumPostExists(long id)
        {
            return _context.ForumPosts.Any(e => e.Id == id);
        }
    }

}

