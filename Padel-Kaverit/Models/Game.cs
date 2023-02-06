using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Models
{
    public class Game
    {
        public DateTime GameTime { get; set; }
        public long Id { get; set; }
        public string CoPlayer { get; set; }
        public User otherPlayer { get; set; }
        public string Score { get; set; }
    }
}
