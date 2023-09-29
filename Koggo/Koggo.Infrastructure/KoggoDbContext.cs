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
