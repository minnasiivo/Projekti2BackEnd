﻿using Microsoft.EntityFrameworkCore;
using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Repositories
{
    public class ForumRepository :  IForumPostRepository
    {
        private readonly PadelContext _context;
        public ForumRepository(PadelContext context)
        {
            _context = context;
        }

        public async Task<ForumPost> AddPostAsync(ForumPost post)
        {
            _context.ForumPost.Add(post);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return post;
        }

        public async Task<bool> DeletePostAsync(long Id)
        {
            ForumPost post = _context.ForumPost.Find(Id);
            _context.ForumPost.Remove(post);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            { 
                return false; 
            }

            return true;
        }

        public async Task<IEnumerable<ForumPost>> GetAllPostAsync()
        {
            return await _context.ForumPost.ToListAsync();
        }

        public async Task<IEnumerable<ForumPost>> GetPostAsync(string writer)
        {

            return await _context.ForumPost.Include(i => i.Writer).ToListAsync();
        }

        public async Task<IEnumerable<ForumPost>> GetPostAsync(DateTime time)
        {
            return await _context.ForumPost.Include(i => i.Published).ToListAsync();
        }

        public async Task<ForumPost> GetPostById(long id)
        {
            return await _context.ForumPost.Where(x => x.Id == id).FirstOrDefaultAsync();
               
        }

        public async Task<ForumPost> UpdatePostAsync(ForumPost post)
        {
            _context.ForumPost.Update(post);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return post;
        }
    }
}

