using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
    public interface IUserService
    {
        public Task<UserDTO> CreateUserAsync(User user);
        public Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        public Task<UserDTO> GetUserAsync(string userName);
       // public Task<UserDTO> GetUserAsync(string firstName);
        //public Task<UserDTO> GetUserAsync(string lastName);
        public Task<UserDTO> GetUserAsync(long id);
        public Task<UserDTO> UpdateUserAsync(UserDTO user);

        public Task<Boolean> DeleteUserAsync(long id);


    }
}
