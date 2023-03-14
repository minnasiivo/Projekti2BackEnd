using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
   public interface IGamesService
    {
        public Task<Game> AddGameasync(Game game, string username);

        public Task<Game> UpdateGameInfoAsync(Game game);
        public Task<Boolean> DeleteGameAsync(long Id);
        public Task<IEnumerable<Game>> GetGames();
        public Task<IEnumerable<Game>> GetGamesForUser(string username);

        public Task<GameResultsDTO> GetGameResults(string username);
    }
}
