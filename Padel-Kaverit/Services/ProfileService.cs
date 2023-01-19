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

        public ProfileService(IProfileRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<ProfileDTO> AddProfileAsync(ProfileDTO profile)
        {

            Profile newProfile = await DTOToProfile(profile);
                if (newProfile == null)
            { return null; }


           // await _repository.AddProfileAsync(newProfile);
            newProfile = await _repository.AddProfileAsync(newProfile);
            if (newProfile.Id != 0)
            {
                // return ProfileToDTO(newProfile);
                return ProfileToDTO(newProfile);
            } 
            else { return null; }
                
          
            /*
            if (newProfile == null)
            { return null; }

            newProfile = await _repository.AddProfileAsync(newProfile);

            return newProfile;
            */
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

        private ProfileDTO ProfileToDTO(Profile profile)
        {
            ProfileDTO dto = new ProfileDTO();
            dto.Id = profile.Id;
            dto.BirthDate = profile.BirthDate;
            dto.Skill = profile.Skill;
            dto.PictureUrl = profile.PictureUrl;
            dto.Bio = profile.Bio;
            dto.Owner = profile.Owner.Id;
            return dto;
        }        

        private async Task<Profile> DTOToProfile(ProfileDTO dto)
        {
            User owner = await _userRepository.GetUserAsync(dto.Owner);
            if (owner == null)
            {
                return null;
            }
            Profile profile = new Profile();
            profile.Id = dto.Id;
            profile.BirthDate = dto.BirthDate;
            profile.Skill = dto.Skill;
            profile.PictureUrl = dto.PictureUrl;
            profile.Bio = dto.Bio;
            profile.Owner = owner;
            return profile;
        }
    }
}
