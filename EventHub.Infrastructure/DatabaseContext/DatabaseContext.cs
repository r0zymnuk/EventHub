using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Infrastructure.DataContext;
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }

    public DbSet<Event> Events => Set<Event>();
    public DbSet<Tour> Tours => Set<Tour>();
    public DbSet<User> Users => Set<User>();
}
