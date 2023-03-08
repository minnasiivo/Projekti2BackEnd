using Padel_Kaverit.Models;
using Padel_Kaverit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
    public class ForumPostService : IForumPostService
    {

        private readonly IForumPostRepository _repository;
        private readonly IUserRepository _userRepository;

        public ForumPostService (IForumPostRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }



        public async Task<ForumPost> AddPostAsync(ForumPost forumPost, User user)
        {
            forumPost.Writer = user;

            try
            {
                await _repository.AddPostAsync(forumPost); 
            }
           catch
            {
                return null;
            }
            return forumPost;

         
        }

        public async Task<IEnumerable<ForumPost>> GetAllPosts()
        {
            IEnumerable<ForumPost> posts = await _repository.GetAllPostAsync();
            return posts;
        }

        public async Task<IEnumerable<ForumPost>> GetPostsByDateAsync(DateTime date)
        {
            IEnumerable<ForumPost> posts = await _repository.GetPostAsync(date);
            return posts;
        }

        public async Task<IEnumerable<ForumPost>> GetPostsByWriter(string username)
        {
            IEnumerable<ForumPost> posts = await _repository.GetPostAsync(username);
            return posts;
        }

        public async Task<bool> RemovePostAsync(long id)
        {
            ForumPost post = await _repository.GetPostById(id);

            if (post != null)
            {
             return  await _repository.DeletePostAsync(id);
                
            }
            return false;
        }

        public async Task<ForumPost> UpdatePostsync(ForumPost forumPost)
        {
            ForumPost dbPost = await _repository.GetPostById(forumPost.Id);
            dbPost.Title = forumPost.Title;
            dbPost.Content = forumPost.Content;
            //dbPost.Answers.Content = forumPost.Answers.Content;


            ForumPost updatePost = await _repository.UpdatePostAsync(dbPost);
            if (updatePost == null)
            {
                return null;
            }

            return updatePost;
         
        }
    }
}
