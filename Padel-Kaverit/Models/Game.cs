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
        public string player2username { get; set; }
        public string player3username { get; set; }
        public string player4username { get; set; }
        public User player1 { get; set; }
        public User player2 { get; set; }
        public User player3 { get; set; }
        public User player4 { get; set; }

        //voitot, tappiot, tasapelit
        public string Score { get; set; }
        
    }
}
