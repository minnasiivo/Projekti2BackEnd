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

        public Task<ForumPost> UpdatePostsync(ForumPost forumPost)
        {
            //Profile profile = await DTOToProfile(profileDTO);

            //Profile dbProfile = await _repository.GetProfleAsync(profile.Id);
            //dbProfile.BirthDate = profile.BirthDate;
            //dbProfile.Bio = profile.Bio;
            //dbProfile.Skill = profile.Skill;

            //Profile updateProfile = await _repository.UpdateProfileAsync(dbProfile);
            //if (updateProfile == null)
            //{
            //    return null;
            //}

            //ProfileDTO updateProfileDTO = ProfileToDTO(updateProfile);
            //return updateProfileDTO;
            return null;
        }
    }
}
