using Padel_Kaverit.Models;
using Padel_Kaverit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
    public class GamesService : IGamesService
    {
        private readonly IGamesReposotory _repository;
        private readonly IUserRepository _userRepository;

        public GamesService (IGamesReposotory repository, IUserRepository userRepository) 
        { 
            _repository = repository;
            _userRepository = userRepository;
        }
       
        public async Task<Game> AddGameasync(Game game, User user)
        {
            game.owner = user;

            try
            {
                await _repository.AddGameasync(game);
            }
            catch
            {
                return null;
            }
            return game;
        }

        public async Task<Boolean> DeleteGameAsync(long Id)
        {
            Game game = await _repository.GetGameAsync(Id);
            if (game != null)
            {
                return await _repository.DeleteGameAsync(Id);

            }
            return false;
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
