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
        public String UserName { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public bool IsAdmin { get; set; }
        public string Email { get; internal set; }
    }
}
