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

        public UserRepository()
        {
        }

        public Task<User> addUserAsync(User user)
        {
            // _context.User.Add(user);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (Exception)
            //{
                
            //}
           return null;
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UptadeUserAsync(User user)
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

       
    }
}
