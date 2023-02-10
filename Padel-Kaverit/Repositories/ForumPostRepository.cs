using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Repositories
{
    public class ForumRepository :  IForumPostRepository
    {
        private readonly PadelContext _context;
        public ForumRepository(PadelContext context)
        {
            _context = context;
        }
    }
}
