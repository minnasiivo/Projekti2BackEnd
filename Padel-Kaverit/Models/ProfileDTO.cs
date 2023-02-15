using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Models
{
    public class ProfileDTO
    {
        public long Id { get; set; }

        //profile DTo --> owner long
        //tallennuksessa haku --> jolla loytyy käyttäjä
        public string Owner { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string BirthDate { get; set; }
        public string Skill { get; set; }
        public string Bio { get; set; }
        public string PictureUrl { get; set; }

  
    }
}
