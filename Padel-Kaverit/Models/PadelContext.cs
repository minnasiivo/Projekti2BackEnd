using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Padel_Kaverit.Models
{
    public class PadelContext : DbContext
   
    {
           public PadelContext(DbContextOptions<PadelContext> options)
                : base(options)
            {
            }

            public DbSet<User> User { get; set; }
        
    }

}
