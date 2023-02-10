using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Repositories
{
    interface IForumPostRepository
    {
        public Task<ForumPost> AddProfileAsync(ForumPost post);

        public Task<ForumPost> UpdateProfileAsync(ForumPost post);
        public Task<ForumPost> GetProfleAsync(string  username);
        public Task<ForumPost> GetProfleAsync(DateTime time);
        public Task<IEnumerable<ForumPost>> GetProfileAsync();
    }
}
