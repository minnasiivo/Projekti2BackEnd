using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
    interface IForumPostService
    {
        public Task<ForumPost> AddPostAsync(ForumPost forumPost);
        public Task<ForumPost> UpdatePostsync(ForumPost forumPost);
        public Task<ForumPost> RemovePostAsync(ForumPost forumPost);
        public Task<ForumPost> GetPostAsync(long Id);
        public Task<ForumPost> GetPostsByWriter(string username);
    }
}
