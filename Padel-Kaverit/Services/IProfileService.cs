using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
   public interface IProfileService
    {
        public Task<ProfileDTO> AddProfileAsync(ProfileDTO profile, String username);
        public Task<ProfileDTO> UpdateProfileAsync(ProfileDTO profile);
        public Task<ProfileDTO> GetProfleAsync(long Id);
        public Task<IEnumerable<ProfileDTO>> GetAllProfilesAsync();
    }
}
