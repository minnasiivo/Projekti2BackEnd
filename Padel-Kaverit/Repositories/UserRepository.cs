using Microsoft.EntityFrameworkCore;
using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PadelContext _context;
        

        public UserRepository(PadelContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.User.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.User.ToListAsync();

        }

        public async Task<User> GetUserAsync(string userName)
        {
            return await _context.User.Where(x => x.UserName == userName).FirstOrDefaultAsync();

        }

        public async Task<User> UpdateUserAsync(User user)
        {
           _context.User.Update(user);

            try
            {
               await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
           return user;
        }

        public async Task<User> GetUserAsync(long id)
        {
            return await _context.User.Where(x => x.Id == id).FirstOrDefaultAsync();
            /*User user = await _repository.GetUserAsync(id);
             if ( user == null)
             {
                 return null;
             }
             return UserToDTO(user);
            */
            //return null;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.User.Remove(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            { return false; }
            return true;
        }

        public Task<User> GetUserAsync(object name)
        {
            throw new NotImplementedException();
        }
    }
}
