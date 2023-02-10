using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Repositories
{
    interface IGamesReposotory
    {
        public Task<Game> AddGameasync(Game game);

        public Task<Game> UpdateGameInfoAsync(Game game);
        public Task<bool> DeleteGameAsync(long Id);
        public Task<IEnumerable<Game>> GetAllGamesAsync();
        public Task<IEnumerable<Game>> GetGamesForUserAsync(string username);
    }
}
