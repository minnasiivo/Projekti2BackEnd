﻿using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
   public interface IProfileService
    {
        public Task<ProfileDTO> AddProfileAsync(ProfileDTO profile, String username);
        public Task<Profile> UpdateProfileAsync(Profile profile);
        public Task<Profile> GetProfleAsync(long Id);
    }
}
