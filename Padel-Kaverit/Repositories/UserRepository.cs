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
            _context.Users.Add(user);

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
            return await _context.Users.ToListAsync();

        }

        public Task<User> GetUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(User user)
        {
          //  _context.Users.Update(user);

            try
            {
               // await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
           return null;
        }

        public Task<User> GetUserAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(User user)
        {
            //throw new NotImplementedException();
            Console.WriteLine("HEIP");
            return null;
        }

        public Task<User> GetUserAsync(object name)
        {
            throw new NotImplementedException();
        }
    }
}
