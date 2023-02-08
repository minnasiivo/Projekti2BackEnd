using Padel_Kaverit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Padel_Kaverit.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Padel_Kaverit.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _repository;

        public UserServices(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<UserDTO> CreateUserAsync(User user)
        {

          
            // tähän voi lisätä tarkistuksen, onko käyttäjänimellä jo luotu käyttäjä, jos on palautetaan virhe

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));


            User newUser = new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,

                Salt = salt,
                Password = hashedPassword,
                IsAdmin = user.IsAdmin
            };

            newUser = await _repository.AddUserAsync(newUser);

            return UserToDTO(newUser);
        }

        public async Task<bool> DeleteUserAsync(long id)
        {
            User user = await _repository.GetUserAsync(id);
            if (user != null)
            {
                return await _repository.DeleteUserAsync(user);

            }
            return false;

        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            List<User> list = (await _repository.GetAllUsersAsync()).ToList();
            List<UserDTO> dtoList = new List<UserDTO>();
            foreach (User u in list)
            {
                dtoList.Add(UserToDTO(u));
            }
            return dtoList;
        }

        public async Task<UserDTO> GetUserAsync(long id)
        {
            User user = await _repository.GetUserAsync(id);
            return UserToDTO(user);
        }

        public async Task<UserDTO> GetUserAsync(string username)
        {
            User user = await _repository.GetUserAsync(username);
            if (user == null)
            {
                return null;
            }
            return UserToDTO(user);
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO user)
        {
            User dbUser = await _repository.GetUserAsync(user.UserName);
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Email = user.Email;
            User updateUser = await _repository.UpdateUserAsync(dbUser);
            if (updateUser == null)
            {
                return null;
            }
            return UserToDTO(updateUser);
        }
        /*
                public Task<UserDTO> UpdateUserAsync(string username)
                {
                    throw new NotImplementedException();
                }
        */
        private User DTOToUser(UserDTO user, String password)
        {
            User newUser = new User();
            newUser.Email = user.Email;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.UserName = user.UserName;
            newUser.Password = password;

            return newUser;
        }



        private UserDTO UserToDTO(User user)
        {
            UserDTO dto = new UserDTO();
            dto.Id = user.Id;
            dto.Email = user.Email;
            dto.FirstName = user.FirstName;
            dto.LastName = user.LastName;
            dto.IsAdmin = user.IsAdmin;
            dto.UserName = user.UserName;

            return dto;

        }
    }
}
