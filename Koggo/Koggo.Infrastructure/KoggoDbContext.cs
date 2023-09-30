using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koggo.Domain.Models;
using Koggo.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Koggo.Infrastructure
{
    public class KoggoDbContext : DbContext, IUnitOfWork
    {
        public KoggoDbContext(DbContextOptions<KoggoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Koggo.Domain.Models.UserService>(us =>
            {
                us.HasOne(x => x.User)
                    .WithMany(p => p.UserServices)
                    .HasForeignKey(f => f.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                us.HasMany(x => x.Reservations)
                    .WithOne(p => p.UserService)
                    .HasForeignKey(f => f.UserServiceId);

                us.HasOne(x => x.Service)
                    .WithMany(p => p.UserServices)
                    .HasForeignKey(f => f.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Koggo.Domain.Models.User>(u =>
            {
                u.HasIndex(x => x.Username)
                    .IsUnique(true);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserService> UserServices { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            var savedCount = await SaveChangesAsync(cancellationToken);

            if (savedCount > 0)
                return true;

            return false;
        }
    }
}
