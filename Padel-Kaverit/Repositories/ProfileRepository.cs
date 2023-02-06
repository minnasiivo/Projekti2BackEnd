using Microsoft.EntityFrameworkCore;
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

        public async Task<Profile> AddProfileAsync(Profile profile)
        {
            _context.Profile.Add(profile);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return profile;
        }

        public async Task<IEnumerable<Profile>> GetProfileAsync()
        {

            return await _context.Profile.Include(i => i.Owner).ToListAsync();
           // return await _context.Profile.ToListAsync();
   
        }

        public async Task<Profile> GetProfleAsync(long Id)
        {
            return await _context.Profile.Where(x => x.Id == Id).FirstOrDefaultAsync();
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
