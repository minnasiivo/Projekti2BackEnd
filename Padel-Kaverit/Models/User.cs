using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }

    }
}
