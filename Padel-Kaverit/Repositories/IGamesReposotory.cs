using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Repositories
{
    public interface IGamesReposotory
    {
        public Task<Game> AddGameasync(Game game);
        public Task<Game> GetGameAsync( long id);

        public Task<Game> UpdateGameInfoAsync(Game game);
        public Task<Boolean> DeleteGameAsync(long Id);
        public Task<IEnumerable<Game>> GetAllGamesAsync();
        
        public Task<IEnumerable<Game>> GetGamesForUserAsync(string username);
    }
}
