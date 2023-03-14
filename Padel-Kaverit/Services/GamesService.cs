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
       
        public async Task<Game> AddGameasync(Game game, string user)
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

        public async Task<GameResultsDTO> GetGameResults(string username)
        {
            List<Game> games =( await _repository.GetGamesForUserAsync(username)).ToList();
            int allGames = games.Count;
            int win = 0;
            int loose = 0;
            int draw = 0;

            foreach (Game game in games)
            {
                string score = game.Score;

                if (score == "voitto")
                {
                    if (game.owner == username) //tai player2username = username
                    {
                        win = +1;
                    }else if (game.player3username == username)//tai player4username = username
                    {
                        loose = +1;
                    }

                } else if (score == "tappio")
                {
                    if (game.owner == username) //tai player2username = username
                    {
                        loose = +1;
                    }
                    else if (game.player4username == username) //tai player4username = username
                    {
                        win = +1;
                    }
                } else if (score == "tasapeli")
                {
                    draw = +1;
                }

            }

            GameResultsDTO result = new GameResultsDTO();
            result.win = win;
            result.loose = loose;
            result.draw = draw;

            return result;

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
