using LogisticBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticBot.DataAccess.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        

        public DbSet<User> Users { get; set; }
        public DbSet<Cargo> Cargos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>()
            .HasOne(pi => pi.User)
            .WithMany(p => p.Cargos)
            .HasForeignKey(pi => pi.OwnerId);
        }
    }
}
