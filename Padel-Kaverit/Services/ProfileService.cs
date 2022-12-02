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
