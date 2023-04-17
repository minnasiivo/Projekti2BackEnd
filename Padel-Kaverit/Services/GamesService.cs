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
            Double win = 0;
            Double loose = 0;
            Double draw = 0;
            List<Game> games =( await _repository.GetGamesForUserAsync(username)).ToList();
            Double allGames = games.Count;


            foreach (Game game in games)
            {
                string score = game.Score;

                if (score == "Voitto")
                {
                    if (game.owner == username || game.player2username==username) // pelaajat 1&2 ovat samaa joukkuetta
                    {
                        win = +1;
                    }else if (game.player3username == username || game.player4username == username) // pelaajat 3 & 4 samaa joukkuetta
                    {
                        loose = +1;
                    }

                } else if (score == "Tappio")
                {
                    if (game.owner == username || game.player2username == username) 
                    {
                        loose = +1;
                    }
                    else if (game.player4username == username || game.player3username == username) 
                    {
                        win = +1;
                    }
                } else if (score == "Tasapeli")
                {
                    draw = +1;
                }

            }
            
            Double winprocent = (win/allGames)*100;
            Double looseprocent = (loose/allGames)*100;
            Double drawprocent = (draw/allGames)*100;

            GameResultsDTO result = new GameResultsDTO();
            result.win = winprocent;
            result.loose = looseprocent;
            result.draw = drawprocent;

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
            //Tarkista muuttuuko pelaajatiedot, siten, että nimen tilalle tulee käyttäjänimi
            IEnumerable<User> users =await _userRepository.GetAllUsersAsync();
            foreach (User user in users){

                if (user.UserName == game.player2)
                {
                    game.player2username = game.player2;
                }
                else if (user.UserName == game.player3)
                {
                    game.player3username = game.player3;
                }
                else if (user.UserName == game.player4)
                {
                    game.player4username = game.player4;
                }

            }

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
