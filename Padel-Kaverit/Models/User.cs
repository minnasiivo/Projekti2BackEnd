using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Models
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public string Name { get; set; }


        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
        public string Password { get; set; }

        public byte[] Salt { get; set; }
       
     


    }
}
