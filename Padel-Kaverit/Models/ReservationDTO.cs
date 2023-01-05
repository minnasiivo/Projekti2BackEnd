using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Models
{
    public class ReservationDTO
    {
        public long Id { get; set; }
        public string Target { get; set; }
        public String Owner { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
