using Microsoft.EntityFrameworkCore;
using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Repositories
{
    public class GamesRepository : IGamesReposotory
    {
        private readonly PadelContext _context;
        public GamesRepository(PadelContext context)
        {
            _context = context;
        }

        public async Task<Game> AddGameasync(Game game)
        {
            _context.Game.Add(game);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return game;
        }

        public async Task<bool> DeleteGameAsync(long Id)
        {
            Game game = _context.Game.Find(Id);
            _context.Game.Remove(game);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            { return false; }
            return true;

        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await _context.Game.ToListAsync();
        }

        public async Task<Game> GetGameAsync( long id )
        {
            return await _context.Game.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesForUserAsync(string uesrname)
        {
            return await _context.Game.Include(i => i.owner).Include(i => i.player2).Include(i => i.player3).Include(i => i.player4).Include(i => i.player2username).Include(i => i.player3username).Include(i => i.player4username).ToListAsync();

        }


        public async Task<Game> UpdateGameInfoAsync(Game game)
        {
            _context.Game.Update(game);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
