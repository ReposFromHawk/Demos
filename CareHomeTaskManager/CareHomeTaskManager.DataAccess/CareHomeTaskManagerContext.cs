using CareHomeTaskManager.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.DataAccess
{
    public class CareHomeTaskManagerContext : DbContext
    {
        public CareHomeTaskManagerContext(DbContextOptions<CareHomeTaskManagerContext> options):base(options)
        {
        }

        public DbSet<CareTask> CareTasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=MyDatabase.db");
        }
    }
}
