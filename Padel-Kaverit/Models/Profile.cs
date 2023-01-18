using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Models
{
    public class Profile
    {
        public long Id { get; set; }
       public User Owner { get; set; }
        public string BirthDate { get; set; }
        public string Skill { get; set; }
        public string Bio { get; set; }
        public string PictureUrl { get; set; }
    }
}
