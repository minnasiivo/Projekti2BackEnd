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

        public Task<IEnumerable<ForumPost>> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ForumPost>> GetPostsByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ForumPost>> GetPostsByWriter(string username)
        {
            throw new NotImplementedException();
        }

        public Task<ForumPost> RemovePostAsync(long id)
        {
            throw new NotImplementedException();
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
