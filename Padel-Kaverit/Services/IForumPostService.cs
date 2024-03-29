﻿using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
    public interface IForumPostService
    {
        public Task<ForumPost> AddPostAsync(ForumPost forumPos, string usernamet);
        public Task<ForumPost> UpdatePostsync(ForumPost forumPost);
        public Task<bool> RemovePostAsync(long id);
        public Task<IEnumerable<ForumPost>> GetPostsByDateAsync(DateTime date);
         public Task<IEnumerable<ForumPost>> GetPostsByWriter(string username);
        public Task<IEnumerable<ForumPost>>  GetAllPosts();
    }
}
