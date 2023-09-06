using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // This is the name of the table in the database.
        // Grabs the AppUser class and creates a table called Users.
        public DbSet<AppUser> Users {get; set;}
    }
}