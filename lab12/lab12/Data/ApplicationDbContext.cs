using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab12.Models;
using Microsoft.EntityFrameworkCore;

namespace lab12.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}
