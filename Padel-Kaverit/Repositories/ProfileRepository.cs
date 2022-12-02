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

        public ProfileRepository(PadelContext context)
        {
            _context = context;
        }

        public Task<Profile> GetProfleAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Profile> UpdateProfileAsync(Profile profile)
        {
            _context.Profile.Update(profile);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return profile;
        }

    }
}
