using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koggo.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Koggo.Infrastructure
{
    public class KoggoDbContext : DbContext
    {
        public KoggoDbContext(DbContextOptions<KoggoDbContext> options) : base(options)
        {
            
        }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserService> UserServices { get; set; }

    }
}
