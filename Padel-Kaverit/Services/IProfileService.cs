﻿using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
   public interface IProfileService
    {
        public Task<Profile> UpdateProfileAsync(Profile profile);
    }
}