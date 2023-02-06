using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Repositories
{
   public interface IProfileRepository
    {
        public Task<Profile> AddProfileAsync(Profile profile);
      
        public Task<Profile> UpdateProfileAsync(Profile profile);
        public Task<Profile> GetProfleAsync(long Id);
        public Task<IEnumerable<Profile>> GetProfileAsync();


    }
}
