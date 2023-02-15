using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Repositories
{
    public interface IForumPostRepository
    {
        public Task<ForumPost> AddPostAsync(ForumPost post);
        public Task<bool> DeletePostAsync(long Id);
        public Task<ForumPost> UpdatePostAsync(ForumPost post);
        public Task<IEnumerable<ForumPost>> GetPostAsync(string  username);
        public Task<IEnumerable<ForumPost>> GetPostAsync(DateTime time);
        public Task<IEnumerable<ForumPost>> GetAllPostAsync();


    }
}
