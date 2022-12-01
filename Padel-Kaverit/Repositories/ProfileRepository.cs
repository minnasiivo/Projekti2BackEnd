using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Repositories
{
    public class ProfileRepository:IProfileRepository
    {
        private readonly PadelContext _context;

        public Task<Profile> GetProfleAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<Profile> UpdateProfileAsync(Profile profile)
        {
            throw new NotImplementedException();
        }

    }
}
