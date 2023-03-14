using Google.Apis.Admin.Directory.directory_v1.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User = Padel_Kaverit.Models.User;

namespace Padel_Kaverit.Middleware
{
    public interface IUserAuthenticationService
    {
        Task<User> Authenticate(string username, string password);
        Task<bool> IsAllowed(String userName, UserDTO user);
        Task<bool> IsAllowed(String userNAme, ReservationDTO reservation);
        Task<bool> IsAllowed(String username, ProfileDTO profile);
    }

    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly PadelContext _padelContext;

        public UserAuthenticationService(PadelContext context)
        {
            _padelContext = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _padelContext.User.Where(x => x.UserName == username).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

           byte[] salt = user.Salt;

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
              password: password,
              salt: salt,
              prf: KeyDerivationPrf.HMACSHA256,
              iterationCount: 10000,
              numBytesRequested: 256 / 8
              ));

            if (hashedPassword != user.Password)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> IsAllowed(string userName, UserDTO userInfo)
        {
            var user = await _padelContext.User.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
            { return false; }
            if (user.IsAdmin || user.UserName == userInfo.UserName)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsAllowed(string userNAme, ReservationDTO reservation)
        {
            var user = await _padelContext.User.Where(x => x.UserName == userNAme).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            if (user.IsAdmin || user.UserName == reservation.Owner)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsAllowed(string username, ProfileDTO profile)
        {
            var user = await _padelContext.User.Where(x => x.UserName == username).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            if (user.UserName == profile.Owner)
            {
                return true;
            }
            return false;
        }
    }
}
