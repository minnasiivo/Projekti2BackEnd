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

        public async Task<IEnumerable<Game>> GetGames()
        {
            IEnumerable<Game> games = await _repository.GetAllGamesAsync();
            
            
            return games;
        }

        public async Task<IEnumerable<Game>> GetGamesForUser(string username)
        {
            IEnumerable<Game> games = await _repository.GetGamesForUserAsync(username);

            return games;
        }

        public async Task<Game> UpdateGameInfoAsync(Game game)
        {
            Game dbGame = await _repository.GetGameAsync(game.Id);
            dbGame.owner = game.owner;
            dbGame.player2 = game.player2;
            dbGame.player3 = game.player3;
            dbGame.player4 = game.player4;
            dbGame.player2username = game.player2username;
            dbGame.player3username = game.player3username;
            dbGame.player4username = game.player4username;
            dbGame.Score = game.Score;
            dbGame.GameTime = game.GameTime;

            Game updateGame = await _repository.UpdateGameInfoAsync(dbGame);
            if (updateGame == null)
            {
                return null;
            }
            return dbGame;
        }
    }
}
