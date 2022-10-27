using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Padel_Kaverit.Models
{
    public class UserDTO
    {
        public long Id { get; set; }
        
        public String Name { get; set; }
        
        public bool IsAdmin { get; set; }
    }
}
