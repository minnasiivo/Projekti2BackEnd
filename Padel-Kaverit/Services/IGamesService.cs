﻿using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
    interface IGamesService
    {
        public Task<Game> AddGameasync(Game game);

        public Task<Game> UpdateGameInfoAsync(Game game);
        public Task<Game> DeleteGameAsync(long Id);
        public Task<IEnumerable<Game>>GetGameStats(string score, string username);
    }
}