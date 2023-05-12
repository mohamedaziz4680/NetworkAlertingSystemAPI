using Microsoft.EntityFrameworkCore;
using NetworkAlertingSystemAPI.Models;

namespace NetworkAlertingSystemAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UsersNotifications> UserNotifications { get; set; }
    }
}
