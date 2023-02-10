using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
    public class GamesService : IGamesService
    {
        public Task<Game> AddGameasync(Game game)
        {
            throw new NotImplementedException();
        }

        public Task<Game> DeleteGameAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetGames()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetGamesForUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task<Game> UpdateGameInfoAsync(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
