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

        public async Task<ProfileDTO> AddProfileAsync(ProfileDTO profile, String username)
        {
           
            profile.Owner = username;

            Profile newProfile = await DTOToProfile(profile);
                if (newProfile == null)
            { return null; }

           
          
           // await _repository.AddProfileAsync(newProfile);
            newProfile = await _repository.AddProfileAsync(newProfile);
            if (newProfile.Id != 0)
            {
               
                return ProfileToDTO(newProfile);
            } 
            else { return null; }
                
        }


        public async Task<ProfileDTO> GetProfleAsync(long Id)
        {
            Profile profile = await _repository.GetProfleAsync(Id);
            ProfileDTO dto = ProfileToDTO(profile);
            return dto;
        }

        public async Task<ProfileDTO> GetProfleAsync(string username)
        {
            
            Profile profile = await _repository.GetProfleAsync(username);
            ProfileDTO dto = ProfileToDTO(profile);
            return dto;
        }

        public async Task<IEnumerable<ProfileDTO>> GetAllProfilesAsync()
        {
            IEnumerable<Profile> profiles = await _repository.GetProfileAsync();
            List<ProfileDTO> profileDTOs = new List<ProfileDTO>();
            foreach (Profile p in profiles)
            {
               profileDTOs.Add(ProfileToDTO(p));
            }
            return profileDTOs;
        }

        public async Task<ProfileDTO> UpdateProfileAsync(ProfileDTO profileDTO)
        {
            Profile profile = await DTOToProfile(profileDTO);

           Profile dbProfile = await _repository.GetProfleAsync(profile.Id);
            dbProfile.BirthDate = profile.BirthDate;
            dbProfile.Bio = profile.Bio;
            dbProfile.Skill = profile.Skill;

            Profile updateProfile = await _repository.UpdateProfileAsync(dbProfile);
            if(updateProfile == null)
            {
                return null; 
            }

            ProfileDTO updateProfileDTO = ProfileToDTO(updateProfile);
            return updateProfileDTO;

        }

        private ProfileDTO ProfileToDTO(Profile profile)
        {
            //User user = await _userRepository.GetUserAsync(profile.Owner.Id);

            ProfileDTO dto = new ProfileDTO();
            dto.Id = profile.Id;
            dto.FirstName = profile.Owner.FirstName;
            dto.LastName =profile.Owner.LastName;
            dto.BirthDate = profile.BirthDate;
            dto.Skill = profile.Skill;
            dto.PictureUrl = profile.PictureUrl;
            dto.Bio = profile.Bio;
            dto.Owner = profile.Owner.UserName;
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
