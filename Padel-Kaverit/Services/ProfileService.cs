using Padel_Kaverit.Models;
using Padel_Kaverit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repository;
        private readonly IUserRepository _userRepository;

        public async Task<Profile> AddProfileAsync(Profile profile)
        {
            Profile newProfile = new Profile
            {
                Id = profile.Id,
                BirthDate = profile.BirthDate,
                Skill = profile.Skill,
                Bio = profile.Bio,
                PictureUrl = profile.PictureUrl
            };

            newProfile = await _repository.AddProfileAsync(newProfile);

            return newProfile;
        }

        public async Task<Profile> GetProfleAsync(long Id)
        {
            Profile profile = await _repository.GetProfleAsync(Id);
            return profile;
        }

        /*
          public ProfileService(IProfileRepository repository)
          {
              _repository = repository;
          }
         */


        public async Task<Profile> UpdateProfileAsync(Profile profile)
        {
       

            Profile dbProfile = await _repository.GetProfleAsync(profile.Id);
            dbProfile.BirthDate = profile.BirthDate;
            dbProfile.Bio = profile.Bio;
            dbProfile.Skill = profile.Skill;

            Profile updateProfile = await _repository.UpdateProfileAsync(dbProfile);
            if(updateProfile == null)
            {
                return null; 
            }

            return updateProfile;

        }
    }
}
