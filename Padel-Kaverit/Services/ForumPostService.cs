using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
    public class ForumPostService : IForumPostService
    {
        public Task<ForumPost> AddPostAsync(ForumPost forumPost)
        {
            throw new NotImplementedException();
        }

        public Task<ForumPost> GetPostAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<ForumPost> GetPostsByWriter(string username)
        {
            throw new NotImplementedException();
        }

        public Task<ForumPost> RemovePostAsync(ForumPost forumPost)
        {
            throw new NotImplementedException();
        }

        public Task<ForumPost> UpdatePostsync(ForumPost forumPost)
        {
            throw new NotImplementedException();
        }
    }
}
